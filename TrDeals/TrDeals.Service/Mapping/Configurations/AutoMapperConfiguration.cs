using AutoMapper;
using TrDeals.Service.Mapping.Profiles;

namespace TrDeals.Service.Mapping.Configurations
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
