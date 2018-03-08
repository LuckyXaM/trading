using System.Collections.Generic;
using System.Threading.Tasks;
using TrModels.Currency;

namespace TrCurrencies.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий валют
    /// </summary>
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Добавляет валюты
        /// </summary>
        void AddRangeCurrency(IEnumerable<Currency> currencies);

        /// <summary>
        /// Добавляет валютные пары
        /// </summary>
        void AddRangeCurrencyPairs(IEnumerable<CurrencyPair> currencyPairs);

        /// <summary>
        /// Получает валюту
        /// </summary>
        Task<Currency> GetCurrency(string currencyId);

        /// <summary>
        /// Получает валютную пару
        /// </summary>
        Task<CurrencyPair> GetCurrencyPair(string currencyFromId, string currencyToId);

        /// <summary>
        /// Получает валюты
        /// </summary>
        List<Currency> GetCurrencies();

        /// <summary>
        /// Получает валютные пары
        /// </summary>
        List<CurrencyPair> GetCurrencyPairs();
    }
}
