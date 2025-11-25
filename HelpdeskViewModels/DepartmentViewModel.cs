using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskViewModels;

// ViewModel for Department entity
// Used to transfer department data between the application layers
// Contains properties for department details and methods to retrieve department data
public class DepartmentViewModel
{
    // Data access object for Department entity
    private readonly DepartmentDAO _dao;

    //Getters and Setters for DepartmentViewModel properties
    public int Id { get; set; }
    public string? DepartmentName { get; set; }
    public string? Timer { get; set; }

    // Constructor
    public DepartmentViewModel()
    {
        _dao = new DepartmentDAO();
    }

    // Method to get all departments and convert them to DepartmentViewModel list
    public async Task<List<DepartmentViewModel>> GetAll()
    {
        // List to hold all DepartmentViewModel instances
        List<DepartmentViewModel> allVms = new();
        try
        {
            // Retrieve all Department entities from the database
            List<Department> allDepartments = await _dao.GetAll();

            // Convert each Department entity to DepartmentViewModel and add to the list
            foreach (Department dep in allDepartments)
            {
                DepartmentViewModel depVm = new()
                {
                    Id = dep.Id,
                    DepartmentName = dep.DepartmentName,
                    // binary value needs to be stored on client as base64
                    Timer = Convert.ToBase64String(dep.Timer!)
                };
                // Add the ViewModel to the list
                allVms.Add(depVm);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return allVms;
    }
}