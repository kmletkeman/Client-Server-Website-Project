using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace HelpdeskWebsite.Controllers;

// Controller for handling Employee-related API requests
// Provides endpoints to perform CRUD operations on employee data
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    // GET: api/Employee/{email}
    // Endpoint to get an employee by email
    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            EmployeeViewModel viewmodel = new() { Email = email };
            await viewmodel.GetByEmail();
            return Ok(viewmodel);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
        }
    }

    // PUT: api/Employee
    // Endpoint to update an existing employee
    [HttpPut]
    public async Task<ActionResult> Put(EmployeeViewModel viewmodel)
    {
        try
        {
            int retVal = await viewmodel.Update();
            return retVal switch
            {
                1 => Ok(new { msg = "Employee " + viewmodel.Lastname + " updated!" }),
                -1 => Ok(new { msg = "Employee " + viewmodel.Lastname + " not updated!" }),
                -2 => Ok(new { msg = "Data is stale for " + viewmodel.Lastname + ", Employee not updated!" }),
                _ => Ok(new { msg = "Employee " + viewmodel.Lastname + " not updated!" }),
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
        }
    }

    // GET: api/Employee
    // Endpoint to get all employees
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            EmployeeViewModel viewmodel = new();
            List<EmployeeViewModel> allEmployees = await viewmodel.GetAll();
            return Ok(allEmployees);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
        }
    }

    // POST: api/Employee
    // Endpoint to add a new employee
    [HttpPost]
    public async Task<ActionResult> Post(EmployeeViewModel viewmodel)
    {
        try
        {
            await viewmodel.Add();
            return viewmodel.Id > 1
            ? Ok(new { msg = "Employee " + viewmodel.Lastname + " added!" })
            : Ok(new { msg = "Employee " + viewmodel.Lastname + " not added!" });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
        }
    }

    // DELETE: api/Employee/{id}
    // Endpoint to delete an employee by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            EmployeeViewModel viewmodel = new() { Id = id };
            int result = await viewmodel.Delete();

            return result == 1
            ? Ok(new { msg = "Employee " + id + " deleted!" })
            : Ok(new { msg = "Employee " + id + " not deleted!" });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
        }
    }
}