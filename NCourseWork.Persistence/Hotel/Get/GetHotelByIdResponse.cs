using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Persistence.Hotel.Get
{
    internal class GetHotelByIdResponse
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; } = null!;
        public int HotelClass { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string Climate { get; set; } = null!;
    }
}
