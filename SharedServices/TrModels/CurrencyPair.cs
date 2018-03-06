using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TrModels
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
        [XmlAttribute("from")]
        public string CurrencyPairFromId { get; set; }

        /// <summary>
        /// Ид валюты покупки
        /// </summary>
        [Required]
        [XmlAttribute("to")]
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
