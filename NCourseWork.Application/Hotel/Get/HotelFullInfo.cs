using NCourseWork.Application.Country.Get;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get
{
    public class HotelFullInfo
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; } = null!;
        public HotelClass HotelClass { get; set; }
        public CountryFullInfo Country { get; set; } = null!;
    }
}
