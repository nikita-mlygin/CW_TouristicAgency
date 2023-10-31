using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Add
{
    public class AddHotelCommand : IRequest<Guid>
    {
        public string HotelName { get; set; } = null!;
        public Guid CountryId { get; set; }
        public HotelClass HotelClass { get; set; }
    }
}
