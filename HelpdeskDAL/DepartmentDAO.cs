using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL;

// Department Data Access Object for interacting with Department entities in the database
// Provides methods to retrieve department data
public class DepartmentDAO
{
    // Generic repository for Department entities
    readonly IRepository<Department> _repo;

    // Constructor
    public DepartmentDAO()
    {
        _repo = new HelpdeskRepository<Department>();
    }

    // Method to get all Department entities from the database
    public async Task<List<Department>> GetAll()
    {
        List<Department> allDepartments;
        try
        {
            // Retrieve all departments using the repository method GetAll
            allDepartments = await _repo.GetAll();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return allDepartments;
    }
}
