namespace NCourseWork.Application.Client.Update
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            await clientRepository.UpdateWithoutStatusAsync(mapper.Map<Client>(request));
        }
    }
}
