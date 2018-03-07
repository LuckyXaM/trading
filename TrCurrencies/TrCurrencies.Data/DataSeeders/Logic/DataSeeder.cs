using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrCurrencies.Data.DataSeeders.Interfaces;
using TrCurrencies.Data.Infrastructure.Interfaces;
using TrCurrencies.Data.Models;
using TrCurrencies.Data.Repositories.Interfaces;
using TrModels;

namespace TrCurrencies.Data.DataSeeders.Logic
{
    /// <summary>
    /// Заполнение данными
    /// </summary>
    public class DataSeeder : IDataSeeder
    {
        #region Поля, свойства

        /// <summary>
        /// Репозиторий валют
        /// </summary>
        private readonly ICurrencyRepository _сurrencyRepository;

        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Валюты
        /// </summary>
        private List<Currency> _currencies;

        /// <summary>
        /// Валютные пары
        /// </summary>
        private List<CurrencyPair> _currencyPairs;

        #endregion

        #region Конструктор

        /// <summary>
        /// Заполнение данными
        /// </summary>
        public DataSeeder(
            ICurrencyRepository сurrencyRepository,
            IConfiguration configuration,
            IUnitOfWork unitOfWork
            )
        {
            _сurrencyRepository = сurrencyRepository;
            _unitOfWork = unitOfWork;
            ParseCurrencies(configuration.GetValue<string>("Storage:Currencies"));
            ParseCurrencyPairs(configuration.GetValue<string>("Storage:CurrencyPairs"));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Заполняет данными
        /// </summary>
        public async Task SeedDataAsync()
        {
            var currencies = _сurrencyRepository.GetCurrencies().Select(c => c.CurrencyId.ToUpper());
            var newCurrencies = _currencies.Distinct()
                .Where(c => !currencies.Contains(c.CurrencyId.ToUpper()) && !string.IsNullOrEmpty(c.CurrencyId));
            _сurrencyRepository.AddRangeCurrency(newCurrencies);

            await _unitOfWork.SaveChangesAsync();

            currencies = _сurrencyRepository.GetCurrencies().Select(c => c.CurrencyId.ToUpper());
            var currencyPairs = _сurrencyRepository.GetCurrencyPairs();
            var newCurrencyPairs = _currencyPairs.Distinct()
                .Where(cp => currencies.Contains(cp.CurrencyPairToId.ToUpper()) && currencies.Contains(cp.CurrencyPairFromId.ToUpper())
                    && !currencyPairs.Any(c => c.CurrencyPairFromId.ToUpper() == cp.CurrencyPairFromId.ToUpper() && c.CurrencyPairToId.ToUpper() == cp.CurrencyPairToId.ToUpper()));
            _сurrencyRepository.AddRangeCurrencyPairs(newCurrencyPairs);

            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region Методы(private)

        /// <summary>
        /// Парсит валюты
        /// </summary>
        private void ParseCurrencies(string path)
        {
            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof(CurrencyFormat));
                var currencyFormat = (CurrencyFormat)serializer.Deserialize(reader);
                currencyFormat.Currencies.ForEach(c => c.CurrencyId = c.CurrencyId.ToUpper());
                _currencies = Mapper.Map<List<CurrencyXml>, List<Currency>>(currencyFormat.Currencies);
            }
        }

        /// <summary>
        /// Парсит валютные пары
        /// </summary>
        private void ParseCurrencyPairs(string path)
        {
            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof(CurrencyPairFormat));
                var currencyPairFormat = (CurrencyPairFormat)serializer.Deserialize(reader);
                currencyPairFormat.CurrencyPairs.ForEach(c => c.CurrencyPairFromId = c.CurrencyPairFromId.ToUpper());
                currencyPairFormat.CurrencyPairs.ForEach(c => c.CurrencyPairToId = c.CurrencyPairToId.ToUpper());
                _currencyPairs = Mapper.Map<List<CurrencyPairXml>, List<CurrencyPair>>(currencyPairFormat.CurrencyPairs);
            }
        }

        #endregion
    }
}
