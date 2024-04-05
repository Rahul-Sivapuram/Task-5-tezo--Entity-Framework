using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DB;
namespace EMS.DAL;

public class EmployeeFilter
{
    public Location? Location { get; set; }
    public Role? JobTitle { get; set; }
    public Department? Department { get; set; }
    public Manager? Manager { get; set; }
    public Project? Project { get; set; }
    public string? EmployeeName { get; set; }
}
