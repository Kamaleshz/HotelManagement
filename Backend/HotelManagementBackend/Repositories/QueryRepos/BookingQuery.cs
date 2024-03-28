using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces;

namespace HotelManagementBackend.Repositories.QueryRepos
{
    public class BookingQuery : IBookingQuery
    {
        public Task<List<BookingDetails>> GetAllBooking()
        {
            throw new NotImplementedException();
        }

        public Task<BookingDetails> GetBookingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookingDetails>> GetBookingByPerson(int id)
        {
            throw new NotImplementedException();
        }
    }
}
