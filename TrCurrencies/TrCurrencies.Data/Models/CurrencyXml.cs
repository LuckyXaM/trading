using System.Xml.Serialization;

namespace TrCurrencies.Data.Models
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class CurrencyXml
    {
        // <summary>
        /// Ид валюты
        /// </summary>
        [XmlAttribute("code")]
        public string CurrencyId { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
