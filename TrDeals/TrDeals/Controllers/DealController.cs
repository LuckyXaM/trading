﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrDeals.Data.Models;
using TrDeals.Service.Services.Interfaces;

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
        [HttpPost("removeOffer/{offerId}")]
        public async Task<bool> RemoveOfferAsync(Guid offerId)
        {
            return await _dealService.RemoveOfferAsync(offerId, _userId);
        }

        /// <summary>
        /// Добавляет предложение
        /// </summary>
        /// <param name="currencyFromId">Ид валюты продажи</param>
        /// <param name="currencyToId">Ид валюты покупки</param>
        /// <param name="ammount"> Сумма</param>
        /// <param name="course">Курс</param>
        /// <returns></returns>
        [HttpPost("addOffer/{currencyFromId}/{currencyToId}/{ammount}/{course}")]
        public async Task<bool> AddOfferAsync(string currencyFromId, string currencyToId, decimal ammount, decimal course)
        {
            return await _dealService.AddOfferAsync(_userId, currencyFromId, currencyToId, ammount, course);
        }

        /// <summary>
        /// Получает предложения пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("getOffers")]
        public async Task<List<Offer>> GetOffersAsync()
        {
            return await _dealService.GetOffersAsync(_userId);
        }

        #endregion
    }
}