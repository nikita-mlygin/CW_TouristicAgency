using NCourseWork.Application.Hotel.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get
{
    public class TourFullInfo
    {
        public Guid Id { get; set; }
        public double PricePerWeek { get; set; }
        public HotelFullInfo HotelFullInfo { get; set; } = null!;
    }
}
