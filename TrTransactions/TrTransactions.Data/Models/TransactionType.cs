namespace TrTransactions.Data.Models
{
    /// <summary>
    /// Тип транзакции
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Пополнение
        /// </summary>
        Replenishment = 1,

        /// <summary>
        /// Вывод
        /// </summary>
        Withdrawal = 2,

        /// <summary>
        /// Коммиссия
        /// </summary>
        //Fee = 3,

        /// <summary>
        /// Продажа
        /// </summary>
        Bid = 4,

        /// <summary>
        /// Покупка
        /// </summary>
        Ask = 5,

        /// <summary>
        /// Бронь
        /// </summary>
        Reserve = 6
    }
}

