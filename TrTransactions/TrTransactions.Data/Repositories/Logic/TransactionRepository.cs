using Microsoft.EntityFrameworkCore;
using System;
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

        #region Конструктор

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
        /// Добавляет транзакции
        /// </summary>
        public void AddRange(List<Transaction> transactions)
        {
            _context.Transactions.AddRange(transactions);
        }

        /// <summary>
        /// Получает транзакции пользователя по статусу
        /// </summary>
        /// <returns></returns>
        public IQueryable<Transaction> GetList(Guid userId, string currencyId, TransactionType transactionType)
        {
            return _context.Transactions.Where(t => t.UserId == userId && t.CurrencyId == currencyId.ToUpper() && t.TransactionType == transactionType);
        }

        /// <summary>
        /// Получает все транзакции пользователя
        /// </summary>
        public IQueryable<Transaction> GetList(Guid userId, string currencyId)
        {
            return _context.Transactions.AsNoTracking()
                .Where(t => t.UserId == userId && t.CurrencyId == currencyId.ToUpper());
        }

        /// <summary>
        /// Удаляет транзакции
        /// </summary>
        public void RemoveRange(List<Transaction> transactions)
        {
            _context.Transactions.RemoveRange(transactions);
        }

        #endregion
    }
}
