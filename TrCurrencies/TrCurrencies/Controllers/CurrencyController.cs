using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrCurrencies.Service.Services.Interfaces;
using TrModels.Currency;

namespace TrCurrencies.Controllers
{
    /// <summary>
    /// API для отслеживания валют
    /// </summary>
    [Route("api/currency")]
    [ApiExplorerSettings(GroupName = "currency")]
    public class CurrencyController : Controller
    {
        #region Поля, свойства

        /// <summary>
        /// Сервис отслеживания валют
        /// </summary>
        private readonly ICurrencyService _currencyService;

        #endregion

        #region Конструктор

        /// <summary>
        /// APi для управления транзакциями
        /// </summary>
        public CurrencyController(
            ICurrencyService currencyService
            )
        {
            _currencyService = currencyService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверяет наличие валюты
        /// </summary>
        /// <param name="currencyId">Ид валюты</param>
        /// <returns></returns>
        [HttpGet("checkCurrency/{currencyId}")]
        public async Task<bool> CheckCurrency(string currencyId)
        {
            return await _currencyService.CheckCurrency(currencyId);
        }

        /// <summary>
        /// Проверяет наличие валютной пары
        /// </summary>
        /// <param name="currencyFromId">Ид валюты продажи</param>
        /// <param name="currencyToId">Ид валюты покупки</param>
        /// <returns></returns>
        [HttpGet("checkCurrencyPair/{currencyFromId}/{currencyToId}")]
        public async Task<bool> CheckCurrencyPair(string currencyFromId, string currencyToId)
        {
            return await _currencyService.CheckCurrencyPair(currencyFromId, currencyToId);
        }

        /// <summary>
        /// Получает все валюты
        /// </summary>
        /// <returns></returns>
        [HttpGet("currencies")]
        public async Task<List<Currency>> Currencies()
        {
            return await _currencyService.GetCurrencies();
        }

        /// <summary>
        /// Получает все валютные пары
        /// </summary>
        /// <returns></returns>
        [HttpGet("currencyPairs")]
        public async Task<List<CurrencyPair>> CurrencyPairs()
        {
            return await _currencyService.GetCurrencyPairs();
        }

        #endregion
    }
}