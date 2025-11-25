using System;
using System.Collections.Generic;

namespace HelpdeskDAL;

// Employee entity class representing an employee in the helpdesk system
// Inherits from HelpdeskEntity which includes common properties like Id, Timer
public partial class Employee : HelpdeskEntity
{
    // Getters and Setters for Employee properties
    public string? Title { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public int DepartmentId { get; set; }

    public bool? IsTech { get; set; }

    public byte[]? StaffPicture { get; set; }

    public virtual Department Department { get; set; } = null!;
}