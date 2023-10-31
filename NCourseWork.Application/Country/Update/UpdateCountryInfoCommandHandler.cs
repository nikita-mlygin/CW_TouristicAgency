namespace NCourseWork.Application.Country.Update
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateCountryInfoCommandHandler : IRequestHandler<UpdateCountryInfoCommand>
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public UpdateCountryInfoCommandHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateCountryInfoCommand request, CancellationToken cancellationToken)
        {
            await countryRepository.UpdateCountryAsync(mapper.Map<Country>(request));
        }
    }
}
