using HotelManagementBackend.DTOs;
using HotelManagementBackend.Exceptions;
using HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces;
using HotelManagementBackend.Models;

namespace HotelManagementBackend.Repositories.QueryRepos
{
    public class RoomQuery : IRoomQuery
    {
        private readonly HotelManagementContext _context;
        public RoomQuery(HotelManagementContext context)
        {
            _context = context; 
        }

        public async Task<List<RoomDetailDTO>> GetAllRooms()
        {
            var rooms = await _context.GetAllRooms();
            if (rooms == null || rooms.Count == 0)
            {
                throw new NullException("No rooms Available");
            }
            return rooms;
        }

        public async Task<RoomDetailDTO?> GetRoomById(int roomId)
        {
            var room = await _context.GetRoomById(roomId);
            if (room == null)
            {
                throw new NullException("This Room could not be found");
            }
            return room;
        }

        public async Task<List<RoomDetailDTO>> GetRoomsByType(string roomTypeId, RoomFilterDTO roomFilterDTO)
        {
            var rooms = await _context.GetRoomsByType(roomTypeId, roomFilterDTO);
            if (rooms == null || rooms.Count == 0)
            {
                throw new NullException("No rooms available for the selected options");
            }
            return rooms;
        }

    }
}
