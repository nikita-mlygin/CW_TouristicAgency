using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Hotel.Add
{
    internal class AddHotelViewInfo
    {
        public string HotelName { get; set; } = null!;
        public Guid CountryId { get; set; }
        public MyComboBoxDataSet<HotelClass> HotelClass { get; set; } = null!;
    }
}
