namespace NCourseWork.Domain.Country
{
    public class Country
    {
        public Country()
        {
        }

        public Guid Id { get; set; } = default;
        public CountryInfo Info { get; set; } = null!;
    }
}
