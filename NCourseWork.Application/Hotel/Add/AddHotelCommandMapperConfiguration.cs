
namespace NCourseWork.Application.Hotel.Add
{
    using Mapster;
    using NCourseWork.Domain.Hotel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class AddHotelCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddHotelCommand, Hotel>()
                .Map(dest => dest.Country.Id, src => src.CountryId);
        }
    }
}
