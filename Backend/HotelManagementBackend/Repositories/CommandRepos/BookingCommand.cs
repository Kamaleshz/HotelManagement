using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.RepositoryInterface.CommandInterfaces;

namespace HotelManagementBackend.Repositories.CommandRepos
{
    public class BookingCommand : IBookingCommand
    {
        public Task<BookingDetails> CancelBooking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookingDetails> CreateBooking(BookingDetails bookingDetails)
        {
            throw new NotImplementedException();
        }
    }
}
