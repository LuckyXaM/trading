using System;

namespace TrModels.Transaction
{
    /// <summary>
    /// Данные операций
    /// </summary>
    public class OperationData
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Валюта продажи
        /// </summary>
        public string CurrencyId { get; set; }

        /// <summary>
        /// Сумма продажи
        /// </summary>
        public decimal Ammount { get; set; }

        /// <summary>
        /// Валюта покупки
        /// </summary>
        public string BuyCurrencyId { get; set; }

        /// <summary>
        /// Сумма покупки
        /// </summary>
        public decimal BuyAmmount { get; set; }
    }
}
