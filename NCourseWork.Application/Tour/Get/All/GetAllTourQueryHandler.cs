using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get.All
{
    internal class GetAllTourQueryHandler : IRequestHandler<GetAllTourQuery, IEnumerable<TourListItemInfo>>
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public GetAllTourQueryHandler(ITourRepository tourRepository, IMapper mapper)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TourListItemInfo>> Handle(GetAllTourQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<TourListItemInfo>>(await tourRepository.GetAll());
        }
    }
}
