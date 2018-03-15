using System.Threading.Tasks;
using TrWebAppTest.Services.Services.Models;

namespace TrWebAppTest.Services.Services.Interfaces
{
    /// <summary>
    /// Сервис веб приложения
    /// </summary>
    public interface IWebAppTestService
    {
        /// <summary>
        /// Получает информацию о сделках в рамках валютной пары
        /// </summary>
        /// <returns></returns>
        Task<TradingInfo> GetTradingInfoAsync(string currencyFromId, string currencyToId);
    }
}
