using System.Net.Http;
using TrCurrencyClient.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace TrCurrencyClient.Logic
{
    /// <summary>
    /// Клиент валют
    /// </summary>
    public class CurrencyClient : ICurrencyClient
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
        public CurrencyClient(
            IConfiguration configuration
            )
        {
            _client.BaseAddress = new Uri(configuration.GetValue<string>("CurrencyUrl"));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверяет наличие валюты
        /// </summary>
        /// <param name="currencyId">Ид валюты</param>
        /// <returns></returns>
        public async Task<bool> CheckCurrencyAsync(string currencyId)
        {
            var uri = $"api/currency/checkCurrency/{currencyId}";

            var response = await _client.GetAsync(uri);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Проверяет наличие валютной пары
        /// </summary>
        /// <param name="currencyFromId">Ид валюты продажи</param>
        /// <param name="currencyToId">Ид валюты покупки</param>
        /// <returns></returns>
        public async Task<bool> CheckCurrencyPairAsync(string currencyFromId, string currencyToId)
        {
            var uri = $"api/currency/checkCurrencyPair/{currencyFromId}/{currencyToId}";

            var response = await _client.GetAsync(uri);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        #endregion
    }
}
