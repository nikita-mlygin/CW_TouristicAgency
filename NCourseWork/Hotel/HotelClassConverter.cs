using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NCourseWork.Hotel
{
    [ValueConversion(typeof(HotelClass), typeof(string))]
    internal class HotelClassConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is HotelClass hotelClass ?
            hotelClass switch
            {
                HotelClass.OneStar => "One Star",
                HotelClass.TwoStar => "Two Star",
                HotelClass.ThreeStar => "Three Star",
                HotelClass.FourStar => "Four Star",
                HotelClass.FiveStar => "Five Star",
                _ => null,
            } : null!;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
