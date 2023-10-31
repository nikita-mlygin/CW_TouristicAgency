namespace NCourseWork.Application.Hotel.Get.ById
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class GetHotelByIdQuery : IRequest<HotelFullInfo?>
    {
        public GetHotelByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
