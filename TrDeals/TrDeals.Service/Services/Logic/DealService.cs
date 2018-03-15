using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrCurrencyClient.Interfaces;
using TrDeals.Data.Infrastructure.Interfaces;
using TrDeals.Data.Models;
using TrDeals.Data.Repositories.Interfaces;
using TrDeals.Service.Services.Interfaces;
using TrModels.ResourceModels;
using TrModels.Transaction;
using TrOperations.Service;
using TrTransactionClient.Interfaces;

namespace TrDeals.Service.Services.Logic
{
    /// <summary>
    /// Сервис сделок
    /// </summary>
    public class DealService : IDealService
    {
        #region Поля, свойства

        /// <summary>
        /// Клиент валют
        /// </summary>
        private readonly ICurrencyClient _currencyClient;

        /// <summary>
        /// Клиент транзакций
        /// </summary>
        private readonly ITransactionClient _transactionClient;

        /// <summary>
        /// Репозиторий транзакций
        /// </summary>
        private readonly IOfferRepository _offerRepository;

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис сделок
        /// </summary>
        public DealService(
            ICurrencyClient currencyClient,
            ITransactionClient transactionClient,
            IOfferRepository offerRepository,
            IUnitOfWork unitOfWork
            )
        {
            _currencyClient = currencyClient;
            _transactionClient = transactionClient;
            _offerRepository = offerRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddOfferAsync(Guid userId, string currencyFromId, string currencyToId, decimal volume, decimal price)
        {
            if (volume == 0 || price == 0 || !await _currencyClient.CheckCurrencyPairAsync(currencyFromId, currencyToId) || !await _transactionClient.ReserveAsync(userId, currencyFromId, volume))
            {
                return false;
            }

            try
            {
                var sameOffers = (await _offerRepository.GetList(userId))
                    .Where(o => o.Price == price && o.CurrencyFromId == currencyFromId && o.CurrencyToId == currencyToId);

                var offer = new Offer
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    Volume = volume + sameOffers.Sum(o => o.Volume),
                    CurrencyFromId = currencyFromId.ToUpper(),
                    CurrencyToId = currencyToId.ToUpper(),
                    Price = price,
                    OfferId = Guid.NewGuid()
                };

                _offerRepository.RemoveRange(sameOffers);
                _offerRepository.Add(offer);

                await _unitOfWork.SaveChangesAsync();

                var offers = (await _offerRepository.GetList(currencyToId, currencyFromId, MathOperations.RoudDivision(1, price)));

                await Matching(offer.Volume, price, userId, offers, currencyFromId, currencyToId);

                return true;
            }
            catch(Exception e)
            {
                await _transactionClient.RemoveReserveAsync(userId, currencyFromId, volume);

                return false;
            }
        }

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveOfferAsync(Guid offerId, Guid userId)
        {
            var offer = _offerRepository.Get(offerId, userId);

            if (offer != null)
            {
                _offerRepository.Remove(offer);

                var transactionResult = await _transactionClient.RemoveReserveAsync(userId, offer.CurrencyFromId, offer.Volume);

                if (transactionResult)
                {
                    await _unitOfWork.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<List<Offer>> GetOffersAsync(Guid userId)
        {
            var result = await _offerRepository.GetList(userId);

            return result;
        }

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<BidAskResourceModel> GetOffersAsync(string currencyFromId, string currencyToId)
        {
            var offers = await _offerRepository.GetList(currencyFromId.ToUpper(), currencyToId.ToUpper());

            var result = new BidAskResourceModel();
            result.Asks = Mapper.Map<List<Offer>, List<OfferRecourceModel>>(offers.Where(o => o.CurrencyFromId == currencyFromId.ToUpper())
                .OrderByDescending(o => o.Price)
                .ToList());
            result.Bids = Mapper.Map<List<Offer>, List<OfferRecourceModel>>(offers.Where(o => o.CurrencyFromId == currencyToId.ToUpper())
                .OrderBy(o => o.Price)
                .ToList());

            return result;
        }

        #endregion

        #region Методы(private)

        /// <summary>
        /// Находит подходящие предложения и осуществляет сделки
        /// </summary>
        private async Task Matching(decimal volume, decimal price, Guid userId, List<Offer> offers, string currencyFromId, string currencyToId)
        {
            if (offers != null && offers.Count > 0)
            {
                var operationDatas = new List<OperationData>();

                while (volume > 0)
                {
                    foreach (var item in offers.OrderByDescending(o => o.Price))
                    {
                        if (volume > 0)
                        {
                            var volumeToSell = MathOperations.RoundMultiplication(MathOperations.RoudDivision(1, item.Price), item.Volume);
                            var volumeToGet = MathOperations.RoundMultiplication(MathOperations.RoudDivision(1, price), volume);

                            decimal sellSellVolume = 0;
                            decimal sellBuyVolume = 0;
                            decimal buySellVolume = 0;
                            decimal buyBuyVolume = 0;

                            if (volume >= volumeToSell)
                            {
                                sellSellVolume = item.Volume;
                                sellBuyVolume = volumeToSell;
                                buySellVolume = volumeToSell;
                                buyBuyVolume = volumeToGet;
                            }
                            else
                            {
                                sellSellVolume = volumeToGet;
                                sellBuyVolume = volume;
                                buySellVolume = volume;
                                buyBuyVolume = item.Volume;

                                var finalOffer = new Offer
                                {
                                    CreatedAt = DateTime.UtcNow,
                                    CurrencyFromId = item.CurrencyFromId,
                                    CurrencyToId = item.CurrencyToId,
                                    Price = item.Price,
                                    UserId = item.UserId,
                                    Volume = item.Volume - buySellVolume
                                };

                                _offerRepository.Add(finalOffer);
                            }

                            _offerRepository.Remove(item);

                            // Операция в пользу продающего
                            var operationDataSell = new OperationData
                            {
                                UserId = item.UserId,
                                CurrencyId = item.CurrencyFromId,
                                BuyCurrencyId = item.CurrencyToId,
                                SellVolume = sellSellVolume,
                                BuyVolume = sellBuyVolume
                            };

                            // Операция в пользу покупающего
                            var operationDataBuy = new OperationData
                            {
                                UserId = userId,
                                CurrencyId = item.CurrencyToId,
                                BuyCurrencyId = item.CurrencyFromId,
                                SellVolume = buySellVolume,
                                BuyVolume = buyBuyVolume
                            };

                            operationDatas.Add(operationDataSell);
                            operationDatas.Add(operationDataBuy);

                            volume = volume - volumeToSell;
                        }
                    }
                }

                if (volume > 0)
                {
                    var finalOffer = new Offer
                    {
                        CreatedAt = DateTime.UtcNow,
                        CurrencyFromId = currencyFromId,
                        CurrencyToId = currencyToId,
                        Price = price,
                        UserId = userId,
                        Volume = volume
                    };

                    _offerRepository.Add(finalOffer);
                }

                var offerToRemove = _offerRepository.Get(userId, currencyToId, currencyFromId, price);
                _offerRepository.Remove(offerToRemove);

                if (await _transactionClient.BuyAsync(operationDatas))
                {
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        #endregion
    }
}
