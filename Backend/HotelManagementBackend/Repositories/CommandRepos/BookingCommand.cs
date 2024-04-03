using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.RepositoryInterface.CommandInterfaces;
using HotelManagementBackend.Models;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Repositories.CommandRepos
{
    public class BookingCommand : IBookingCommand
    {
        private readonly HotelManagementContext _context;
        public BookingCommand(HotelManagementContext context) 
        {
            _context = context;
        }

        public async Task<BookingView?> CancelBooking(DeletionDTO deletion)
        {
            var Booking = await _context.GetBookingsDetailsById(deletion.Id);
            await _context.CancelBooking(deletion);
            return Booking;
        }

        public async Task<BookingDetailsDTO> CreateBooking(BookingDetailsDTO bookingDetails)
        {
            var Booking = await _context.CreateBooking(bookingDetails);
            return bookingDetails;
        }
    }
}
