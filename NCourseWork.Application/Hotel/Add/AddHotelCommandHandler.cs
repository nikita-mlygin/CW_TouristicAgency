namespace NCourseWork.Application.Hotel.Add
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Hotel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;


    internal class AddHotelCommandHandler : IRequestHandler<AddHotelCommand, Guid>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public AddHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddHotelCommand request, CancellationToken cancellationToken)
        {
            var newHotel = mapper.Map<Hotel>(request);
            newHotel.Id = Guid.NewGuid();
            await hotelRepository.AddHotelAsync(newHotel);
            return newHotel.Id;
        }
    }
}
