using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.All
{
    internal class GetAllHotelQueryHandler : IRequestHandler<GetAllHotelQuery, IEnumerable<HotelListInfo>>
    {
        private readonly IMapper mapper;
        private readonly IHotelRepository hotelRepository;

        public GetAllHotelQueryHandler(IMapper mapper, IHotelRepository hotelRepository)
        {
            this.mapper = mapper;
            this.hotelRepository = hotelRepository;
        }

        public async Task<IEnumerable<HotelListInfo>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<HotelListInfo>>(await hotelRepository.GetAllAsync());
        }
    }
}
