using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Get.ById
{
    internal class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientFullInfo?>
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        public async Task<ClientFullInfo?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await clientRepository.GetClientByIdAsync(request.ClientId);

            if (client is null)
            {
                return null;
            }

            var res = mapper.Map<ClientFullInfo>(client);

            return res;
        }
    }
}
