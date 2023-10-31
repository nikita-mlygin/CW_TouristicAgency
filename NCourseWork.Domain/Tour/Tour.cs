namespace NCourseWork.Domain.Tour
{
    using NCourseWork.Domain.Hotel;


    public class Tour
    {
        public Guid Id { get; set; } = default;
        public double PricePerWeek { get; set; } = default;
        public Hotel Hotel { get; set; } = null!;
    }

}
