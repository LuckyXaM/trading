using System.Threading.Tasks;

namespace TrDeals.Data.Infrastructure.Interfaces
{
    /// <summary>
    /// Единица работы с БД
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        Task SaveChangesAsync();
    }
}
