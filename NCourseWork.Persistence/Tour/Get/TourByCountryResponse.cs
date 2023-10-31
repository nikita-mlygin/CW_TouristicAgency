namespace NCourseWork.Persistence.Tour.Get
{
    internal class TourByCountryResponse
    {
        public Guid Id { get; set; }
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set;}
        public string HotelName { get; set; } = null!;
    }
}