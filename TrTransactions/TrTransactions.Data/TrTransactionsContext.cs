using Microsoft.EntityFrameworkCore;
using TrTransactions.Data.Models;

namespace TrTransactions.Data
{
    /// <summary>
    /// Контекст для работы с БД
    /// </summary>
    public class TrTransactionsContext : DbContext
    {
        #region Свойства (Таблицы)

        /// <summary>
        /// Транзакция
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        public TrTransactionsContext(DbContextOptions<TrTransactionsContext> options) : base(options)
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// Модифицирует таблицы
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>()
                 .HasIndex(u => u.UserId)
                 .IsUnique();
        }

        #endregion
    }
}
