using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get.WithFilter
{
    public class GetTourWithFilterQuery : IRequest<IEnumerable<TourListItemInfo>>
    {
        public Guid? CountryFilter { get; set; }
        public string? HotelNameFilter { get; set; }
        public HotelClass? HotelClassFilter { get; set; }
        public int? MinPriceFilter { get; set; }
        public int? MaxPriceFilter { get; set; }
    }
}
