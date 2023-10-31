using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Update
{
    public class UpdateHotelCommand : IRequest
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; } = default!;
        public HotelClass HotelClass { get; set; }
        public Guid CountryId { get; set; }
    }
}
