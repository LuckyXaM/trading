using System.Threading.Tasks;
using TrCurrencies.Data.Infrastructure.Interfaces;

namespace TrCurrencies.Data.Infrastructure.Logic
{
    /// <summary>
    /// Единица работы с БД
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Поля, свойства

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private TrCurrenciesContext _docflowContext;

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        protected TrCurrenciesContext DataContext
        {
            get { return _docflowContext; }
        }

        #endregion

        #region

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        public UnitOfWork(TrCurrenciesContext docflowContext)
        {
            _docflowContext = docflowContext;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await DataContext.SaveChangesAsync();
        }

        #endregion
    }
}
