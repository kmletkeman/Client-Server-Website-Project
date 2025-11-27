using HelpdeskDAL;
using System.Diagnostics;
using System.Reflection;

namespace HelpdeskViewModels;

// ViewModel for Employee entity
// Used to transfer employee data between the application layers
// Contains properties for employee details and methods to perform CRUD operations
public class EmployeeViewModel
{
    // data access object for Employee entity
    private readonly EmployeeDAO _dao;

    // Getters and Setters for EmployeeViewModel properties
    public string? Title { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Phoneno { get; set; }
    public string? Timer { get; set; }
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public int? Id { get; set; }
    public bool? IsTech { get; set; }
    public string? StaffPicture64 { get; set; }
    
    // Constructor
    public EmployeeViewModel()
    {
        _dao = new EmployeeDAO();
    }

    // Method to find employee using email property
    public async Task GetByEmail()
    {
        try
        {
            Employee emp = await _dao.GetByEmail(Email!);
            Title = emp.Title;
            Firstname = emp.FirstName;
            Lastname = emp.LastName;
            Phoneno = emp.PhoneNo;
            Email = emp.Email;
            Id = emp.Id;
            DepartmentId = emp.DepartmentId;
            if (emp.StaffPicture != null)
            {
                StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
            }
            Timer = Convert.ToBase64String(emp.Timer!);
        }
        catch (NullReferenceException nex)
        {
            Debug.WriteLine(nex.Message);
            Email = "not found";
        }
        catch (Exception ex)
        {
            Email = "not found";
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
    }

    // Method to find employee using phone number property
    public async Task GetByPhoneNumber()
    {
        try
        {
            Employee emp = await _dao.GetByPhoneNumber(Phoneno!);
            Title = emp.Title;
            Firstname = emp.FirstName;
            Lastname = emp.LastName;
            Phoneno = emp.PhoneNo;
            Email = emp.Email;
            Id = emp.Id;
            DepartmentId = emp.DepartmentId;
            if (emp.StaffPicture != null)
            {
                StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
            }
            Timer = Convert.ToBase64String(emp.Timer!);
        }
        catch (NullReferenceException nex)
        {
            Debug.WriteLine(nex.Message);
            Phoneno = "not found";
        }
        catch (Exception ex)
        {
            Phoneno = "not found";
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
    }

    // Method to find employee using Id property
    public async Task GetById()
    {
        try
        {
            Employee emp = await _dao.GetById((int)Id!);
            Title = emp.Title;
            Firstname = emp.FirstName;
            Lastname = emp.LastName;
            Phoneno = emp.PhoneNo;
            Email = emp.Email;
            Id = emp.Id;
            DepartmentId = emp.DepartmentId;
            if (emp.StaffPicture != null)
            {
                StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
            }
            Timer = Convert.ToBase64String(emp.Timer!);
        }
        catch (NullReferenceException nex)
        {
            Debug.WriteLine(nex.Message);
            Id = -1;
        }
        catch (Exception ex)
        {
            Id = -1;
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
    }

    // Method to get all employees and convert them to EmployeeViewModel list
    public async Task<List<EmployeeViewModel>> GetAll()
    {
        List<EmployeeViewModel> allVms = new();
        try
        {
            List<Employee> allEmployees = await _dao.GetAll();
            // we need to convert Employee instance to EmployeeViewModel because
            // the Web Layer isn't aware of the Domain class Employee
            foreach (Employee emp in allEmployees)
            {
                EmployeeViewModel empVm = new()
                {
                    Title = emp.Title,
                    Firstname = emp.FirstName,
                    Lastname = emp.LastName,
                    Phoneno = emp.PhoneNo,
                    Email = emp.Email,
                    Id = emp.Id,
                    DepartmentId = emp.DepartmentId,
                    DepartmentName = emp.Department.DepartmentName,
                    // binary value needs to be stored on client as base64
                    Timer = Convert.ToBase64String(emp.Timer!),
                    StaffPicture64 = emp.StaffPicture != null ? Convert.ToBase64String(emp.StaffPicture) : null
                };
                allVms.Add(empVm);
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

    // Method to Add a new employee using the property values
    public async Task Add()
    {
        Id = -1;
        try
        {
            Employee emp = new()
            {
                Title = Title,
                FirstName = Firstname,
                LastName = Lastname,
                PhoneNo = Phoneno,
                Email = Email,
                DepartmentId = DepartmentId,
                StaffPicture = StaffPicture64 != null ? Convert.FromBase64String(StaffPicture64!) : null
            };
            Id = await _dao.Add(emp);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
    }

    // Method to Update an existing employee using the property values
    public async Task<int> Update()
    {
        int updateStatus;
        try
        {
            Employee emp = new()
            {
                Title = Title,
                FirstName = Firstname,
                LastName = Lastname,
                PhoneNo = Phoneno,
                Email = Email,
                Id = (int)Id!,
                DepartmentId = DepartmentId,
                StaffPicture = StaffPicture64 != null ? Convert.FromBase64String(StaffPicture64!) : null,
                Timer = Convert.FromBase64String(Timer!)
            };
            updateStatus = -1; // start out with a failed state
            updateStatus = Convert.ToInt16(await _dao.Update(emp)); // overwrite status
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
        return updateStatus;
    }

    // Method to Delete an existing employee using the Id property
    public async Task<int> Delete()
    {
        try
        {
            // dao will return # of rows deleted
            return await _dao.Delete(Id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Problem in " + GetType().Name + " " +
            MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            throw;
        }
    }
}