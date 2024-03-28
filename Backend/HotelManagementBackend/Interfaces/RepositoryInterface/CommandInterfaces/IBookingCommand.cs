using HotelManagementBackend.DTOs;

namespace HotelManagementBackend.Interfaces.RepositoryInterface.CommandInterfaces
{
    public interface IBookingCommand
    {
        public Task<BookingDetails> CreateBooking(BookingDetails bookingDetails);
        public Task<BookingDetails> CancelBooking(int id);
    }
}
