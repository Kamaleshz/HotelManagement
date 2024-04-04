using HotelManagementBackend.DTOs;
using HotelManagementBackend.Exceptions;
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
            var result = await _context.CancelBooking(deletion);
            if (result == 0)
            {
                throw new NullException("Booking Doesnt Exist");
            }
            return Booking;
        }

        public async Task<BookingDetailsDTO> CreateBooking(BookingDetailsDTO bookingDetails)
        {
            var Booking = await _context.CreateBooking(bookingDetails);
            if (Booking == 0)
            {
                throw new InvalidSqlException("Booking was Unsuccessful!");
            }
            return bookingDetails;
        }
    }
}
