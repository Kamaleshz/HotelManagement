using System;
using System.Collections.Generic;

namespace HotelManagementBackend.Models;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string? RoomTypeName { get; set; }

    public string? RoomTypeDescription { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<RoomTypeAmenity> RoomTypeAmenities { get; set; } = new List<RoomTypeAmenity>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
