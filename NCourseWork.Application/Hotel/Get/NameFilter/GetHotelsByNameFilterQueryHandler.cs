using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.NameFilter
{
    internal class GetHotelsByNameFilterQueryHandler : IRequestHandler<GetHotelsByNameFilterQuery, IEnumerable<HotelListInfo>>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public GetHotelsByNameFilterQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<HotelListInfo>> Handle(GetHotelsByNameFilterQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<HotelListInfo>>(await hotelRepository.GetHotelByNameFilter(request.Filter));
        }
    }
}
