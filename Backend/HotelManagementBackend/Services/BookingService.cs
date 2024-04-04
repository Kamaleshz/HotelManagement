using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.RepositoryInterface.CommandInterfaces;
using HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces;
using HotelManagementBackend.Interfaces.ServiceInterfaces;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingQuery _bookingquery;
        private readonly IBookingCommand _bookingcommand;
        public BookingService(IBookingQuery bookingQuery, IBookingCommand bookingCommand)
        {
            _bookingquery = bookingQuery;
            _bookingcommand = bookingCommand;
        }

        public async Task<BookingView?> CancelBooking(DeletionDTO identity)
        {
            var Booking = await _bookingcommand.CancelBooking(identity);
            return Booking;
        }

        public async Task<BookingDetailsDTO> CreateBooking(BookingDetailsDTO booking)
        {
            var book = await _bookingcommand.CreateBooking(booking);
            return book;
        }

        public async Task<List<BookingView>> GetAllBooking()
        {
            var Booking = await _bookingquery.GetAllBooking();
            return Booking;
        }

        public async Task<BookingView?> GetBookingById(int id)
        {
            var booking = await _bookingquery.GetBookingById(id);
            return booking;
        }

        public async Task<List<BookingView>> GetBookingByPerson(int id)
        {
            var Booking = await _bookingquery.GetBookingByPerson(id);
            return Booking;
        }
    }
}
