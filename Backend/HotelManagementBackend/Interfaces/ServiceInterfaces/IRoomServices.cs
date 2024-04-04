using HotelManagementBackend.DTOs;
using HotelManagementBackend.Models;

namespace HotelManagementBackend.Interfaces.ServiceInterfaces
{
    public interface IRoomServices
    {
        public Task<List<RoomDetailDTO>> GetAllRooms();
        public Task<List<RoomDetailDTO>> GetRoomsByType(string roomTypeId, RoomFilterDTO roomFilterDTO);
        public Task<RoomDetailDTO?> GetRoomById(int roomId);
    }
}
