using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Get.ById
{
    public class GetPurchaseByIdQuery : IRequest<PurchaseFullInfo?>
    {
        public GetPurchaseByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
