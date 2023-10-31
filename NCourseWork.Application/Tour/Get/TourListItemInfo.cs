using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get
{
    public class TourListItemInfo
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; } = null!;
    }
}
