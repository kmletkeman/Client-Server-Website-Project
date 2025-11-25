using HelpdeskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasestudyTests;

// Unit tests for EmployeeViewModel methods to perform CRUD operations
// These tests exercise the ViewModel methods which in turn call the API methods
public class ViewModelTests
{
    // Test method to get Employee by Email
    [Fact]
    public async Task Employee_GetByEmailTest()
    {
        EmployeeViewModel vm = new() { Email = "bs@abc.com" };
        await vm.GetByEmail();
        Assert.NotNull(vm.Firstname);
    }

    // Test method to get Employee by Phone Number
    [Fact]
    public async Task Employee_GetByPhoneNumberTest()
    {
        EmployeeViewModel vm = new() { Phoneno = "(555) 555-5551" };
        await vm.GetByPhoneNumber();
        Assert.NotNull(vm.Firstname);
    }

    // Test method to get Employee by Id
    [Fact]
    public async Task Employee_GetByIdTest()
    {
        EmployeeViewModel vm = new() { Id = 1 };
        await vm.GetById();
        Assert.NotNull(vm.Firstname);
    }

    // Test method to get all Employees
    [Fact]
    public async Task Employee_GetAllTest()
    {
        List<EmployeeViewModel> allEmployeeVms;
        EmployeeViewModel vm = new();
        allEmployeeVms = await vm.GetAll();
        Assert.True(allEmployeeVms.Count > 0);
    }

    // Test method to add a new Employee
    [Fact]
    public async Task Employee_AddTest()
    {
        EmployeeViewModel vm;
        vm = new()
        {
            Title = "Mr.",
            Firstname = "Kevin",
            Lastname = "Letkeman",
            Email = "some@abc.com",
            Phoneno = "(777) 777-7777",
            DepartmentId = 100 // ensure department id is in Department table
        };
        await vm.Add();
        Assert.True(vm.Id > 0);
    }

    // Test method to update an existing Employee
    [Fact]
    public async Task Employee_UpdateTest()
    {
        EmployeeViewModel vm = new() { Phoneno = "(777) 777-7777" };
        await vm.GetByPhoneNumber(); // Employee just added in Add test
        vm.Email = vm.Email == "some@abc.com" ? "kl@abc.com" : "some@abc.com";
        // will be -1 if failed 0 if no data changed, 1 if succcessful
        Assert.True(await vm.Update() == 1);
    }

    // Test method to delete an existing Employee
    [Fact]
    public async Task Employee_DeleteTest()
    {
        EmployeeViewModel vm = new() { Phoneno = "(777) 777-7777" };
        await vm.GetByPhoneNumber(); // Employee just added
        Assert.True(await vm.Delete() == 1); // 1 Employee deleted
    }
}