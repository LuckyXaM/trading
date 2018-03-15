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
        void Add(Offer offer);

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        void Remove(Offer offer);

        /// <summary>
        /// Удаляет предложения
        /// </summary>
        void RemoveRange(IEnumerable<Offer> offers);

        /// <summary>
        /// Получает предложение
        /// </summary>
        Offer Get(Guid offerId, Guid userId);

        /// <summary>
        /// Получает предложение
        /// </summary>
        Offer Get(Guid userId, string currencyPairFromId, string currencyPairToId, decimal price);

        /// <summary>
        /// Получает предложения
        /// </summary>
        Task<List<Offer>> GetList(string currencyPairFromId, string currencyPairToId, decimal price);

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetList(Guid userId);
    }
}
