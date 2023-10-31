namespace NCourseWork.Hotel.Open
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using NCourseWork.Common.Components.ComboBox;
    using NCourseWork.Domain.Hotel;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    internal partial class HotelUpdateViewInfo : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MyComboBoxDataSet<HotelClass>> hotelClasses = new()
        {
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.OneStar), Domain.Hotel.HotelClass.OneStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.TwoStar), Domain.Hotel.HotelClass.TwoStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.ThreeStar), Domain.Hotel.HotelClass.ThreeStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.FourStar), Domain.Hotel.HotelClass.FourStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.FiveStar), Domain.Hotel.HotelClass.FiveStar),
        };

        public string Name { get; set; } = null!;

        [ObservableProperty]
        private MyComboBoxDataSet<HotelClass>? hotelClass;

        public Guid CountryId { get; set; }
    }
}
