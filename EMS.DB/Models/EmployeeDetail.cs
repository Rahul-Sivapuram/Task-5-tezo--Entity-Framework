using System;
using System.Collections.Generic;
namespace EMS.DB;

public partial class EmployeeDetail
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Dob { get; set; }

    public string EmailId { get; set; } = null!;

    public long MobileNumber { get; set; }

    public string? JoiningDate { get; set; }

    public string? Location { get; set; }

    public string? Role { get; set; }

    public string? Department { get; set; }

    public string Manager { get; set; } = null!;

    public string? Project { get; set; }
}
