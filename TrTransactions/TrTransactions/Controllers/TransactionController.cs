using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrTransactions.Data.Models;
using TrTransactions.Service.Services.Interfaces;

namespace TrTransactions.Controllers
{
    /// <summary>
    /// APi для управления транзакциями
    /// </summary>
    [Route("api/transaction")]
    [ApiExplorerSettings(GroupName = "transaction")]
    public class TransactionController : Controller
    {
        #region

        /// <summary>
        /// Сервис управления транзакциями
        /// </summary>
        private readonly ITransactionService _transactionService;

        #endregion

        #region

        /// <summary>
        /// APi для управления транзакциями
        /// </summary>
        public TransactionController(
            ITransactionService transactionService
            )
        {
            _transactionService = transactionService;
        }

        #endregion

        #region

        /// <summary>
        /// Получает баланс пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyTypeId">Валюта</param>
        /// <returns></returns>
        [HttpGet("balance/{userId}/{currencyTypeId}")]
        public async Task<decimal> GetBalanceAsync(Guid userId, string currencyTypeId)
        {
            return await _transactionService.GetBalanceAsync(userId, currencyTypeId);
        }

        /// <summary>
        /// Пополняет баланс пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyTypeId">Валюта</param>
        /// <param name="ammount">Сумма</param>
        /// <returns></returns>
        [HttpPost("replenishment/{userId}/{currencyTypeId}/{ammount}")]
        public async Task<bool> ReplenishmentAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            return await _transactionService.ReplenishmentAsync(userId, currencyTypeId, ammount);
        }

        /// <summary>
        /// Резервирует средства пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyTypeId">Валюта</param>
        /// <param name="ammount">Сумма</param>
        /// <returns></returns>
        [HttpPost("reserve/{userId}/{currencyTypeId}/{ammount}")]
        public async Task<bool> ReserveAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            return await _transactionService.ReserveAsync(userId, currencyTypeId, ammount);
        }

        /// <summary>
        /// Совершает покупку
        /// </summary>
        /// <param name="operationData">Данные операции</param>
        /// <returns></returns>
        [HttpPost("buy")]
        public async Task<bool> BuyAsync([FromBody]List<OperationData> operationData)
        {
            return await _transactionService.BuyAsync(operationData);
        }

        /// <summary>
        /// Удаляет резерв
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyTypeId">Валюта</param>
        /// <param name="ammount">Сумма</param>
        /// <returns></returns>
        [HttpPost("unReserve/{userId}/{currencyTypeId}/{ammount}")]
        public async Task<bool> RemoveReserveAsync(Guid userId, string currencyTypeId, decimal ammount)
        {
            return await _transactionService.RemoveReserveAsync(userId, currencyTypeId, ammount);
        }

        #endregion
    }
}