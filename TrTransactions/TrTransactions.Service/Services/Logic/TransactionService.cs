using Microsoft.EntityFrameworkCore;
using System;
using TrTransactions.Data.Models;
using System.Threading.Tasks;
using TrTransactions.Data.Repositories.Interfaces;
using TrTransactions.Service.Services.Interfaces;
using TrTransactions.Data.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TrTransactions.Service.Services.Logic
{
    /// <summary>
    /// Сервис для управления транзакциями
    /// </summary>
    public class TransactionService : ITransactionService
    {
        #region Поля, свойства

        /// <summary>
        /// Репозиторий транзакций
        /// </summary>
        private readonly ITransactionRepository _transactionRepository;

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис для управления транзакциями
        /// </summary>
        public TransactionService(
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork
            )
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получает баланс пользователя
        /// </summary>
        public async Task<decimal> GetBalanceAsync(Guid userId, string currencyTypeId)
        {
            return await GetUserBalanceAsync(userId, currencyTypeId);
        }

        /// <summary>
        /// Пополняет баланс пользователя
        /// </summary>
        public async Task<bool> ReplenishmentAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            return await AddTransaction(TransactionType.Replenishment, ammount, userId, currencyTypeId);
        }

        /// <summary>
        /// Выводит средства пользователя
        /// </summary>
        public async Task<bool> WithdrawalAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            if (ammount == 0)
            {
                return false;
            }

            var balance = await GetUserBalanceAsync(userId, currencyTypeId);

            if (balance >= ammount)
            {
                return await AddTransaction(TransactionType.Withdrawal, ammount * -1, userId, currencyTypeId);
            }

            return false;
        }

        /// <summary>
        /// Резервирует средства пользователя
        /// </summary>
        public async Task<bool> ReserveAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            if (ammount == 0)
            {
                return false;
            }

            var balance = await GetUserBalanceAsync(userId, currencyTypeId);

            if (balance >= ammount)
            {
                var result = await AddTransaction(TransactionType.Reserve, ammount * -1, userId, currencyTypeId);

                if (result)
                {
                    await ReCreateReserves(new List<OperationData> { new OperationData { CurrencyTypeId = currencyTypeId, UserId = userId } });
                }

                return result;
            }

            return false;
        }

        /// <summary>
        /// Совершает покупку
        /// </summary>
        /// <returns></returns>
        public async Task<bool> BuyAsync(List<OperationData> operationData)
        {
            if (operationData.Any(o => o.Ammount == 0 || o.BuyAmmount == 0))
            {
                return false;
            }

            try
            {

                foreach (var item in operationData)
                {
                    var reservedTransaction = _transactionRepository.GetList(item.UserId, item.CurrencyTypeId, TransactionType.Reserve);
                    var reservedTransactionSum = await reservedTransaction.SumAsync(s => s.Ammount) * -1;

                    if (reservedTransactionSum >= item.Ammount)
                    {
                        var transactions = new List<Transaction> {
                            new Transaction {
                                Ammount = item.Ammount,
                                CreatedAt = DateTime.UtcNow,
                                CurrencyTypeId = item.CurrencyTypeId.ToUpper(),
                                TransactionType = TransactionType.Reserve,
                                UserId = item.UserId
                            },
                            new Transaction {
                                Ammount = item.Ammount * -1,
                                CreatedAt = DateTime.UtcNow,
                                CurrencyTypeId = item.CurrencyTypeId.ToUpper(),
                                TransactionType = TransactionType.Ask,
                                UserId = item.UserId
                            },
                            new Transaction {
                                Ammount = item.BuyAmmount,
                                CreatedAt = DateTime.UtcNow,
                                CurrencyTypeId = item.BuyCurrencyTypeId.ToUpper(),
                                TransactionType = TransactionType.Bid,
                                UserId = item.UserId
                            }
                        };

                        _transactionRepository.AddRange(transactions);
                    }
                    else
                    {
                        return false;
                    }
                }

                await _unitOfWork.SaveChangesAsync();

                await ReCreateReserves(operationData);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Удаляет резерв
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveReserveAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            if (ammount == 0)
            {
                return false;
            }

            var transactions = await _transactionRepository.GetList(userId, currencyTypeId, TransactionType.Reserve)
                .ToListAsync();
            var transactionsSum = transactions.Sum(t => t.Ammount) * -1;

            if (transactionsSum >= ammount)
            {
                var result = await AddTransaction(TransactionType.Reserve, ammount, userId, currencyTypeId);

                if (result)
                {
                    await ReCreateReserves(new List<OperationData> { new OperationData { CurrencyTypeId = currencyTypeId, UserId = userId } });
                }

                return result;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Методы(private)

        /// <summary>
        /// Получает баланс пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<decimal> GetUserBalanceAsync(Guid userId, string currencyTypeId)
        {
            var result = await _transactionRepository.GetList(userId, currencyTypeId)
                .SumAsync(s => s.Ammount);

            return result;
        }

        /// <summary>
        /// Добавляет транзакцию
        /// </summary>
        /// <returns></returns>
        private async Task<bool> AddTransaction(TransactionType transactionType, decimal ammount, Guid userId, string currencyTypeId)
        {
            try
            {
                var transaction = new Transaction
                {
                    Ammount = ammount,
                    CreatedAt = DateTime.UtcNow,
                    CurrencyTypeId = currencyTypeId.ToUpper(),
                    TransactionType = transactionType,
                    UserId = userId
                };

                _transactionRepository.Add(transaction);

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Группировака резервов
        /// </summary>
        /// <returns></returns>
        private async Task ReCreateReserves(List<OperationData> operationData)
        {
            try
            {
                foreach (var item in operationData)
                {
                    var transactions = await _transactionRepository.GetList(item.UserId, item.CurrencyTypeId, TransactionType.Reserve).ToListAsync();
                    var transactionsSum = transactions.Sum(s => s.Ammount);

                    if (transactionsSum != 0)
                    {
                        var transaction = new Transaction
                        {
                            Ammount = transactionsSum,
                            CreatedAt = DateTime.UtcNow,
                            CurrencyTypeId = item.CurrencyTypeId.ToUpper(),
                            UserId = item.UserId,
                            TransactionType = TransactionType.Reserve
                        };

                        _transactionRepository.Add(transaction);
                    }
                    _transactionRepository.RemoveRange(transactions);
                }

                await _unitOfWork.SaveChangesAsync();
            }
            catch { }
        }

        #endregion
    }
}
