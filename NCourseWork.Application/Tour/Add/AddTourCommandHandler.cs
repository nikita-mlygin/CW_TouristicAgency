using MapsterMapper;
namespace NCourseWork.Application.Tour.Add
{
    using MediatR;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class AddTourCommandHandler : IRequestHandler<AddTourCommand, Guid>
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public AddTourCommandHandler(ITourRepository tourRepository, IMapper mapper)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddTourCommand request, CancellationToken cancellationToken)
        {
            var newTour = mapper.Map<Tour>(request);

            newTour.Id = Guid.NewGuid();

            await tourRepository.AddTourAsync(newTour);

            return newTour.Id;
        }
    }
}
