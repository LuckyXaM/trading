using Microsoft.EntityFrameworkCore;
using TrModels;

namespace TrCurrencies.Data
{
    /// <summary>
    /// Контекст для работы с БД
    /// </summary>
    public class TrCurrenciesContext : DbContext
    {
        #region Свойства (Таблицы)

        /// <summary>
        /// Валюты
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Валютные пары
        /// </summary>
        public DbSet<CurrencyPair> CurrencyPairs { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        public TrCurrenciesContext(DbContextOptions<TrCurrenciesContext> options) : base(options)
        {
        }

        #endregion
    }
}
