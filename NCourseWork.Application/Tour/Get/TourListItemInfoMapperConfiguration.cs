namespace NCourseWork.Application.Tour.Get
{
    using Mapster;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class TourListItemInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Tour, TourListItemInfo>()
                .Map(dest => dest.HotelId, src => src.Hotel.Id)
                .Map(dest => dest, src => src.Hotel)
                .Map(dest => dest, src => src.Hotel.Country.Info)
                .Map(dest => dest.CountryId, src => src.Hotel.Country.Id);
        }
    }
}
