using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL;

// Employee Data Access Object for interacting with Employee entities in the database
// Provides methods to retrieve, add, update, and delete employee data
public class EmployeeDAO
{
    // Generic repository for Employee entities
    readonly IRepository<Employee> _repo;

    // Constructor
    public EmployeeDAO()
    {
        _repo = new HelpdeskRepository<Employee>();
    }

    // Method to get an Employee entity by email from the database
    public async Task<Employee> GetByEmail(string email)
    {
        Employee? selectedEmployee;
        try
        {
            // Retrieve the employee with the specified email using the repository method GetOne
            selectedEmployee = await _repo.GetOne(emp => emp.Email == email);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return selectedEmployee!;
    }

    // Method to get an Employee entity by phone number from the database
    public async Task<Employee> GetByPhoneNumber(string phone)
    {
        Employee? selectedEmployee;
        try
        {
            // Retrieve the employee with the specified phone number using the repository method GetOne
            selectedEmployee = await _repo.GetOne(emp => emp.PhoneNo == phone);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return selectedEmployee!;
    }

    // Method to get an Employee entity by ID from the database
    public async Task<Employee> GetById(int id)
    {
        Employee? selectedEmployee;
        try
        {
            // Retrieve the employee with the specified ID using the repository method GetOne
            selectedEmployee = await _repo.GetOne(emp => emp.Id == id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return selectedEmployee!;
    }

    // Method to get all Employee entities from the database
    public async Task<List<Employee>> GetAll()
    {
        List<Employee> allEmployees;
        try
        {
            // Retrieve all employees using the repository method GetAll
            allEmployees = await _repo.GetAll();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return allEmployees;
    }

    // Method to add a new Employee entity to the database
    public async Task<int> Add(Employee newEmployee)
    {
        try
        {
            // Add the new employee using the repository method Add
            await _repo.Add(newEmployee);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return newEmployee.Id;
    }

    // Method to update an existing Employee entity in the database
    public async Task<UpdateStatus> Update(Employee updatedEmployee)
    {
        UpdateStatus status;
        try
        {
            // Update the employee using the repository method Update
            status = await _repo.Update(updatedEmployee);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return status;
    }

    // Method to delete an Employee entity from the database by ID
    public async Task<int> Delete(int? id)
    {
        // Variable to hold the number of employees deleted
        int employeesDeleted = -1;
        try
        {
            // Delete the employee with the specified ID using the repository method Delete
            employeesDeleted = await _repo.Delete((int)id!);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return employeesDeleted;
    }
}