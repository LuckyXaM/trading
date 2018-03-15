using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrDeals.Data.Models;
using TrDeals.Data.Repositories.Interfaces;

namespace TrDeals.Data.Repositories.Logic
{
    /// <summary>
    /// Репозиторий предложений
    /// </summary>
    public class OfferRepository : IOfferRepository
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
        public OfferRepository(TrDealsContext context)
        {
            _context = context;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        public void Add(Offer offer)
        {
            _context.Offers.Add(offer);
        }

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        public void Remove(Offer offer)
        {
            _context.Offers.Remove(offer);
        }

        /// <summary>
        /// Удаляет предложения
        /// </summary>
        public void RemoveRange(IEnumerable<Offer> offers)
        {
            _context.Offers.RemoveRange(offers);
        }

        /// <summary>
        /// Получает предложение
        /// </summary>
        public Offer Get(Guid offerId, Guid userId)
        {
            return _context.Offers.AsNoTracking()
                .FirstOrDefault(a => a.OfferId == offerId && a.UserId == userId);
        }

        /// <summary>
        /// Получает предложения
        /// </summary>
        public async Task<List<Offer>> GetList(string currencyPairFromId, string currencyPairToId, decimal price)
        {
            return await _context.Offers.AsNoTracking()
                .Where(o => o.CurrencyFromId == currencyPairFromId && o.CurrencyToId == currencyPairToId && o.Price <= price)
                .ToListAsync();
        }

        /// <summary>
        /// Получает предложение
        /// </summary>
        public Offer Get(Guid userId, string currencyPairFromId, string currencyPairToId, decimal price)
        {
            return _context.Offers.AsNoTracking()
                .FirstOrDefault(o => o.UserId == userId && o.CurrencyFromId == currencyPairFromId && o.CurrencyToId == currencyPairToId && o.Price == price);
        }

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<List<Offer>> GetList(Guid userId)
        {
            return await _context.Offers.AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        #endregion
    }
}
