using System;
using System.Collections.Generic;

namespace UserManagement.Models;

public partial class RoomTypeAmenity
{
    public int RoomTypeAmenityId { get; set; }

    public int? RoomTypeId { get; set; }

    public int? AmenityId { get; set; }

    public virtual Amenity? Amenity { get; set; }

    public virtual RoomType? RoomType { get; set; }
}
