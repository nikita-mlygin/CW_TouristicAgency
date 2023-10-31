namespace NCourseWork.Application.Client.Delete
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IClientRepository clientRepository;

        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            await clientRepository.DeleteClientAsync(request.Id);
        }
    }
}
