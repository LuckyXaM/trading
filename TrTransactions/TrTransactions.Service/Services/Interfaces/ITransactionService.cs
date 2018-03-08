using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrModels.Transaction;

namespace TrTransactions.Service.Services.Interfaces
{
    /// <summary>
    /// Сервис для управления транзакциями
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Получает баланс пользователя
        /// </summary>
        Task<decimal> GetBalanceAsync(Guid userId, string currencyTypeId);

        /// <summary>
        /// Пополняет баланс пользователя
        /// </summary>
        Task<bool> ReplenishmentAsync(Guid userId, string currencyTypeId, decimal ammount);

        /// <summary>
        /// Резервирует средства пользователя
        /// </summary>
        Task<bool> ReserveAsync(Guid userId, string currencyTypeId, decimal ammoun);

        /// <summary>
        /// Совершает покупку
        /// </summary>
        Task<bool> BuyAsync(List<OperationData> operationData);

        /// <summary>
        /// Удаляет резерв
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveReserveAsync(Guid userId, string currencyTypeId, decimal ammount);
    }
}
