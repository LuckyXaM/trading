using System;
using System.Threading.Tasks;
using TrModels.ResourceModels;

namespace TrDealsClient.Interfaces
{
    /// <summary>
    /// Клиент сделок
    /// </summary>
    public interface IDealClient
    {
        /// <summary>
        /// Получает предложения пользователя для валютной пары
        /// </summary>
        /// <returns></returns>
        Task<BidAskResourceModel> GetUserOffersAsync(string currencyOneId, string currencyTwoId);

        /// <summary>
        /// Получает предложения для валютной пары
        /// </summary>
        /// <returns></returns>
        Task<BidAskResourceModel> GetOffersAsync(string currencyOneId, string currencyTwoId);

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveOfferAsync(Guid offerId);

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        /// <returns></returns>
        Task<bool> AddOfferAsync(string currencyFromId, string currencyToId, decimal volume, decimal price);
    }
}
