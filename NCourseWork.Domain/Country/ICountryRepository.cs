namespace NCourseWork.Domain.Country
{
    using System.Collections;

    public interface ICountryRepository
    {
        Task<Country?> GetCountryByIdAsync(Guid countryId);
        Task<IEnumerable<Country>> GetWithFilterAsync(string? nameFilter, string? climateNameFilter);
        Task AddCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);
        Task DeleteCountryAsync(Guid countryId);
        Task<IEnumerable<Country>> GetAll();
    }

}
