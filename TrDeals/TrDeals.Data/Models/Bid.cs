using System;
using System.ComponentModel.DataAnnotations;
using TrModels;

namespace TrDeals.Data.Models
{
    /// <summary>
    /// Бид
    /// </summary>
    public class Bid
    {
        /// <summary>
        /// Ид бида
        /// </summary>
        [Key]
        public Guid AskId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        public virtual CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// Ид валюты
        /// </summary>
        [Required]
        public string CurrencyTypeId { get; set; }

        /// <summary>
        /// Ид пользователя
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        [Required]
        public decimal Ammount { get; set; }
    }
}
