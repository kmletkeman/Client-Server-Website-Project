using HelpdeskDAL;

namespace CasestudyTests;

// Unit tests for DAO classes to validate CRUD operations, including concurrency and data retrieval.
public class DAOTests
{
    // Get employee by email test
    [Fact]
    public async Task Employee_GetByEmailTest()
    {
        EmployeeDAO dao = new();
        Employee selectedEmployee = await dao.GetByEmail("kl@abc.com");
        Assert.NotNull(selectedEmployee);
    }

    // Get employee by phone number test
    [Fact]
    public async Task Employee_GetByPhoneNumberTest()
    {
        EmployeeDAO dao = new();
        Employee selectedEmployee = await dao.GetByPhoneNumber("(555) 555-5551");
        Assert.NotNull(selectedEmployee);
    }

    // Get employee by ID test
    [Fact]
    public async Task Employee_GetByIdTest()
    {
        EmployeeDAO dao = new();
        Employee selectedEmployee = await dao.GetById(1);
        Assert.NotNull(selectedEmployee);
    }

    // Get all employees test
    [Fact]
    public async Task Employee_GetAllTest()
    {
        EmployeeDAO dao = new();
        List<Employee> allEmployees = await dao.GetAll();
        Assert.True(allEmployees.Count > 0);
    }

    // Add new employee test
    [Fact]
    public async Task Employee_AddTest()
    {
        EmployeeDAO dao = new();
        Employee newEmployee = new()
        {
            FirstName = "Kevin",
            LastName = "Letkeman",
            PhoneNo = "(555) 555-1234",
            Title = "Mr.",
            DepartmentId = 100,
            Email = "kl@abc.com"
        };
        Assert.True(await dao.Add(newEmployee) > 0);
    }

    // Update existing employee test
    [Fact]
    public async Task Employee_UpdateTest()
    {
        EmployeeDAO dao = new();
        Employee? employeeForUpdate = await dao.GetByEmail("kl@abc.com");
        if (employeeForUpdate != null)
        {
            string oldPhoneNo = employeeForUpdate.PhoneNo!;
            string newPhoneNo = oldPhoneNo == "(555) 555-1234" ? "(555) 555-5555" : "(555) 555-1234";
            employeeForUpdate!.PhoneNo = newPhoneNo;
        }
        Assert.True(await dao.Update(employeeForUpdate!) == UpdateStatus.Ok); // 1 indicates the # of rows updated
    }

    // Concurrency test for employee updates
    [Fact]
    public async Task Employee_ConcurrencyTest()
    {
        EmployeeDAO dao1 = new();
        EmployeeDAO dao2 = new();
        Employee employeeForUpdate1 = await dao1.GetByEmail("kl@abc.com");
        Employee employeeForUpdate2 = await dao2.GetByEmail("kl@abc.com");
        if (employeeForUpdate1 != null)
        {
            // get original phone number
            string? oldPhoneNo = employeeForUpdate1.PhoneNo;
            // change phone number to a different value
            string? newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
            // update phone number in first instance
            employeeForUpdate1.PhoneNo = newPhoneNo;
            // first update should succeed
            if (await dao1.Update(employeeForUpdate1) == UpdateStatus.Ok)
            {
                // second update should fail with stale data
                employeeForUpdate2.PhoneNo = "666-666-6668";
                Assert.True(await dao2.Update(employeeForUpdate2) == UpdateStatus.Stale);
            }
            else
                Assert.True(false); // first update failed
        }
        else
            Assert.True(false); // didn't find employee 1
    }

    // Delete employee test
    [Fact]
    public async Task Employee_DeleteTest()
    {
        EmployeeDAO dao = new();
        Employee? selectedEmployee = await dao.GetByEmail("kl@abc.com");
        Assert.True(await dao.Delete(selectedEmployee.Id!) == 1); // 1 indicates the # of rows updated
    }

    [Fact]
    public async Task Employee_LoadPicsTest()
    {
        {
            PicsUtility util = new();
            Assert.True(await util.AddEmployeePicsToDb());
        }
    }
}