namespace TrModels.Transaction
{
    /// <summary>
    /// Баланс пользователя
    /// </summary>
    public class UserBalance
    {
        /// <summary>
        /// Ид валюты
        /// </summary>
        public string CurrencyPairId { get; set; }

        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; }
    }
}
