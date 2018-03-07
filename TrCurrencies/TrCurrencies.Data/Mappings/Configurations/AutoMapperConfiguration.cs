using AutoMapper;
using TrCurrencies.Data.Mappings.Profiles;

namespace TrCurrencies.Data.Mappings.Configurations
{
    /// <summary>
    /// Конфигурация маппинга объектов
    /// </summary>
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Конфигурация маппинга объектов
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });
        }
    }
}