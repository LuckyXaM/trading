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
        Offer Get(Guid userId, string currencyFromId, string currencyToId, decimal price);

        /// <summary>
        /// Получает предложения
        /// </summary>
        Task<List<Offer>> GetList(string currencyFromId, string currencyToId, decimal price);

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetList(Guid userId);

        /// <summary>
        /// Получает предложения пользователя для валютной пары
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetList(Guid userId, string currencyOneId, string currenctTwoId);

        /// <summary>
        /// Получает предложения для валютной пары
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetList(string currencyOneId, string currenctTwoId);
    }
}
