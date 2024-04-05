using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using EMS.DB;
namespace EMS.DAL;

public class DropDownDal : IDropDownDal
{
    private readonly RahulContext _context;

    public DropDownDal(RahulContext context)
    {
        _context = context;
    }

    public List<Location> GetLocations()
    {
        return _context.Locations.ToList() ?? null;
    }

    public List<Department> GetDepartments()
    {
        return _context.Departments.ToList() ?? null;
    }

    public List<Manager> GetManagers()
    {
        try
        {
            var managers = _context.Employees
                .Where(emp => emp.ManagerId == null)
                .Select(emp => new Manager
                {
                    Id = emp.Id,
                    Name = emp.FirstName + " " + emp.LastName
                })
                .ToList();

            return managers;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<Project> GetProjects()
    {
        return _context.Projects.ToList() ?? null;
    }
}