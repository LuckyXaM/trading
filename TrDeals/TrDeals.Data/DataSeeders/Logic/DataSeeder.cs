using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrModels;
using TrDeals.Data.DataSeeders.Interfaces;
using TrDeals.Data.Infrastructure.Interfaces;
using TrDeals.Data.Repositories.Interfaces;

namespace TrDeals.Data.DataSeeders.Logic
{
    /// <summary>
    /// Заполнение данными
    /// </summary>
    public class DataSeeder : IDataSeeder
    {
        #region Поля

        /// <summary>
        /// Репозиторий сделок
        /// </summary>
        private readonly IDealRepository _dealRepository;

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Конструктор

        /// <summary>
        /// Заполнение данными
        /// </summary>
        public DataSeeder(
            IDealRepository dealRepository,
            IUnitOfWork unitOfWork
            )
        {
            _dealRepository = dealRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Добавление валют
        /// </summary>
        public async Task SeedCurrencies()
        {
            var actualCurrencies = CurrencyType();

            var currencies = _dealRepository.GetCurrencyTypes(actualCurrencies.Select(c => c.TransactionTypeId));

             _dealRepository.AddCurrencyTypes(actualCurrencies.Where(u => !currencies.ToList().Select(c => c.TransactionTypeId).Contains(u.TransactionTypeId)));

            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region Методы(private)

        /// <summary>
        /// Список актуальных валют
        /// </summary>
        /// <returns></returns>
        private List<CurrencyType> CurrencyType()
        {
            return new List<CurrencyType> {
                new CurrencyType{
                    TransactionTypeId = "BTC",
                    Title = "Биткоин"
                },
                new CurrencyType{
                    TransactionTypeId = "BCH",
                    Title = "Биткоин кэш"
                }
            };
        }

        #endregion
    }
}
