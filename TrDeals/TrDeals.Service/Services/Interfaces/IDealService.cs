using System;
using System.Threading.Tasks;

namespace TrDeals.Service.Services.Interfaces
{
    /// <summary>
    /// Сервис сделок
    /// </summary>
    public interface IDealService
    {
        /// <summary>
        /// Добавляет предложение
        /// </summary>
        /// <returns></returns>
        Task<bool> AddOfferAsync(Guid userId, string currencyFromId, string currencyToId, decimal ammount, decimal course);

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveOfferAsync(Guid askId, Guid userId);
    }
}
