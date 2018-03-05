using System.Threading.Tasks;
using TrDeals.Data.Infrastructure.Interfaces;

namespace TrDeals.Data.Infrastructure.Logic
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
        private TrDealsContext _docflowContext;

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        protected TrDealsContext DataContext
        {
            get { return _docflowContext; }
        }

        #endregion

        #region

        /// <summary>
        /// Единица работы с БД
        /// </summary>
        public UnitOfWork(TrDealsContext docflowContext)
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
