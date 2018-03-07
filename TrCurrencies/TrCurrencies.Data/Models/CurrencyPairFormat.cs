using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrCurrencies.Data.Models
{
    /// <summary>
    /// Справочник Валютных пар
    /// </summary>
    [XmlRoot("currencyPairs")]
    public class CurrencyPairFormat
    {
        /// <summary>
        /// Валютные пары
        /// </summary>
        [XmlElement("pair")]
        public List<CurrencyPairXml> CurrencyPairs { get; set; }
    }
}
