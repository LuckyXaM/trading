using System;

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
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public virtual CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// Ид валюты
        /// </summary>
        public string CurrencyTypeId { get; set; }

        /// <summary>
        /// Ид пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public double Ammount { get; set; }

        /// <summary>
        /// Тип транзакции
        /// </summary>
        public TransactionType TransactionType { get; set; }
    }
}
