using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrCurrencies.Data.Models
{
    /// <summary>
    /// Справочник Валют
    /// </summary>
    [XmlRoot("currencies")]
    public class CurrencyFormat
    {
        /// <summary>
        /// Валюты
        /// </summary>
        [XmlElement("currency")]
        public List<CurrencyXml> Currencies { get; set; }
    }
}
