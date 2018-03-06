using System.Collections.Generic;
using System.Xml.Serialization;
using TrModels;

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
        public List<Currency> Currencies { get; set; }
    }
}
