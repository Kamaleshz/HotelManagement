﻿using System;
using System.Collections.Generic;

namespace Feedback.Models;

public partial class Amenity
{
    public int AmenityId { get; set; }

    public string? AmenityName { get; set; }

    public virtual ICollection<RoomTypeAmenity> RoomTypeAmenities { get; set; } = new List<RoomTypeAmenity>();
}
