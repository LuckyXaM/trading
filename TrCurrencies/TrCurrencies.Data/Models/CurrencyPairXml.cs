using System;
using System.Xml.Serialization;

namespace TrCurrencies.Data.Models
{
    /// <summary>
    /// Валютная пара
    /// </summary>
    public class CurrencyPairXml
    {
        /// <summary>
        /// Ид валюты продажи
        /// </summary>
        [XmlAttribute("from")]
        public string CurrencyPairFromId { get; set; }

        /// <summary>
        /// Ид валюты покупки
        /// </summary>
        [XmlAttribute("to")]
        public string CurrencyPairToId { get; set; }
    }
}
