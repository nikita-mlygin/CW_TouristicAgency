using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get.WithFilter
{
    internal class GetTourWithFilterQueryHandler : IRequestHandler<GetTourWithFilterQuery, IEnumerable<TourListItemInfo>>
    {
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;

        public GetTourWithFilterQueryHandler(ITourRepository tourRepository, IMapper mapper)
        {
            this.tourRepository = tourRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TourListItemInfo>> Handle(GetTourWithFilterQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<TourListItemInfo>>(await tourRepository.GetWithFilterAsync(request.CountryFilter, request.HotelNameFilter, request.HotelClassFilter, request.MinPriceFilter, request.MaxPriceFilter));
        }
    }
}
