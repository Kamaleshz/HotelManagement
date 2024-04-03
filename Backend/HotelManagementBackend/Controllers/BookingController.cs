using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.ServiceInterfaces;
using HotelManagementBackend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<List<BookingView>> GetBookingDetails()
        {
            var Bookings = await _bookingService.GetAllBooking();
            return Bookings;
        }

        [HttpGet]
        public async Task<List<BookingView>> GetBookingDetailsByPerson(int id)
        {
            var Bookings = await _bookingService.GetBookingByPerson(id);
            return Bookings;
        }

        [HttpGet]
        public async Task<BookingView?> GetBookingDetailsById(int id)
        {
            var Bookings = await _bookingService.GetBookingById(id);
            return Bookings;
        }

        [HttpPut]
        public async Task<BookingView?> CancelBooking(DeletionDTO identity)
        {
            var Booking = await _bookingService.CancelBooking(identity);
            return Booking;
        }

        [HttpPost]
        public async Task<BookingDetailsDTO> CreateBooking(BookingDetailsDTO booking)
        {
            var book = await _bookingService.CreateBooking(booking);
            return book;
        }
    }
}
