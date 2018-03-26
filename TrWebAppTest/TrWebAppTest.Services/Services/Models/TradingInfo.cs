using TrModels.ResourceModels;

namespace TrWebAppTest.Services.Services.Models
{
    /// <summary>
    /// Информация о сделках в рамках валютной пары
    /// </summary>
    public class TradingInfo
    {
        /// <summary>
        /// Биды и аски
        /// </summary>
        public BidAskResourceModel BidAskResourceModel { get; set; }

        /// <summary>
        /// Биды и аски пользователя
        /// </summary>
        public BidAskResourceModel BidAskUserResourceModel { get; set; }

        /// <summary>
        /// Ид валюты покупки
        /// </summary>
        public string CurrencyToId { get; set; }

        /// <summary>
        /// Ид валюты продажи
        /// </summary>
        public string CurrencyFromId { get; set; }

        /// <summary>
        /// Uri tr-deals
        /// </summary>
        public string DealUri { get; set; }
    }
}
