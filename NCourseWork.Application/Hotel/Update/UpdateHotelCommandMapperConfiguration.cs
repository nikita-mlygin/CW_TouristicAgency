


namespace NCourseWork.Application.Hotel.Update
{
    using Mapster;
    using NCourseWork.Domain.Hotel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateHotelCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateHotelCommand, Hotel>()
                .Map(dest => dest.Country.Id, src => src.CountryId);
        }
    }
}
