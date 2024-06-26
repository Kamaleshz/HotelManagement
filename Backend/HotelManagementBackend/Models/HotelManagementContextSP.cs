﻿using HotelManagementBackend.DTOs;
using HotelManagementBackend.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelManagementBackend.Models
{
    public partial class HotelManagementContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomDetailDTO>().HasNoKey();
            modelBuilder.Entity<BookingView>().HasNoKey();
            modelBuilder.Entity<DeletionDTO>().HasNoKey();
            modelBuilder.Entity<BookingTimeDTO>().HasNoKey();
            modelBuilder.Entity<RoomTypeView>().HasNoKey();
            modelBuilder.Entity<RoomsView>().HasNoKey();

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

        public async Task<List<RoomTypeView>> GetRoomTypeAvailable(BookingTimeDTO bookingTime)
        {
            var query = $"Exec GetRoomTypes @CheckIn = '{bookingTime.CheckIn}', @Checkout = '{bookingTime.CheckOut}'";
            return await Set<RoomTypeView>().FromSqlRaw(query).ToListAsync();
        }

        public async Task<List<RoomsView>> GetAvailableRooms(BookingTimeDTO bookingTime)
        {
            var query = $"Exec GetAvailableRooms @CheckIn = '{bookingTime.CheckIn}', @Checkout = '{bookingTime.CheckOut}', @RoomTypeId = '{bookingTime.id}'";
            return await Set<RoomsView>().FromSqlRaw(query).ToListAsync();
        }

        public async Task<List<BookingView>> GetBookingDetails()
        {
            var query = "Exec GetBookingDetails";
            return await Set<BookingView>().FromSqlRaw(query).ToListAsync();
        }

        public async Task<List<BookingView>> GetBookingsDetailsByUser(int UserId)
        {
            var query = $"Exec GetBookingDetailsByUser @Userid = '{UserId}'";
            return await Set<BookingView>().FromSqlRaw(query).ToListAsync();
        }

        public async Task<BookingView?> GetBookingsDetailsById(int BookingId)
        {
            var query = $"Exec GetBookingDetailsById @BookingId = '{BookingId}'";
            var booking = await Set<BookingView>().FromSqlRaw(query).ToListAsync();
            return booking.FirstOrDefault();
        }
        public async Task<int> CancelBooking(DeletionDTO Booking)
        {

            string query = "EXEC CancelBooking @BookingId, @ModifiedBy";

            // Parameter for the stored procedure
            SqlParameter idparameter = new SqlParameter("@BookingId", Booking.Id);
            SqlParameter modifiedbyparameter = new SqlParameter("@ModifiedBy", Booking.ModifiedBy);

            // Execute the stored procedure
            return await Database.ExecuteSqlRawAsync(query, idparameter, modifiedbyparameter);
        }

        public async Task<int> CreateBooking(BookingDetailsDTO booking)
        {
            // Your stored procedure call using raw SQL
            string sql = "EXEC CreateBooking @RoomId, @UserId, @CheckIn, @CheckOut, @PeopleCount, @Amount, @CreatedBy, @Success OUTPUT";

            // Parameters for the stored procedure
            SqlParameter[] parameters =
            {
        new SqlParameter("@RoomId", booking.RoomID),
        new SqlParameter("@UserId", booking.UserID),
        new SqlParameter("@CheckIn", booking.CheckIn),
        new SqlParameter("@CheckOut", booking.CheckOut),
        new SqlParameter("@PeopleCount", booking.PeopleCount),
        new SqlParameter("@Amount", booking.Amount),
        new SqlParameter("@CreatedBy", booking.CreatedBy),
        new SqlParameter("@Success", SqlDbType.Int) { Direction = ParameterDirection.Output }
    };

            // Execute the stored procedure
            await Database.ExecuteSqlRawAsync(sql, parameters);

            // Check the value of the output parameter to determine success
            int success = Convert.ToInt32(parameters[7].Value);
            return success;
        }

    }
}
