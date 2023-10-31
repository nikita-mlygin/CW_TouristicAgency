using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get
{
    public class HotelListInfo
    {
        public HotelListInfo()
        {
        }

        public HotelListInfo(Guid id, string hotelName)
        {
            Id = id;
            HotelName = hotelName;
        }

        public Guid Id { get; set; }
        public string HotelName { get; set; } = null!;
    }
}
