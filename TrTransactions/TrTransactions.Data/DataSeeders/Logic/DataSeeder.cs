using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrModels;
using TrTransactions.Data.DataSeeders.Interfaces;
using TrTransactions.Data.Infrastructure.Interfaces;
using TrTransactions.Data.Models;
using TrTransactions.Data.Repositories.Interfaces;

namespace TrTransactions.Data.DataSeeders.Logic
{
    /// <summary>
    /// Заполнение данными
    /// </summary>
    public class DataSeeder : IDataSeeder
    {
        #region Поля

        /// <summary>
        /// Репозиторий транзакций
        /// </summary>
        private readonly ITransactionRepository _transactionRepository;

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
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork
            )
        {
            _transactionRepository = transactionRepository;
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

            var currencies = _transactionRepository.GetCurrencyTypes(actualCurrencies.Select(c => c.TransactionTypeId));

             _transactionRepository.AddCurrencyTypes(actualCurrencies.Where(u => !currencies.ToList().Select(c => c.TransactionTypeId).Contains(u.TransactionTypeId)));

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
