using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrCurrencies.Data.Repositories.Interfaces;
using TrModels;

namespace TrCurrencies.Data.Repositories.Logic
{
    /// <summary>
    /// Репозиторий валют
    /// </summary>
    public class CurrencyRepository : ICurrencyRepository
    {
        #region Свойства

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly TrCurrenciesContext _context;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public CurrencyRepository(TrCurrenciesContext context)
        {
            _context = context;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет валюты
        /// </summary>
        public void AddRangeCurrency(IEnumerable<Currency> currencies)
        {
            _context.Currencies.AddRange(currencies);
        }

        /// <summary>
        /// Добавляет валютные пары
        /// </summary>
        public void AddRangeCurrencyPairs(IEnumerable<CurrencyPair> currencyPairs)
        {
            _context.CurrencyPairs.AddRange(currencyPairs);
        }

        /// <summary>
        /// Получает валюту
        /// </summary>
        /// <returns></returns>
        public async Task<Currency> GetCurrency(string currencyId)
        {
            return await _context.Currencies.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CurrencyId == currencyId);
        }

        /// <summary>
        /// Получает валютную пару
        /// </summary>
        /// <returns></returns>
        public async Task<CurrencyPair> GetCurrencyPair(string currencyFromId, string currencyToId)
        {
            return await _context.CurrencyPairs.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CurrencyPairFromId == currencyFromId && c.CurrencyPairToId == currencyToId);
        }

        /// <summary>
        /// Получает валюты
        /// </summary>
        public List<Currency> GetCurrencies()
        {
            return _context.Currencies.AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Получает валютные пары
        /// </summary>
        public List<CurrencyPair> GetCurrencyPairs()
        {
            return _context.CurrencyPairs.AsNoTracking()
                .Include(c => c.CurrencyPairFrom)
                .Include(c => c.CurrencyPairTo)
                .ToList();
        }
        
        #endregion
    }
}
