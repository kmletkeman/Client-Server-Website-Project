using System;
using System.Collections.Generic;

namespace HelpdeskDAL;

// Department entity class representing a department in the helpdesk system
// Inherits from HelpdeskEntity which includes common properties like Id, Timer
public partial class Department : HelpdeskEntity
{
    // Getters and Setters for Department properties
    public string? DepartmentName { get; set; }
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}