using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrModels.Transaction;
using TrTransactionClient.Interfaces;

namespace TrTransactionClient.Logic
{
    /// <summary>
    /// Клиент транзакций
    /// </summary>
    public class TransactionClient : ITransactionClient
    {
        #region Поля, свойства

        /// <summary>
        /// Http client
        /// </summary>
        private static HttpClient _client { get; } = new HttpClient();

        #endregion

        #region Конструктор

        /// <summary>
        /// Клиент валют
        /// </summary>
        public TransactionClient(
            IConfiguration configuration
            )
        {
            _client.BaseAddress = new Uri(configuration.GetValue<string>("TransactionUrl"));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удаляет резерв
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Ид валюты</param>
        /// <param name="volume">Сумма</param>
        /// <returns></returns>
        public async Task<bool> RemoveReserveAsync(Guid userId, string currencyId, decimal volume)
        {
            var uri = $"api/transaction/unReserve/{userId}/{currencyId}/{volume}";

            var response = await _client.DeleteAsync(uri);
            var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());

            return result;
        }

        /// <summary>
        /// Добавляет резерв
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currencyId">Ид валюты</param>
        /// <param name="volume">Сумма</param>
        /// <returns></returns>
        public async Task<bool> ReserveAsync(Guid userId, string currencyId, decimal volume)
        {
            var uri = $"api/transaction/reserve/{userId}/{currencyId}/{volume}";

            var response = await _client.PostAsync(uri, new StringContent(""));
            var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());

            return result;
        }

        /// <summary>
        /// Совершает покупку
        /// </summary>
        /// <param name="operationData">Данные операции</param>
        /// <returns></returns>
        public async Task<bool> BuyAsync(List<OperationData> operationData)
        {
            var uri = $"api/transaction/buy";
            var content = new StringContent(JsonConvert.SerializeObject(operationData), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Получает баланс пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserBalance>> BalanceAsync(Guid userId)
        {
            var uri = $"/api/transaction/balance/{userId}";

            var response = await _client.GetAsync(uri);
            return JsonConvert.DeserializeObject<List<UserBalance>>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Пополняет баланс пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ReplenishmentAsync(Guid userId, string currencyId, decimal ammount)
        {
            var uri = $"/api/transaction/replenishment/{userId}/{currencyId}/{ammount}";

            var response = await _client.PostAsync(uri, new StringContent(""));
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        #endregion
    }
}
