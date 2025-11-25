using HelpdeskViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace HelpdeskWebsite.Controllers;

// Controller for handling Department-related API requests
// Provides endpoints to retrieve department data
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    // GET: api/Department
    // Endpoint to get all departments
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            DepartmentViewModel viewmodel = new();
            List<DepartmentViewModel> allDepartments = await viewmodel.GetAll();
            return Ok(allDepartments);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
        }
    }
}
