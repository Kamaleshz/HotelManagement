using System;
using System.Collections.Generic;

namespace UserManagement.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? RoomId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? Checkout { get; set; }

    public int? PeopleCount { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual Room? Room { get; set; }

    public virtual User? User { get; set; }
}
