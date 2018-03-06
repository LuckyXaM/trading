using Microsoft.EntityFrameworkCore;
using TrDeals.Data.Models;
using TrModels;

namespace TrDeals.Data
{
    /// <summary>
    /// Контекст для работы с БД
    /// </summary>
    public class TrDealsContext : DbContext
    {
        #region Свойства (Таблицы)

        /// <summary>
        /// Аски
        /// </summary>
        public DbSet<Ask> Asks { get; set; }

        /// <summary>
        /// Биды
        /// </summary>
        public DbSet<Bid> Bids { get; set; }

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
