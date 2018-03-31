using System;
using System.ComponentModel.DataAnnotations;
namespace TrDeals.Data.Models
{
    /// <summary>
    /// Предложение
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// Ид предложения
        /// </summary>
        [Key]
        public Guid OfferId { get; set; }

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
        public decimal Volume { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Тип предложения
        /// </summary>
        [Required]
        public OfferType OfferType { get; set; }
    }
}
