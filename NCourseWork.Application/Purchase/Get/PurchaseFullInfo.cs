using NCourseWork.Application.Client.Get;
using NCourseWork.Application.Hotel.Get;
using NCourseWork.Application.Tour.Get;

namespace NCourseWork.Application.Purchase.Get
{
    public class PurchaseFullInfo
    {
        public Guid Id { get; set; }
        public int WeekCount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public TourFullInfo TourFullInfo { get; set; } = null!;
        public ClientFullInfo ClientFullInfo { get; set; } = null!;
    }
}