using System;
using System.Collections.Generic;
using System.Linq;
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
        IQueryable<Transaction> GetList(Guid userId, string currencyTypeId);

        /// <summary>
        /// Удаляет транзакции
        /// </summary>
        void RemoveRange(List<Transaction> transactions);
    }
}
