using Mapster;
using NCourseWork.Application.Hotel.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Hotel.Add
{
    internal class AddHotelViewInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddHotelViewInfo, AddHotelCommand>()
                .Map(dest => dest.HotelClass, src => src.HotelClass.Value);
        }
    }
}
