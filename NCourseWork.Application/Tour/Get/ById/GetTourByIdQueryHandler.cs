using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get.ById
{
    internal class GetTourByIdQueryHandler : IRequestHandler<GetTourByIdQuery, TourFullInfo?>
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public GetTourByIdQueryHandler(ITourRepository tourRepository, IMapper mapper)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }

        public async Task<TourFullInfo?> Handle(GetTourByIdQuery request, CancellationToken cancellationToken)
        {
            var tour = await tourRepository.GetTourByIdAsync(request.Id);

            return tour is null ? null : mapper.Map<TourFullInfo>(tour);
        }
    }
}
