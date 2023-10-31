namespace NCourseWork.Application.Tour.Get.ByCountry
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Country;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class GetToursByCountryQueryHandler : IRequestHandler<GetToursByCountryQuery, IEnumerable<TourListItemInfo>>
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public GetToursByCountryQueryHandler(ITourRepository tourRepository, IMapper mapper)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TourListItemInfo>> Handle(GetToursByCountryQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<TourListItemInfo>>(await tourRepository.GetByCountryAsync(mapper.Map<Country>(request)));
        }
    }
}
