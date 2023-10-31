using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Get.ByName
{
    internal class GetClientByNameQueryHandler : IRequestHandler<GetClientByNameQuery, IEnumerable<ClientListItemInfo>>
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        public GetClientByNameQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClientListItemInfo>> Handle(GetClientByNameQuery request, CancellationToken cancellationToken)
        {
            if (request.FirstName is not null && request.LastName is null && request.MiddleName is null)
            {
                return mapper.Map<IEnumerable<ClientListItemInfo>>(await clientRepository.GetClientsWithOneNameFilterAsync(request.FirstName));
            }

            return mapper.Map<IEnumerable<ClientListItemInfo>>(await clientRepository.GetClientsWithNameFilterAsync(request.FirstName, request.LastName, request.MiddleName));
        }
    }
}
