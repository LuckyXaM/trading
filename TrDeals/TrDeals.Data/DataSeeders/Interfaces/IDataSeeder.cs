using System.Threading.Tasks;

namespace TrDeals.Data.DataSeeders.Interfaces
{
    /// <summary>
    /// Заполнение данными
    /// </summary>
    public interface IDataSeeder
    {
        /// <summary>
        /// Добавление валют
        /// </summary>
        Task SeedCurrencies();
    }
}
