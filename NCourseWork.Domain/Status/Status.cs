namespace NCourseWork.Domain.Status
{
    public class Status
    {
        public Guid Id { get; set; } = default;
        public string StatusName { get; set; } = null!;
        public double DiscountPercentage { get; set; } = default;
    }
}
