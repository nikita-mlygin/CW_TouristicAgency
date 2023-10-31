using MediatR;
using NCourseWork.Domain.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Delete
{
    internal class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly ICountryRepository countryRepository;

        public DeleteCountryCommandHandler(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            await countryRepository.DeleteCountryAsync(request.Id);
        }
    }
}
