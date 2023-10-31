using NCourseWork.Application.Client.Get;
using NCourseWork.Application.Tour.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Get
{
    public class PurchaseListInfo
    {
        public Guid Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int WeekCount { get; set; }
        public ClientListItemInfo ClientBaseInfo { get; set; } = null!;
        public TourListItemInfo TourBaseInfo { get; set; } = null!;
    }
}
