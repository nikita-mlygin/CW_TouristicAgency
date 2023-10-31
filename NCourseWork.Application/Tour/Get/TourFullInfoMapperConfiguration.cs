using Mapster;
namespace NCourseWork.Application.Tour.Get
{
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class TourFullInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Tour, TourFullInfo>()
                .Map(dest => dest.HotelFullInfo, src => src.Hotel);
        }
    }
}
