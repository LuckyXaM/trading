using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TrDeals.Data.Models;
using TrDeals.Data.Repositories.Interfaces;

namespace TrDeals.Data.Repositories.Logic
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public class DealRepository : IDealRepository
    {
        #region Свойства

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly TrDealsContext _context;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public DealRepository(TrDealsContext context)
        {
            _context = context;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        public void AddOffer(Offer offer)
        {
            _context.Offers.Add(offer);
        }

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        public void RemoveOffer(Offer offer)
        {
            _context.Offers.Remove(offer);
        }

        /// <summary>
        /// Получает предложение
        /// </summary>
        public Offer GetOffer(Guid offerId, Guid userId)
        {
            return _context.Offers.AsNoTracking()
                .FirstOrDefault(a => a.OfferId == offerId && a.UserId == userId);
        }
        
        #endregion
    }
}
