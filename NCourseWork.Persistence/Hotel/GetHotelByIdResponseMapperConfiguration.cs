namespace NCourseWork.Persistence.Hotel
{
    using Mapster;
    using NCourseWork.Domain.Hotel;
    using NCourseWork.Persistence.Hotel.Get;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class GetHotelByIdResponseMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetHotelByIdResponse, Hotel>()
                .Map(dest => dest.Country.Id, src => src.CountryId)
                .Map(dest => dest.Country.Info, src => src);
        }
    }
}
