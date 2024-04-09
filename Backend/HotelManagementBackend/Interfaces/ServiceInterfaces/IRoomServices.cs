using HotelManagementBackend.DTOs;
using HotelManagementBackend.Models;
using HotelManagementBackend.ViewModels;

namespace HotelManagementBackend.Interfaces.ServiceInterfaces
{
    public interface IRoomServices
    {
        public Task<List<RoomDetailDTO>> GetAllRooms();
        public Task<List<RoomDetailDTO>> GetRoomsByType(string roomTypeId, RoomFilterDTO roomFilterDTO);
        public Task<RoomDetailDTO?> GetRoomById(int roomId);
        public Task<List<RoomTypeView>> GetAvailableRooms(BookingTimeDTO bookingTimeDTO);
        public Task<List<RoomsView>> GetAllAvailableRooms(BookingTimeDTO bookingTimeDTO);
    }
}
