using System;

namespace TrTransactions.Data.Models
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
        public string CurrencyTypeId { get; set; }

        /// <summary>
        /// Сумма продажи
        /// </summary>
        public decimal Ammount { get; set; }

        /// <summary>
        /// Валюта покупки
        /// </summary>
        public string BuyCurrencyTypeId { get; set; }

        /// <summary>
        /// Сумма покупки
        /// </summary>
        public decimal BuyAmmount { get; set; }
    }
}
