using MediatR;
using NCourseWork.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Delete
{
    internal class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IHotelRepository hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            await hotelRepository.DeleteHotelAsync(request.Id);
        }
    }
}
