using System.Collections.Generic;
using System.Threading.Tasks;
using TrModels.Currency;

namespace TrCurrencies.Service.Services.Interfaces
{
    /// <summary>
    /// Сервис отслеживания валют
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Проверяет наличие валюты
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckCurrency(string currencyId);

        /// <summary>
        /// Проверяет наличие валютной пары
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckCurrencyPair(string currencyFromId, string currencyToId);

        /// <summary>
        /// Получает все валюты
        /// </summary>
        /// <returns></returns>
        Task<List<Currency>> GetCurrencies();

        /// <summary>
        /// Получает все валютные пары
        /// </summary>
        /// <returns></returns>
        Task<List<CurrencyPair>> GetCurrencyPairs();
    }
}
