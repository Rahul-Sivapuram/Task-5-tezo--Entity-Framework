namespace EMS.DAL;

public class Employee
{
    public int? Id { get; set; }
    public string? Uid { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DOB { get; set; }
    public string? EmailId { get; set; }
    public long? MobileNumber { get; set; }
    public string? JoiningDate { get; set; }
    public int? LocationId { get; set; }
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
    public bool IsManager { get; set; }
    public int? ManagerId { get; set; }
    public int? ProjectId { get; set; }
}
