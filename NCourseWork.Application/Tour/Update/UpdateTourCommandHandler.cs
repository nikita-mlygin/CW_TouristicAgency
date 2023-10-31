namespace NCourseWork.Application.Tour.Update
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand>
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public UpdateTourCommandHandler(ITourRepository tourRepository, IMapper mapper)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateTourCommand request, CancellationToken cancellationToken)
        {
            await tourRepository.UpdateTourAsync(mapper.Map<Tour>(request));
        }
    }
}
