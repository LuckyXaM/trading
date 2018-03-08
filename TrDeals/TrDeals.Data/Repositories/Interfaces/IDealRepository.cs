using System;
using TrDeals.Data.Models;

namespace TrDeals.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public interface IDealRepository
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
    }
}
