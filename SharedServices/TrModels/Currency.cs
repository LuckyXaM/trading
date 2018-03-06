using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TrModels
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Ид валюты
        /// </summary>
        [Key]
        [XmlAttribute("code")]
        public string CurrencyId { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
