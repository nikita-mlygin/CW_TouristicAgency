namespace NCourseWork.Application.Country.Add
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    internal class AddCountryCommandHandler : IRequestHandler<AddCountryCommand, Guid>
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public AddCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var newCountry = new Country
            {
                Info = mapper.Map<CountryInfo>(request),
                Id = Guid.NewGuid()
            };

            await countryRepository.AddCountryAsync(newCountry);

            return newCountry.Id;
        }
    }
}
