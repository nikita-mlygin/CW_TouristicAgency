namespace NCourseWork.Domain.Hotel
{
    public interface IHotelRepository
    {
        Task<Hotel?> GetHotelByIdAsync(Guid hotelId);
        Task<IEnumerable<Hotel>> GetHotelByNameFilter(string filter);
        Task AddHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(Guid hotelId);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<IEnumerable<Hotel>> GetWithFilterAsync(string? nameFilter, Guid? countryFilter, HotelClass? hotelClassFilter);
    }

}
