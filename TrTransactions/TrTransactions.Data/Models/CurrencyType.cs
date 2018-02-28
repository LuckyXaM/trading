using System.ComponentModel.DataAnnotations;

namespace TrTransactions.Data.Models
{
    /// <summary>
    /// Тип валюты
    /// </summary>
    public class CurrencyType
    {
        /// <summary>
        /// Ид валюты
        /// </summary>
        [Key]
        public string TransactionTypeId { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        public string Title { get; set; }
    }
}
