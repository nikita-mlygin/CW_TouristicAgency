using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Get.All
{
    internal class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, IEnumerable<CountryListItemInfo>>
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public GetAllCountryQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CountryListItemInfo>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<CountryListItemInfo>>(await countryRepository.GetAll());
        }
    }
}
