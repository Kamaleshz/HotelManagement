using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementBackend.Models;

public partial class HotelManagementContext : DbContext
{
    public HotelManagementContext()
    {
    }

    public HotelManagementContext(DbContextOptions<HotelManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<RoomTypeAmenity> RoomTypeAmenities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.AmenityId).HasName("PK__Amenity__842AF50B88A5969D");

            entity.ToTable("Amenity");

            entity.Property(e => e.AmenityName).HasMaxLength(20);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AEDA6BFFF8A");

            entity.ToTable("Booking");

            entity.Property(e => e.CheckIn).HasColumnType("datetime");
            entity.Property(e => e.Checkout).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Booking__RoomId__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserId__5812160E");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.HasKey(e => e.FeedBackId).HasName("PK__FeedBack__E2CB3B87A0C17A24");

            entity.ToTable("FeedBack");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Review).HasMaxLength(100);

            entity.HasOne(d => d.Booking).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__FeedBack__Bookin__59063A47");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A2FA9CD87");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__32863939E2A4236A");

            entity.ToTable("Room");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK__Room__RoomTypeId__59FA5E80");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.RoomTypeId).HasName("PK__RoomType__BCC89631F7362F50");

            entity.ToTable("RoomType");

            entity.Property(e => e.RoomTypeDescription).HasMaxLength(100);
            entity.Property(e => e.RoomTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<RoomTypeAmenity>(entity =>
        {
            entity.HasKey(e => e.RoomTypeAmenityId).HasName("PK__RoomType__A2C9AC415CD42D5D");

            entity.ToTable("RoomTypeAmenity");

            entity.HasOne(d => d.Amenity).WithMany(p => p.RoomTypeAmenities)
                .HasForeignKey(d => d.AmenityId)
                .HasConstraintName("FK__RoomTypeA__Ameni__5BE2A6F2");

            entity.HasOne(d => d.RoomType).WithMany(p => p.RoomTypeAmenities)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK__RoomTypeA__RoomT__5AEE82B9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CEA111789");

            entity.ToTable("User");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .HasConstraintName("FK__User__UserRole__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
