using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrCurrencyClient.Interfaces;
using TrDealsClient.Interfaces;
using TrModels.Transaction;
using TrTransactionClient.Interfaces;
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

        /// <summary>
        /// Параметры конфигурации
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Клиент валют
        /// </summary>
        private readonly ICurrencyClient _currencyClient;

        /// <summary>
        /// Клиент управлениями транзакций
        /// </summary>
        private readonly ITransactionClient _transactionClient;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис веб приложения
        /// </summary>
        public WebAppTestService(
            IDealClient dealClient,
            IConfiguration configuration,
            ICurrencyClient currencyClient,
            ITransactionClient transactionClient
            )
        {
            _dealClient = dealClient;
            _configuration = configuration;
            _currencyClient = currencyClient;
            _transactionClient = transactionClient;
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
                BidAskUserResourceModel = bidAskUserResourceModel,
                CurrencyFromId = currencyFromId,
                CurrencyToId = currencyToId,
                DealUri = _configuration.GetValue<string>("LocalDealUri")
            };
        }

        /// <summary>
        /// Получает инфо о балансе пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<UserBalanceViewModel> GetOfficeInfoAsync()
        {
            var result = new UserBalanceViewModel();
            result.TradingUri = _configuration.GetValue<string>("LocalTransactionUri");
            result.UserId = Guid.Parse(_configuration.GetValue<string>("UserId"));
            result.UserBalance = new List<UserBalance>();

            var currencies = await _currencyClient.GetCurrenciesAsync();
            var userBalance = await _transactionClient.BalanceAsync(result.UserId);
            foreach (var item in currencies.OrderBy(c => c.CurrencyId))
            {
                var balance = userBalance.Any(b => b.CurrencyPairId == item.CurrencyId)
                    ? userBalance.FirstOrDefault(b => b.CurrencyPairId == item.CurrencyId).Balance
                    : 0;
                result.UserBalance.Add(new UserBalance { CurrencyPairId = item.CurrencyId.ToUpper(), Balance = balance });
            }

            return result;
        }

        #endregion
    }
}
