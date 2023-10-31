namespace NCourseWork.Application.Purchase.Add
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Purchase;
    using NCourseWork.Domain.Tour;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal class AddPurchaseCommandHandler : IRequestHandler<AddPurchaseCommand, Guid>
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly ITourRepository tourRepository;
        private readonly IClientRepository clientRepository;
        private readonly IClientService clientService;
        private readonly IPurchaseService purchaseService;
        private readonly IMapper mapper;

        public AddPurchaseCommandHandler(IPurchaseRepository purchaseRepository, ITourRepository tourRepository, IClientRepository clientRepository, IPurchaseService purchaseService, IMapper mapper, IClientService clientService)
        {
            this.purchaseRepository = purchaseRepository;
            this.tourRepository = tourRepository;
            this.clientRepository = clientRepository;
            this.purchaseService = purchaseService;
            this.mapper = mapper;
            this.clientService = clientService;
        }

        public async Task<Guid> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
        {
            var newEntity = mapper.Map<Purchase>(request);
            var client = await clientRepository.GetClientByIdAsync(newEntity.Client.Id) ?? throw new ArgumentException("Client with this id is not founded", nameof(request));

            newEntity.Id = Guid.NewGuid();
            newEntity.Tour = await tourRepository.GetTourByIdAsync(newEntity.Tour.Id) ?? throw new ArgumentException("Tour with this id is not founded", nameof(request));
            newEntity.Client = client;
            newEntity = await purchaseService.UpdateCostAndDiscount(newEntity);

            await purchaseRepository.AddPurchaseAsync(newEntity);

            client = await clientService.ChangePurchaseCount(client, 1);

            await clientRepository.UpdateClientAsync(client);

            return newEntity.Id;
        }
    }
}
