using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Get.ById
{
    internal class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, PurchaseFullInfo?>
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IMapper mapper;

        public GetPurchaseByIdQueryHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            this.purchaseRepository = purchaseRepository;
            this.mapper = mapper;
        }

        public async Task<PurchaseFullInfo?> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await purchaseRepository.GetPurchaseByIdAsync(request.Id);

            return response is null ? null : mapper.Map<PurchaseFullInfo>(response);
        }
    }
}
