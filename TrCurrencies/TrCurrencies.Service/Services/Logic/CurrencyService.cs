using System.Collections.Generic;
using System.Threading.Tasks;
using TrCurrencies.Data.Repositories.Interfaces;
using TrCurrencies.Service.Services.Interfaces;
using TrModels;

namespace TrCurrencies.Service.Services.Logic
{
    /// <summary>
    /// Сервис отслеживания валют
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        #region Поля, свойства

        /// <summary>
        /// Репозиторий валют
        /// </summary>
        private readonly ICurrencyRepository _currencyRepository;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис отслеживания валют
        /// </summary>
        public CurrencyService(
            ICurrencyRepository currencyRepository
            )
        {
            _currencyRepository = currencyRepository;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверяет наличие валюты
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckCurrency(string currencyId)
        {
            var currency = await _currencyRepository.GetCurrency(currencyId.ToUpper());

            return currency != null;
        }

        /// <summary>
        /// Проверяет наличие валютной пары
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckCurrencyPair(string currencyFromId, string currencyToId)
        {
            var currencyPair = await _currencyRepository.GetCurrencyPair(currencyFromId.ToUpper(), currencyToId.ToUpper());

            return currencyPair != null;
        }

        /// <summary>
        /// Получает все валюты
        /// </summary>
        /// <returns></returns>
        public async Task<List<Currency>> GetCurrencies()
        {
            var result = default(List<Currency>);

            await Task.Run(() =>
            {
                result = _currencyRepository.GetCurrencies();
            });

            return result;
        }

        /// <summary>
        /// Получает все валютные пары
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurrencyPair>> GetCurrencyPairs()
        {
            var result = default(List<CurrencyPair>);

            await Task.Run(() =>
            {
                result = _currencyRepository.GetCurrencyPairs();
            });

            return result;
        }

        #endregion
    }
}
