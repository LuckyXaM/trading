using System.ComponentModel.DataAnnotations;

namespace TrModels.Currency
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
        public string CurrencyId { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
