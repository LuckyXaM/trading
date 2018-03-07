using AutoMapper;
using System;
using TrCurrencies.Data.Models;
using TrModels;

namespace TrCurrencies.Data.Mappings.Profiles
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
            CreateMap<CurrencyXml, Currency>();
            CreateMap<CurrencyPairXml, CurrencyPair>()
                .ForMember(x => x.CurrencyPairId, opt => opt.MapFrom(s => Guid.NewGuid()));
        }

        #endregion Конструкторы
    }
}
