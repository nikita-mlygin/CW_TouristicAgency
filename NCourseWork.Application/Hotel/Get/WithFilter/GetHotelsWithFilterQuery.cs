using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.WithFilter
{
    public class GetHotelsWithFilterQuery : IRequest<IEnumerable<HotelListInfo>>
    {
        public string? HotelNameFilter { get; set; }
        public HotelClass? HotelClassFilter { get; set; }
        public Guid? CountryFilter { get; set; }
    }
}
