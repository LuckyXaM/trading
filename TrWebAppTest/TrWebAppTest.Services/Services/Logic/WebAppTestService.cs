using System.Threading.Tasks;
using TrDealsClient.Interfaces;
using TrWebAppTest.Services.Services.Interfaces;
using TrWebAppTest.Services.Services.Models;

namespace TrWebAppTest.Services.Services.Logic
{
    /// <summary>
    /// Сервис веб приложения
    /// </summary>
    public class WebAppTestService: IWebAppTestService
    {
        #region Поля, свойства

        /// <summary>
        /// Клиент сделок
        /// </summary>
        private readonly IDealClient _dealClient;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис веб приложения
        /// </summary>
        public WebAppTestService(
            IDealClient dealClient
            )
        {
            _dealClient = dealClient;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получает информацию о сделках в рамках валютной пары
        /// </summary>
        /// <returns></returns>
        public async Task<TradingInfo> GetTradingInfoAsync(string currencyFromId, string currencyToId)
        {
            var bidAskResourceModel = await _dealClient.GetOffersAsync(currencyFromId, currencyToId);
            var bidAskUserResourceModel = await _dealClient.GetUserOffersAsync(currencyFromId, currencyToId);

            return new TradingInfo
            {
                BidAskResourceModel = bidAskResourceModel,
                BidAskUserResourceModel = bidAskUserResourceModel
            };
        }

        #endregion
    }
}
