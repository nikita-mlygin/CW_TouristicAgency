using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Get.WithNameFilter
{
    public class GetCountryWithFilterQueryHandler : IRequestHandler<GetCountryWithFilterQuery, IEnumerable<CountryListItemInfo>>
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public GetCountryWithFilterQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CountryListItemInfo>> Handle(GetCountryWithFilterQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<CountryListItemInfo>>(await countryRepository.GetWithFilterAsync(request.NameFilter, request.ClimateNameFilter));
        }
    }
}
