using NCourseWork.Persistence.Client.Get;
using NCourseWork.Persistence.Tour.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Persistence.Purchase.Get
{
    internal class PurchaseListInfoResponse
    {
        public Guid PurchaseId { get; set; }
        public int WeekCount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid ClientId { get; set; }
        public GetClientListItemResponse ClientInfo { get; set; } = null!;
        public Guid TourId { get; set; }
        public AllTourResponse TourInfo { get; set; } = null!;
    }
}
