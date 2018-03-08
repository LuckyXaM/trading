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
        /// Получает предложение
        /// </summary>
        Offer GetOffer(Guid offerId, Guid userId);

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetOffers(Guid userId);
    }
}
