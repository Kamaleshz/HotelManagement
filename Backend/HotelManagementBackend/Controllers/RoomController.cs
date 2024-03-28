using HotelManagementBackend.DTOs;
using HotelManagementBackend.Interfaces.ServiceInterfaces;
using HotelManagementBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomService;
        public RoomController(IRoomServices roomService)
        {
            _roomService = roomService;
        }
        [HttpGet]
        public async Task<List<RoomDetailDTO>> GetAllRooms()
        {
            var Rooms = await _roomService.GetAllRooms();   
            return Rooms;
        }

        [HttpGet]
        public async Task<RoomDetailDTO?> GetRoomById(int roomId)
        {
            var room = await _roomService.GetRoomById(roomId);  
            return room;
        }

        [HttpGet]
        public async Task<List<RoomDetailDTO>> GetRoomsByType(string roomTypeId, [FromQuery] RoomFilterDTO roomFilterDTO)
        {
            var Rooms = await _roomService.GetRoomsByType(roomTypeId, roomFilterDTO);
            return Rooms;
        }
    }
}
