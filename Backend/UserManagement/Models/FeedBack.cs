using System;
using System.Collections.Generic;

namespace UserManagement.Models;

public partial class FeedBack
{
    public int FeedBackId { get; set; }

    public string? Review { get; set; }

    public int? Rating { get; set; }

    public int? BookingId { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Booking? Booking { get; set; }
}
