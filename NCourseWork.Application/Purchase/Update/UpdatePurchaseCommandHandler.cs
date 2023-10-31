namespace NCourseWork.Application.Purchase.Update
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand>
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IPurchaseService purchaseService;
        private readonly IMapper mapper;

        public UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IMapper mapper, IPurchaseService purchaseService)
        {
            this.purchaseRepository = purchaseRepository;
            this.mapper = mapper;
            this.purchaseService = purchaseService;
        }

        public async Task Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
        {
            var oldPurchase = await purchaseRepository.GetPurchaseByIdAsync(request.Id) ?? throw new ArgumentException($"Purchase with id {request.Id} not found.", nameof(request));
            mapper.Map(request, oldPurchase);

            var newPurchase = await purchaseService.UpdateCostAndDiscount(oldPurchase);

            await purchaseRepository.UpdatePurchaseAsync(newPurchase);
        }
    }
}
