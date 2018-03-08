using System;
using System.ComponentModel.DataAnnotations;

namespace TrModels.Currency
{
    /// <summary>
    /// Валютная пара
    /// </summary>
    public class CurrencyPair
    {
        /// <summary>
        /// Ид валютной пары
        /// </summary>
        [Key]
        public Guid CurrencyPairId { get; set; }

        /// <summary>
        /// Ид валюты продажи
        /// </summary>
        [Required]
        public string CurrencyPairFromId { get; set; }

        /// <summary>
        /// Ид валюты покупки
        /// </summary>
        [Required]
        public string CurrencyPairToId { get; set; }

        /// <summary>
        /// Валюта продажи
        /// </summary>
        public virtual Currency CurrencyPairFrom { get;  set; }

        /// <summary>
        /// Валюта покупки
        /// </summary>
        public virtual Currency CurrencyPairTo { get; set; }
    }
}
