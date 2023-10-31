using MediatR;
using NCourseWork.Application.Client.Get.ById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Get.WithFilter
{
    public class GetPurchaseWithFilterQuery : IRequest<IEnumerable<PurchaseListInfo>>
    {
        public Guid? ClientFilter { get; set; }
        public DateTime? StartDateTimeFilter { get; set; }
        public DateTime? EndDateTimeFilter { get; set; }
    }
}
