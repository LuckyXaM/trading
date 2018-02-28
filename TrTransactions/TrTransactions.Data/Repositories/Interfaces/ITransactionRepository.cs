using System.Collections.Generic;
using TrTransactions.Data.Models;

namespace TrTransactions.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Добавляет транзакцию
        /// </summary>
        void Add(Transaction transaction);

        /// <summary>
        /// Получает типы валют
        /// </summary>
        IEnumerable<CurrencyType> GetCurrencyTypes(IEnumerable<string> currencyTypes);

        /// <summary>
        /// Добавляет типы валют
        /// </summary>
        void AddCurrencyTypes(IEnumerable<CurrencyType> currencyTypes);
    }
}
