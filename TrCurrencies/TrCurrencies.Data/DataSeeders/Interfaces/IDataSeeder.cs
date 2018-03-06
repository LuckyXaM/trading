using System.Threading.Tasks;

namespace TrCurrencies.Data.DataSeeders.Interfaces
{
    /// <summary>
    /// Заполнение данными
    /// </summary>
    public interface IDataSeeder
    {
        /// <summary>
        /// Заполняет данными
        /// </summary>
        Task SeedDataAsync();
    }
}
