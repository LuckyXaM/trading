using System.Threading.Tasks;

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
        Task<bool> CheckCurrency(string currencyId);

        /// <summary>
        /// Проверяет наличие валютной пары
        /// </summary>
        /// <param name="currencyFromId">Ид валюты продажи</param>
        /// <param name="currencyToId">Ид валюты покупки</param>
        /// <returns></returns>
        Task<bool> CheckCurrencyPair(string currencyFromId, string currencyToId);
    }
}
