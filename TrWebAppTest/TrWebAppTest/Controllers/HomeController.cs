using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrWebAppTest.Services.Services.Interfaces;

namespace TrWebAppTest.Controllers
{
    /// <summary>
    /// Контроллер по-умолчанию
    /// </summary>
    public class HomeController : Controller
    {
        #region Поля, свойства

        /// <summary>
        /// Сервис веб приложения
        /// </summary>
        private readonly IWebAppTestService _webAppTestService;

        #endregion

        #region Конструктор

        /// <summary>
        /// Контроллер по-умолчанию
        /// </summary>
        public HomeController(
            IWebAppTestService webAppTestService
            )
        {
            _webAppTestService = webAppTestService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Страница предложений
        /// </summary>
        /// <returns></returns>
        [HttpGet("trading/{currencyFromId}/{currencyToId}")]
        public async Task<IActionResult> Trading(string currencyFromId, string currencyToId)
        {
            var result = await _webAppTestService.GetTradingInfoAsync(currencyFromId, currencyToId);

            return View(result);
        }


        #endregion
    }
}