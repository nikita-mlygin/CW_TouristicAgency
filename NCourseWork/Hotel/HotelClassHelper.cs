using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Hotel
{
    public static class HotelClassHelper
    {
        private readonly static IDictionary<HotelClass, string> hotelClassNames = new Dictionary<HotelClass, string>()
        {
            { HotelClass.OneStar, "One Star" },
            { HotelClass.TwoStar, "Two Star" },
            { HotelClass.ThreeStar, "Three Star" },
            { HotelClass.FourStar, "Four Star" },
            { HotelClass.FiveStar, "Five Star" },
        };

        public static string GetHotelClassName(HotelClass hotelClass)
        {
            if (hotelClassNames.TryGetValue(hotelClass, out var res))
            {
                return  res;
            }

            throw new NotImplementedException("No name for this class");
        }
    }
}
