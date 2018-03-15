using AutoMapper;
using TrDeals.Data.Models;
using TrModels.ResourceModels;

namespace TrDeals.Service.Mapping.Profiles
{
    /// <summary>
    /// Конфигурация маппинга
    /// </summary>
    public class MappingProfile : Profile
    {
        #region Конструктор

        /// <summary>
        /// Профиль для маппинга объектов
        /// </summary>
        public MappingProfile()
        {
            CreateMap<OfferRecourceModel, Offer>();
            CreateMap<OfferUserRecourceModel, Offer>();
        }

        #endregion Конструкторы
    }
}
