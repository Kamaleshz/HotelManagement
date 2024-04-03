using HotelManagementBackend.DTOs;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Interfaces.RepositoryInterface.CommandInterfaces
{
    public interface IBookingCommand
    {
        public Task<BookingDetailsDTO> CreateBooking(BookingDetailsDTO bookingDetails);
        public Task<BookingView?> CancelBooking(DeletionDTO id);
    }
}
