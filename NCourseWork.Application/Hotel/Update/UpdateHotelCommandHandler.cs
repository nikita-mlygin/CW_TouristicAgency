namespace NCourseWork.Application.Hotel.Update
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Hotel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            await hotelRepository.UpdateHotelAsync(mapper.Map<Hotel>(request));
        }
    }
}
