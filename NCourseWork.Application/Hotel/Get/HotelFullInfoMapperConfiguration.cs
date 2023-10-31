namespace NCourseWork.Application.Hotel.Get
{
    using Mapster;
    using NCourseWork.Domain.Hotel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class HotelFullInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hotel, HotelFullInfo>()
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.Country, src => src.Country.Info)
                .Map(dest => dest.Country.Name, src => src.Country.Info.CountryName);
        }
    }
}
