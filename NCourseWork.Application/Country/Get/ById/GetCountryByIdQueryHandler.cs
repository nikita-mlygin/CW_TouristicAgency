using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Get.ById
{
    internal class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryFullInfo?>
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public GetCountryByIdQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<CountryFullInfo?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await countryRepository.GetCountryByIdAsync(request.Id);

            return res is null ? null : mapper.Map<CountryFullInfo>(res);
        }
    }
}
