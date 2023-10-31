using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Add
{
    public class AddPurchaseCommand : IRequest<Guid>
    {
        public AddPurchaseCommand()
        {
        }

        public AddPurchaseCommand(DateTime purchaseDate, Guid tourId, int weekCount, Guid clientId)
        {
            PurchaseDate = purchaseDate;
            TourId = tourId;
            WeekCount = weekCount;
            ClientId = clientId;
        }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public Guid TourId { get; set; }
        public int WeekCount { get; set; }
        public Guid ClientId { get; set; }
    }
}
