namespace NCourseWork.Application.Purchase.Delete
{
    using MediatR;
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    internal class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand>
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IClientRepository clientRepository;
        private readonly IClientService clientService;

        public DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IClientRepository clientRepository, IClientService clientService)
        {
            this.purchaseRepository = purchaseRepository;
            this.clientRepository = clientRepository;
            this.clientService = clientService;
        }

        public async Task Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            var client = await clientRepository.GetClientByPurchaseAsync(new Purchase(request.Id));

            await purchaseRepository.RemovePurchaseAsync(request.Id);

            client = await clientService.ChangePurchaseCount(client, -1);
            await clientRepository.UpdateClientAsync(client);
        }
    }
}
