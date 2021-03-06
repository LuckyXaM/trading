﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrModels.Transaction;
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
        #region Поля, свойства

        /// <summary>
        /// Сервис управления транзакциями
        /// </summary>
        private readonly ITransactionService _transactionService;

        #endregion

        #region Конструктор

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

        #region Методы

        /// <summary>
        /// Получает баланс пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Валюта</param>
        /// <returns></returns>
        [HttpGet("balance/{userId}/{currencyId}")]
        public async Task<decimal> GetBalanceAsync(Guid userId, string currencyId)
        {
            return await _transactionService.GetBalanceAsync(userId, currencyId);
        }

        /// <summary>
        /// Получает баланс пользователя по всем валютам
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        [HttpGet("balance/{userId}")]
        public async Task<List<UserBalance>> GetBalanceAllAsync(Guid userId)
        {
            return await _transactionService.GetBalanceAsync(userId);
        }

        /// <summary>
        /// Пополняет баланс пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Валюта</param>
        /// <param name="volume">Сумма</param>
        /// <returns></returns>
        [HttpPost("replenishment/{userId}/{currencyId}/{volume}")]
        public async Task<bool> ReplenishmentAsync(Guid userId, string currencyId, decimal volume)
        {
            return await _transactionService.ReplenishmentAsync(userId, currencyId, volume);
        }

        /// <summary>
        /// Резервирует средства пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Валюта</param>
        /// <param name="volume">Сумма</param>
        /// <returns></returns>
        [HttpPost("reserve/{userId}/{currencyId}/{volume}")]
        public async Task<bool> ReserveAsync(Guid userId, string currencyId, decimal volume)
        {
            return await _transactionService.ReserveAsync(userId, currencyId, volume);
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
        /// <param name="currencyId">Валюта</param>
        /// <param name="volume">Сумма</param>
        /// <returns></returns>
        [HttpDelete("unReserve/{userId}/{currencyId}/{volume}")]
        public async Task<bool> RemoveReserveAsync(Guid userId, string currencyId, decimal volume)
        {
            return await _transactionService.RemoveReserveAsync(userId, currencyId, volume);
        }

        #endregion
    }
}