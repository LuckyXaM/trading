using Microsoft.AspNetCore.Mvc;

namespace TrTransactions.Controllers
{
    /// <summary>
    /// Контроллер по-умолчанию
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Перенапрявляет на страницу с документацией
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return new RedirectResult("~/api-docs");
        }
    }
}