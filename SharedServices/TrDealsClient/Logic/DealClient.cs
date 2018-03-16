using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TrDealsClient.Interfaces;
using TrModels.ResourceModels;

namespace TrDealsClient.Logic
{
    /// <summary>
    /// Клиент сделок
    /// </summary>
    public class DealClient : IDealClient
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
        public DealClient(
            IConfiguration configuration
            )
        {
            _client.BaseAddress = new Uri(configuration.GetValue<string>("DealUrl"));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получает предложения пользователя для валютной пары
        /// </summary>
        /// <returns></returns>
        public async Task<BidAskResourceModel> GetUserOffersAsync(string currencyOneId, string currencyTwoId)
        {
            var uri = $"api/deal/offers/user/{currencyOneId}/{currencyTwoId}";

            var response = await _client.GetAsync(uri);
            var result = JsonConvert.DeserializeObject<BidAskResourceModel>(await response.Content.ReadAsStringAsync());

            return result;

        }

        /// <summary>
        /// Получает предложения для валютной пары
        /// </summary>
        /// <returns></returns>
        public async Task<BidAskResourceModel> GetOffersAsync(string currencyOneId, string currencyTwoId)
        {
            var uri = $"api/deal/offers/{currencyOneId}/{currencyTwoId}";

            var response = await _client.GetAsync(uri);
            var result = JsonConvert.DeserializeObject<BidAskResourceModel>(await response.Content.ReadAsStringAsync());

            return result;

        }

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveOfferAsync(Guid offerId)
        {
            var uri = $"api/deal/offer/{offerId}";

            var response = await _client.DeleteAsync(uri);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());

        }

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddOfferAsync(string currencyFromId, string currencyToId, decimal volume, decimal price)
        {
            var uri = $"api/deal/offer/{currencyFromId}/{currencyToId}/{volume}/{price}";

            var response = await _client.PostAsync(uri, new StringContent(""));
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());

        }

        #endregion
    }
}
