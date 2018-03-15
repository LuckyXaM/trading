using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrDeals.Data.Models;
using TrModels.ResourceModels;

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

        /// <summary>
        /// Получает предложения пользователя для валютной пары
        /// </summary>
        /// <returns></returns>
        Task<BidAskUserResourceModel> GetOffersAsync(Guid userId, string currencyOneId, string currencyTwoId);

        /// <summary>
        /// Получает предложения для валютной пары
        /// </summary>
        /// <returns></returns>
        Task<BidAskResourceModel> GetOffersAsync(string currencyOneId, string currencyTwoId);
    }
}
