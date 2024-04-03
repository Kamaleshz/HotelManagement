using HotelManagementBackend.DTOs;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Interfaces.ServiceInterfaces
{
    public interface IBookingService
    {
        public Task<List<BookingView>> GetAllBooking();
        public Task<List<BookingView>> GetBookingByPerson(int id);
        public Task<BookingView?> GetBookingById(int id);
        public Task<BookingView?> CancelBooking(DeletionDTO identity);
        public Task<BookingDetailsDTO> CreateBooking(BookingDetailsDTO dto);    
    }
}
