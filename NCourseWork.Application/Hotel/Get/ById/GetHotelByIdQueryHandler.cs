using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Get.ById
{
    internal class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelFullInfo?>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public GetHotelByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<HotelFullInfo?> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await hotelRepository.GetHotelByIdAsync(request.Id);

            return res is null ? null : mapper.Map<HotelFullInfo?>(res);
        }
    }
}
