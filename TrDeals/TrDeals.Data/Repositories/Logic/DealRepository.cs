using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TrDeals.Data.Repositories.Interfaces;
using TrModels;

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
        /// Получает типы валют
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CurrencyType> GetCurrencyTypes(IEnumerable<string> currencyTypes)
        {
            return _context.CurrencyTypes.AsNoTracking()
                .Where(c => currencyTypes.Contains(c.TransactionTypeId.ToUpper()));
        }

        /// <summary>
        /// Добавляет типы валют
        /// </summary>
        public void AddCurrencyTypes(IEnumerable<CurrencyType> currencyTypes)
        {
            _context.CurrencyTypes.AddRange(currencyTypes);
        }

        #endregion
    }
}
