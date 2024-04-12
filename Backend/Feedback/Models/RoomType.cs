using System;
using System.Collections.Generic;

namespace Feedback.Models;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string? RoomTypeName { get; set; }

    public string? RoomTypeDescription { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<RoomTypeAmenity> RoomTypeAmenities { get; set; } = new List<RoomTypeAmenity>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
