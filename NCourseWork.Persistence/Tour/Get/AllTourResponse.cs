using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Persistence.Tour.Get
{
    internal class AllTourResponse
    {
        public Guid Id { get; set; }
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; } = null!;
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
    }
}
