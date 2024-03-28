using HotelManagementBackend.DTOs;

namespace HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces
{
    public interface IBookingQuery
    {
        public Task<List<BookingDetails>> GetAllBooking();
        public Task<List<BookingDetails>> GetBookingByPerson(int id);
        public Task<BookingDetails> GetBookingById(int id);
    }
}
