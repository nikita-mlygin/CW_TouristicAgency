using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.NameFilter
{
    public class GetHotelsByNameFilterQuery : IRequest<IEnumerable<HotelListInfo>>
    {
        public GetHotelsByNameFilterQuery()
        {
        }

        public GetHotelsByNameFilterQuery(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; set; } = null!;
    }
}
