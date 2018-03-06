using System;
using System.ComponentModel.DataAnnotations;
using TrModels;

namespace TrDeals.Data.Models
{
    /// <summary>
    /// Аск
    /// </summary>
    public class Ask
    {
        /// <summary>
        /// Ид аска
        /// </summary>
        [Key]
        public Guid AskId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Ид валюты продажи
        /// </summary>
        [Required]
        public string CurrencyFromId { get; set; }

        /// <summary>
        /// Ид валюты покупки
        /// </summary>
        [Required]
        public string CurrencyToId { get; set; }

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
