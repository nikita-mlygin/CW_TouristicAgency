using Microsoft.Identity.Client;
using NCourseWork.Domain.Hotel;

namespace NCourseWork.Persistence.Purchase.Get
{
    internal class PurchaseGetFullResponse
    {
        public Guid Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int WeekCount { get; set; }
        public Guid ClientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Guid StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public double DiscountPercentage { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public Guid TourId { get; set; }
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; } = null!;
        public HotelClass HotelClass { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string Climate { get; set; } = null!;
    }
}