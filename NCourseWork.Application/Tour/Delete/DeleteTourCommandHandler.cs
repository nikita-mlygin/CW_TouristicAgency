using MediatR;
using NCourseWork.Domain.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Delete
{
    internal class DeleteTourCommandHandler : IRequestHandler<DeleteTourCommand>
    {
        private readonly ITourRepository tourRepository;

        public DeleteTourCommandHandler(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }

        public async Task Handle(DeleteTourCommand request, CancellationToken cancellationToken)
        {
            await tourRepository.DeleteTourAsync(request.Id);
        }
    }
}
