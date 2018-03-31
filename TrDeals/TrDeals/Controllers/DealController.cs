using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrDeals.Data.Models;
using TrDeals.Service.Services.Interfaces;
using TrModels.ResourceModels;

namespace TrDeals.Controllers
{
    /// <summary>
    /// API для сделок
    /// </summary>
    [Route("api/deal")]
    [ApiExplorerSettings(GroupName = "deal")]
    public class DealController : Controller
    {
        #region Поля, свойства

        /// <summary>
        /// Сервис сделок
        /// </summary>
        private readonly IDealService _dealService;

        /// <summary>
        /// Ид пользователя
        /// </summary>
        private Guid _userId;

        #endregion

        #region Конструктор

        /// <summary>
        /// API для сделок
        /// </summary>
        public DealController(
            IDealService dealService
            )
        {
            _dealService = dealService;

            //TODO авторизация пользователя
            _userId = Guid.Parse("ee01b062-7613-4eb5-a2aa-c614a977a51f");
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удаляет предложение
        /// </summary>
        /// <param name="offerId">Ид предложения</param>
        [HttpDelete("offer/{offerId}")]
        public async Task<bool> RemoveOfferAsync(Guid offerId)
        {
            return await _dealService.RemoveOfferAsync(offerId, _userId);
        }

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        /// <param name="currencyFromId">Ид валюты продажи</param>
        /// <param name="currencyToId">Ид валюты покупки</param>
        /// <param name="volume"> Сумма</param>
        /// <param name="price">Курс</param>
        /// <param name="offerType">Тип предложения</param>
        /// <returns></returns>
        [HttpPost("offer/{currencyFromId}/{currencyToId}/{volume}/{price}/{offerType}")]
        public async Task<bool> AddOfferAsync(string currencyFromId, string currencyToId, decimal volume, decimal price, OfferType offerType)
        {
            return await _dealService.AddOfferAsync(_userId, currencyFromId, currencyToId, volume, price, offerType);
        }

        /// <summary>
        /// Получает предложения пользователя по валютной паре
        /// </summary>
        /// <returns></returns>
        [HttpGet("offers/user/{currencyOneId}/{currencyTwoId}")]
        public async Task<BidAskResourceModel> GetUserOffersAsync(string currencyOneId, string currencyTwoId)
        {
            return await _dealService.GetOffersAsync(_userId, currencyOneId, currencyTwoId);
        }

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("offers/{currencyOneId}/{currencyTwoId}")]
        public async Task<BidAskResourceModel> GetOffersAsync(string currencyOneId, string currencyTwoId)
        {
            return await _dealService.GetOffersAsync(currencyOneId, currencyTwoId);
        }

        #endregion
    }
}