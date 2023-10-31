using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.All
{
    public class GetAllHotelQuery : IRequest<IEnumerable<HotelListInfo>>
    {
    }
}
