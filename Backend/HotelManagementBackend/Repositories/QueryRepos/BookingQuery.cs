using HotelManagementBackend.Exceptions;
using HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces;
using HotelManagementBackend.Models;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Repositories.QueryRepos
{
    public class BookingQuery : IBookingQuery
    {
        private readonly HotelManagementContext _context;

        public BookingQuery(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<List<BookingView>> GetAllBooking()
        {
            var Booking = await _context.GetBookingDetails();
            if (Booking.Count == 0)
            {
                throw new NullException("No Rooms Booked");
            }
            return Booking;
        }

        public async Task<BookingView?> GetBookingById(int id)
        {
            var booking = await _context.GetBookingsDetailsById(id);
            if (booking == null)
            {
                throw new NullException("Booking Not found");
            }
            return booking;
        }

        public async Task<List<BookingView>> GetBookingByPerson(int id)
        {
            var booking = await _context.GetBookingsDetailsByUser(id);
            if (booking.Count == 0)
            {
                throw new NullException("No Rooms Booked");
            }
            return booking;
        }
    }
}
