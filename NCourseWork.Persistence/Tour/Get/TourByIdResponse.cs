using Microsoft.Identity.Client;
using NCourseWork.Domain.Hotel;

namespace NCourseWork.Persistence.Tour.Get
{
    internal class TourByIdResponse
    {
        public Guid Id { get; set; }
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; } = null!;
        public HotelClass HotelClass { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string Climate { get; set; } = null!;
    }
}