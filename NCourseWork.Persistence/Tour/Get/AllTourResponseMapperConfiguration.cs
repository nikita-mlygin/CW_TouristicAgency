namespace NCourseWork.Persistence.Tour.Get
{
    using Mapster;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class AllTourResponseMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AllTourResponse, Tour>()
                .Map(dest => dest.Hotel, src => src)
                .Map(dest => dest.Hotel.Id, src => src.HotelId)
                .Map(dest => dest.Hotel.Country.Info, src => src)
                .Map(dest => dest.Hotel.Country.Id, src => src.CountryId);
        }
    }
}
