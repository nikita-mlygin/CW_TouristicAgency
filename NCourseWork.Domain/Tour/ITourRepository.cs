namespace NCourseWork.Domain.Tour
{
    using NCourseWork.Domain.Country;
    using NCourseWork.Domain.Hotel;

    public interface ITourRepository
    {
        Task<Tour?> GetTourByIdAsync(Guid tourId);
        Task<IEnumerable<Tour>> GetByCountryAsync(Country country);
        Task<IEnumerable<Tour>> GetAll();
        Task AddTourAsync(Tour tour);
        Task UpdateTourAsync(Tour tour);
        Task DeleteTourAsync(Guid tourId);
        Task<IEnumerable<Tour>> GetWithFilterAsync(Guid? countryFilter, string? hotelNameFilter, HotelClass? hotelClassFilter, int? minValueFilter, int? maxValueFilter);
    }

}
