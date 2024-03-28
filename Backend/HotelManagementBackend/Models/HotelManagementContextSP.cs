using HotelManagementBackend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementBackend.Models
{
    public partial class HotelManagementContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomDetailDTO>().HasNoKey();
        }

        public async Task<List<RoomDetailDTO>> GetAllRooms()
        {
            var query = "Exec GetRoomDetails";
            return await Set<RoomDetailDTO>().FromSqlRaw(query).ToListAsync();
        }

        public async Task<List<RoomDetailDTO>> GetRoomsByType(string roomTypeIds, RoomFilterDTO roomFilterDTO)
        {
            var query = $"Exec GetRoomDetailsByType @RoomTypeIds = '{roomTypeIds}', @MinPrice = '{roomFilterDTO.MinPrice}', @MaxPrice = '{roomFilterDTO.MaxPrice}', @AmenityIds = '{roomFilterDTO.AmenityIds}'";
            return await Set<RoomDetailDTO>().FromSqlRaw(query).ToListAsync();
        }

        public async Task<RoomDetailDTO?> GetRoomById(int id)
        {
            var query = $"Exec GetRoomDetailsById @RoomId = '{id}'";
            var rooms =  await Set<RoomDetailDTO>().FromSqlRaw(query).ToListAsync();
            return rooms.FirstOrDefault();

        }
    }
}
