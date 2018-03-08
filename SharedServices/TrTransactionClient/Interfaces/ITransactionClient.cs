using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrModels.Transaction;

namespace TrTransactionClient.Interfaces
{
    public interface ITransactionClient
    {
        /// <summary>
        /// Удаляет резерв
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Ид валюты</param>
        /// <param name="ammount">Сумма</param>
        /// <returns></returns>
        Task<bool> RemoveReserveAsync(Guid userId, string currencyId, decimal ammount);

        /// <summary>
        /// Добавляет резерв
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Ид валюты</param>
        /// <param name="ammount">Сумма</param>
        /// <returns></returns>
        Task<bool> ReserveAsync(Guid userId, string currencyId, decimal ammount);

        /// <summary>
        /// Совершает покупку
        /// </summary>
        /// <param name="operationData">Данные операции</param>
        /// <returns></returns>
        Task<bool> BuyAsync(List<OperationData> operationData);
    }
}
