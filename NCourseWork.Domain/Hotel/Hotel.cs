namespace NCourseWork.Domain.Hotel
{
    using NCourseWork.Domain.Country;

    public class Hotel
    {
        public Guid Id { get; set; } = default;
        public string HotelName { get; set; } = null!;
        public HotelClass HotelClass { get; set; } = default;
        public Country Country { get; set; } = null!;
    }

}
