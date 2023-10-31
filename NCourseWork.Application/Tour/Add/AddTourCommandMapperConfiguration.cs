namespace NCourseWork.Application.Tour.Add
{
    using Mapster;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class AddTourCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddTourCommand, Tour>()
                .Map(dest => dest.Hotel.Id, src => src.HotelId);
        }
    }
}
