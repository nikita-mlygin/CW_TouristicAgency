using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Update
{
    public class UpdatePurchaseCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public int WeekCount { get; set; }
    }
}
