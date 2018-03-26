using System.Collections.Generic;
using System.Threading.Tasks;
using TrModels.Currency;

namespace TrCurrencyClient.Interfaces
{
    /// <summary>
    /// Клиент валют
    /// </summary>
    public interface ICurrencyClient
    {
        /// <summary>
        /// Проверяет наличие валюты
        /// </summary>
        /// <param name="currencyId">Ид валюты</param>
        /// <returns></returns>
        Task<bool> CheckCurrencyAsync(string currencyId);

        /// <summary>
        /// Проверяет наличие валютной пары
        /// </summary>
        /// <param name="currencyFromId">Ид валюты продажи</param>
        /// <param name="currencyToId">Ид валюты покупки</param>
        /// <returns></returns>
        Task<bool> CheckCurrencyPairAsync(string currencyFromId, string currencyToId);

        /// <summary>
        /// Получает все валюты
        /// </summary>
        /// <returns></returns>
        Task<List<Currency>> GetCurrenciesAsync();
    }
}
