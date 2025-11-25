using System;
using System.Collections.Generic;

namespace HelpdeskDAL;

// Problem entity class representing a problem in the helpdesk system
// Inherits from HelpdeskEntity which includes common properties like Id, Timer
public partial class Problem : HelpdeskEntity
{
    // Getters and Setters for Problem properties
    public string? Description { get; set; }
}
