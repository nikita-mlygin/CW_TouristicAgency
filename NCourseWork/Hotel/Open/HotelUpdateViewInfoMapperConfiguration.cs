using Mapster;
using NCourseWork.Application.Hotel.Get;
using NCourseWork.Application.Hotel.Update;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Hotel.Open
{
    internal class HotelUpdateViewInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<HotelUpdateViewInfo, UpdateHotelCommand>()
                .Map(dest => dest.HotelName, src => src.Name)
                .Map(dest => dest.HotelClass, src => src.HotelClass.Value);

            config.NewConfig<HotelFullInfo, HotelUpdateViewInfo>()
                .Map(dest => dest.Name, src => src.HotelName)
                .Map(dest => dest.HotelClass, src => new MyComboBoxDataSet<HotelClass>(HotelClassHelper.GetHotelClassName(src.HotelClass), src.HotelClass))
                .Map(dest => dest.CountryId, src => src.Country.Id);
        }
    }
}
