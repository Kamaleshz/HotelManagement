using System;
using System.Collections.Generic;

namespace HotelManagementBackend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? UserEmail { get; set; }

    public byte[]? UserPassword { get; set; }

    public byte[]? HashKey { get; set; }

    public long? UserPhoneNumber { get; set; }

    public int? UserRole { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? UserRoleNavigation { get; set; }
}
