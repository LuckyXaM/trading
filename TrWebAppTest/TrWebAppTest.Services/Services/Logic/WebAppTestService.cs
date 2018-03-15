using TrDealsClient.Interfaces;
using TrWebAppTest.Services.Services.Interfaces;

namespace TrWebAppTest.Services.Services.Logic
{
    /// <summary>
    /// Сервис веб приложения
    /// </summary>
    public class WebAppTestService: IWebAppTestService
    {
        #region Поля, свойства

        /// <summary>
        /// Клиент сделок
        /// </summary>
        private readonly IDealClient _dealClient;

        #endregion

        #region Конструктор

        /// <summary>
        /// Сервис веб приложения
        /// </summary>
        public WebAppTestService(
            IDealClient dealClient
            )
        {
            _dealClient = dealClient;
        }

        #endregion

        #region Методы

        #endregion
    }
}
