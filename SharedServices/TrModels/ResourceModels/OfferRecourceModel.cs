namespace TrModels.ResourceModels
{
    /// <summary>
    /// Модель предложения
    /// </summary>
    public class OfferRecourceModel
    {
        /// <summary>
        /// Валюта покупки
        /// </summary>
        public string CurrencyFromId { get; set; }

        /// <summary>
        /// Валюта продажи
        /// </summary>
        public string CurrencyToId { get; set; }

        /// <summary>
        /// Объём
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public decimal Price { get; set; }
    }
}
