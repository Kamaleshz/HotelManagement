using System;
using System.Collections.Generic;

namespace HotelManagementBackend.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int? RoomNumber { get; set; }

    public int? RoomTypeId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomType? RoomType { get; set; }
}
