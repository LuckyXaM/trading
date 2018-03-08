using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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

        #endregion
    }
}