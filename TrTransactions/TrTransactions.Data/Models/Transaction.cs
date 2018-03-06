using System;
using System.ComponentModel.DataAnnotations;

namespace TrTransactions.Data.Models
{
    /// <summary>
    /// Транзакция
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Ид транзакции
        /// </summary>
        [Key]
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Ид валюты
        /// </summary>
        [Required]
        public string CurrencyId { get; set; }

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

        /// <summary>
        /// Тип транзакции
        /// </summary>
        [Required]
        public TransactionType TransactionType { get; set; }
    }
}
