using System;
using System.Collections.Generic;
using TrModels.Transaction;

namespace TrWebAppTest.Services.Services.Models
{
    /// <summary>
    /// Вью модель баланса пользователя
    /// </summary>
    public class UserBalanceViewModel
    {
        /// <summary>
        /// Балансы пользователя
        /// </summary>
        public List<UserBalance> UserBalance { get; set; }

        /// <summary>
        /// Внешний uri для управление транзакциями
        /// </summary>
        public string TradingUri { get; set; }

        /// <summary>
        /// Ид пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
