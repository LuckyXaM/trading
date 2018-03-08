using System;
using System.Collections.Generic;
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
        private readonly IDealRepository _dealRepository;

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
            IDealRepository dealRepository,
            IUnitOfWork unitOfWork
            )
        {
            _currencyClient = currencyClient;
            _transactionClient = transactionClient;
            _dealRepository = dealRepository;
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
            var currencies = new List<string> { currencyFromId, currencyToId };

            if (ammount == 0 || course == 0 || !await _currencyClient.CheckCurrencies(currencies) || !await _transactionClient.ReserveAsync(userId, currencyFromId, ammount))
            {
                return false;
            }

            var offer = new Offer
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Ammount = ammount,
                CurrencyFromId = currencyFromId,
                CurrencyToId = currencyToId,
                Course = course,
                OfferId = Guid.NewGuid()
            };

            _dealRepository.AddOffer(offer);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveOfferAsync(Guid offerId, Guid userId)
        {
            var offer = _dealRepository.GetOffer(offerId, userId);

            if (offer != null)
            {
                _dealRepository.RemoveOffer(offer);

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

        #endregion

        #region Методы(private)

        #endregion
    }
}
