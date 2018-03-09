using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrCurrencyClient.Interfaces;
using TrDeals.Data.Infrastructure.Interfaces;
using TrDeals.Data.Models;
using TrDeals.Data.Repositories.Interfaces;
using TrDeals.Service.Services.Interfaces;
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
        public async Task<bool> AddOfferAsync(Guid userId, string currencyFromId, string currencyToId, decimal ammount, decimal course)
        {
            if (ammount == 0 || course == 0 || !await _currencyClient.CheckCurrencyPair(currencyFromId, currencyToId) || !await _transactionClient.ReserveAsync(userId, currencyFromId, ammount))
            {
                return false;
            }

            try
            {

                var sameOffers = (await _offerRepository.GetOffers(userId))
                    .Where(o => o.Course == course && o.CurrencyFromId == currencyFromId && o.CurrencyToId == currencyToId);

                var offer = new Offer
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    Ammount = ammount + sameOffers.Sum(o => o.Ammount),
                    CurrencyFromId = currencyFromId,
                    CurrencyToId = currencyToId,
                    Course = course,
                    OfferId = Guid.NewGuid()
                };

                _offerRepository.RemoveOffers(sameOffers);
                _offerRepository.AddOffer(offer);

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                await _transactionClient.RemoveReserveAsync(userId, currencyFromId, ammount);

                return false;
            }
        }

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveOfferAsync(Guid offerId, Guid userId)
        {
            var offer = _offerRepository.GetOffer(offerId, userId);

            if (offer != null)
            {
                _offerRepository.RemoveOffer(offer);

                var transactionResult = await _transactionClient.RemoveReserveAsync(userId, offer.CurrencyFromId, offer.Ammount);

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
            var result = await _offerRepository.GetOffers(userId);

            return result;
        }

        #endregion

        #region Методы(private)

        #endregion
    }
}
