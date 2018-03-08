using Microsoft.EntityFrameworkCore;
using TrDeals.Data.Models;

namespace TrDeals.Data
{
    /// <summary>
    /// Контекст для работы с БД
    /// </summary>
    public class TrDealsContext : DbContext
    {
        #region Свойства (Таблицы)

        /// <summary>
        /// Предложения
        /// </summary>
        public DbSet<Offer> Offers { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        public TrDealsContext(DbContextOptions<TrDealsContext> options) : base(options)
        {
        }

        #endregion
    }
}
