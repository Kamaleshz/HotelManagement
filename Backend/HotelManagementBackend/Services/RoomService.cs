using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.RepositoryInterface.QueryInterfaces;
using HotelManagementBackend.Interfaces.ServiceInterfaces;
using HotelManagementBackend.Repositories.QueryRepos;

namespace HotelManagementBackend.Services
{
    public class RoomService : IRoomServices
    {
        private readonly IRoomQuery _roomQuery;
        public RoomService(IRoomQuery roomQuery)
        {
            _roomQuery = roomQuery;
        }
        public Task<List<RoomDetailDTO>> GetAllRooms()
        {
            var rooms = _roomQuery.GetAllRooms();
            return rooms;
        }

        public Task<RoomDetailDTO?> GetRoomById(int roomId)
        {
            var room = _roomQuery.GetRoomById(roomId);
            return room;
        }

        public Task<List<RoomDetailDTO>> GetRoomsByType(string roomTypeId, RoomFilterDTO roomFilterDTO)
        {
            var rooms = _roomQuery.GetRoomsByType(roomTypeId, roomFilterDTO);
            return rooms;
        }
    }
}
