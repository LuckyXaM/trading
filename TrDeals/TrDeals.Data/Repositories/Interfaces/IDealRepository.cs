using System.Collections.Generic;
using TrModels;

namespace TrDeals.Data.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий транзакций
    /// </summary>
    public interface IDealRepository
    {
        /// <summary>
        /// Получает типы валют
        /// </summary>
        IEnumerable<CurrencyType> GetCurrencyTypes(IEnumerable<string> currencyTypes);

        /// <summary>
        /// Добавляет типы валют
        /// </summary>
        void AddCurrencyTypes(IEnumerable<CurrencyType> currencyTypes);
    }
}
