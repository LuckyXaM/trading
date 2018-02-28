using System.Collections.Generic;
using System.Linq;
using TrTransactions.Data.Models;
using TrTransactions.Data.Repositories.Interfaces;

namespace TrTransactions.Data.Repositories.Logic
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        #region Свойства

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly TrTransactionsContext _context;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public TransactionRepository(TrTransactionsContext context)
        {
            _context = context;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет транзакцию
        /// </summary>
        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        /// <summary>
        /// Получает типы валют
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CurrencyType> GetCurrencyTypes(IEnumerable<string> currencyTypes)
        {
            return _context.CurrencyTypes.Where(c => currencyTypes.Contains(c.Title));
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
