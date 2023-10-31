using Mapster;
using NCourseWork.Application.Tour.Get;
using NCourseWork.Application.Tour.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Tour.Open
{
    internal class UpdateTourViewInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TourFullInfo, UpdateTourViewInfo>()
                .Map(dest => dest.HotelId, src => src.HotelFullInfo.Id);

            config.NewConfig<UpdateTourViewInfo, UpdateTourCommand>();
        }
    }
}
