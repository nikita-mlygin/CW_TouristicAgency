using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Get.WithFilter
{
    internal class GetPurchaseWithFilterQueryHandler : IRequestHandler<GetPurchaseWithFilterQuery, IEnumerable<PurchaseListInfo>>
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IMapper mapper;

        public GetPurchaseWithFilterQueryHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            this.purchaseRepository = purchaseRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseListInfo>> Handle(GetPurchaseWithFilterQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<PurchaseListInfo>>(await purchaseRepository.GetWithFilterAsync(request.ClientFilter, request.StartDateTimeFilter, request.EndDateTimeFilter));
        }
    }
}
