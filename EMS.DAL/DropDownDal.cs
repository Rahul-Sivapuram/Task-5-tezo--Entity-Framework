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
        // List<DropDown> empData = new List<DropDown>();
        // string sqlSelect = @"SELECT 
        // emp.Id, emp.Uid, emp.FirstName, emp.LastName, emp.Dob as DOB, emp.EmailId, emp.MobileNumber, emp.JoiningDate, Location.Name as Location, Roles.Name as Role, Department.Name as Department,CONCAT(manager.FirstName, ' ', manager.LastName) as Manager,Project.Name as Project  
        // FROM Employee as emp LEFT JOIN Employee as manager ON emp.ManagerId = manager.Id
        // JOIN Location ON emp.LocationId = Location.Id JOIN Roles ON emp.RoleId = Roles.Id
        // JOIN Department ON emp.DepartmentId = Department.Id JOIN Project ON emp.ProjectId = Project.Id
        // WHERE emp.ManagerId IS NULL";
        // SqlConnection conn = new SqlConnection(_connectionString);
        // try
        // {
        //     using (conn)
        //     {
        //         conn.Open();
        //         using (SqlCommand command = new SqlCommand(sqlSelect, conn))
        //         {
        //             using (SqlDataReader reader = command.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     DropDown manager = new DropDown();
        //                     manager.Id = reader.GetInt32(reader.GetOrdinal("Id"));
        //                     manager.Name = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
        //                     empData.Add(manager);
        //                 }
        //             }
        //         }
        //     }
        // }
        // catch (System.Exception)
        // {
        //     throw;
        // }
        // finally
        // {
        //     conn.Close();
        // }
        // return empData;
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
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public List<Project> GetProjects()
    {
        return _context.Projects.ToList() ?? null;
    }
}