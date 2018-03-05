using System;
using System.Collections.Generic;
using System.Linq;
using TrModels;
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
        /// Добавляет транзакции
        /// </summary>
        void AddRange(List<Transaction> transactions);

        /// <summary>
        /// Получает транзакции пользователя по статусу
        /// </summary>
        /// <returns></returns>
        IQueryable<Transaction> GetList(Guid userId, string currencyTypeId, TransactionType transactionType);

        /// <summary>
        /// Получает все транзакции пользователя
        /// </summary>
        /// <returns></returns>
        IQueryable<Transaction> GetList(Guid abonentId, string currencyTypeId);

        /// <summary>
        /// Удаляет транзакции
        /// </summary>
        void RemoveRange(List<Transaction> transactions);

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
