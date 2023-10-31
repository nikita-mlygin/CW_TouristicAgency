using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.WithFilter
{
    internal class GetHotelsWithFilterQueryHandler : IRequestHandler<GetHotelsWithFilterQuery, IEnumerable<HotelListInfo>>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public GetHotelsWithFilterQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<HotelListInfo>> Handle(GetHotelsWithFilterQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<HotelListInfo>>(await hotelRepository.GetWithFilterAsync(request.HotelNameFilter, request.CountryFilter, request.HotelClassFilter));
        }
    }
}
