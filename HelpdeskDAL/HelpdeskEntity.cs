using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL;

// Base class for Helpdesk entities with common properties
// Other entity classes will inherit from this class
public class HelpdeskEntity
{
    // Getters and Setters for common properties
    public int Id { get; set; }
    [Timestamp]
    public byte[]? Timer { get; set; }
}
