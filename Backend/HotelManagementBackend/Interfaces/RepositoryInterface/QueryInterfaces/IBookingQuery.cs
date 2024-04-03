using HotelManagementBackend.DTOs;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces
{
    public interface IBookingQuery
    {
        public Task<List<BookingView>> GetAllBooking();
        public Task<List<BookingView>> GetBookingByPerson(int id);
        public Task<BookingView?> GetBookingById(int id);
    }
}
