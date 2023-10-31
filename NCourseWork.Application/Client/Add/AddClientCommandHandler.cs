namespace NCourseWork.Application.Client.Add
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Status;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class AddClientCommandHandler : IRequestHandler<AddClientCommand, Guid>
    {
        private readonly IClientRepository clientRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IMapper mapper;

        public AddClientCommandHandler(IClientRepository clientRepository, IMapper mapper, IStatusRepository statusRepository)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
            this.statusRepository = statusRepository;
        }

        public async Task<Guid> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var client = mapper.Map<Client>(request);
            client.Id = Guid.NewGuid();
            client.Status = await statusRepository.GetFirstStatusIdAsync();

            await clientRepository.AddClientAsync(client);

            return client.Id;
        }
    }
}
