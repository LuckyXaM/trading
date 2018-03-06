using System.Linq;
using TrDeals.Data.Models;
using TrDeals.Data.Repositories.Interfaces;

namespace TrDeals.Data.Repositories.Logic
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public class DealRepository : IDealRepository
    {
        #region Свойства

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly TrDealsContext _context;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public DealRepository(TrDealsContext context)
        {
            _context = context;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет аск
        /// </summary>
        public void AddAsk(Ask ask)
        {
            _context.Asks.Add(ask);
        }

        /// <summary>
        /// Удаляет аск
        /// </summary>
        public void RemoveAsk(Ask ask)
        {
            _context.Asks.Remove(ask);
        }

        /// <summary>
        /// Получает аски
        /// </summary>
        public void GetAsks(decimal ammount)
        {
            _context.Asks.Where(a => a.Ammount >= ammount);
        }

        /// <summary>
        /// Добавляет бид
        /// </summary>
        public void AddBid(Bid bid)
        {
            _context.Bids.Add(bid);
        }

        /// <summary>
        /// Удаляет бид
        /// </summary>
        public void RemoveBid(Bid bid)
        {
            _context.Bids.Remove(bid);
        }

        /// <summary>
        /// Получает биды
        /// </summary>
        public void GetBids(decimal ammount)
        {
            _context.Bids.Where(a => a.Ammount <= ammount);
        }

        #endregion
    }
}
