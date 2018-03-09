using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrDeals.Data.Models;

namespace TrDeals.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий предложений
    /// </summary>
    public interface IOfferRepository
    {
        /// <summary>
        /// Добавляет предложение
        /// </summary>
        void AddOffer(Offer offer);

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        void RemoveOffer(Offer offer);

        /// <summary>
        /// Удаляет предложения
        /// </summary>
        void RemoveOffers(IEnumerable<Offer> offers);

        /// <summary>
        /// Получает предложение
        /// </summary>
        Offer GetOffer(Guid offerId, Guid userId);

        /// <summary>
        /// Получает предложение
        /// </summary>
        Offer GetOffer(Guid userId, string currencyPairFromId, string currencyPairToId, decimal price);

        /// <summary>
        /// Получает предложения
        /// </summary>
        Task<List<Offer>> GetOffers(string currencyPairFromId, string currencyPairToId, decimal price);

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetOffers(Guid userId);
    }
}
