using System.Collections.Generic;
using TrDeals.Data.Models;
using TrModels;

namespace TrDeals.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public interface IDealRepository
    {
        /// <summary>
        /// Добавляет аск
        /// </summary>
        void AddAsk(Ask ask);

        /// <summary>
        /// Удаляет аск
        /// </summary>
        void RemoveAsk(Ask ask);

        /// <summary>
        /// Получает аски
        /// </summary>
        void GetAsks(decimal ammount);

        /// <summary>
        /// Добавляет бид
        /// </summary>
        void AddBid(Bid bid);

        /// <summary>
        /// Удаляет бид
        /// </summary>
        void RemoveBid(Bid bid);

        /// <summary>
        /// Получает биды
        /// </summary>
        void GetBids(decimal ammount);
    }
}
