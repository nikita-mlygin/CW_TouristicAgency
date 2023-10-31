
namespace NCourseWork.Application.Tour.Update
{
    using Mapster;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class UpdateTourCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateTourCommand, Tour>()
                .Map(dest => dest.Hotel.Id, src => src.HotelId);
        }
    }
}
