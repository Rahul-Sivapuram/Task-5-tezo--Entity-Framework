using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EMS.Common;
using Microsoft.EntityFrameworkCore;

namespace EMS.DAL;

public class EmployeeDal : IEmployeeDal
{
    private readonly string _connectionString;
    private readonly EmployeeDbContext _context;

    public EmployeeDal(string connectionString, EmployeeDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }

    public List<EmployeeDetail> GetAll()
    {
        try
        {
            var data = _context.EmployeeDetails.ToList();
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public int Insert(Employee employee)
    {
        try
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee.Id ?? -1;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public int Update(string employeeNumber, Employee employee)
    {
        try
        {
            var existingEmp = _context.Employees.Find(employeeNumber);
            if (existingEmp != null)
            {
                existingEmp.Id = employee.Id;
                existingEmp.FirstName = employee.FirstName;
                existingEmp.LastName = employee.LastName;
                existingEmp.EmailId = employee.EmailId;
                existingEmp.MobileNumber = employee.MobileNumber;
                existingEmp.JoiningDate = employee.JoiningDate;
                existingEmp.LocationId = employee.LocationId;
                existingEmp.RoleId = employee.RoleId;
                existingEmp.DepartmentId = employee.DepartmentId;
                existingEmp.IsManager = employee.IsManager;
                existingEmp.ManagerId = employee.ManagerId;
                existingEmp.ProjectId = employee.ProjectId;
                _context.SaveChanges();
            }
            return existingEmp.Id ?? -1;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }

    public int Delete(string employeeId)
    {
        try
        {
            int rows = 0;
            var employee = _context.Employees.Find(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                rows = _context.SaveChanges();
            }
            return rows;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }

    public List<EmployeeDetail> Filter(EmployeeFilter? employee)
    {
        // var sqlSelect = @"SELECT 
        // emp.Id, emp.Uid, emp.FirstName, emp.LastName, CONVERT(varchar(10),emp.Dob,23) as DOB, emp.EmailId, emp.MobileNumber,CONVERT(varchar(10),emp.JoiningDate,23) as JoiningDate, 
        // Location.Name as Location, 
        // Roles.Name as Role, 
        // Department.Name as Department,
        // CONCAT(manager.FirstName, ' ', manager.LastName) as Manager,
        // Project.Name as Project  
        // FROM 
        //     Employee as emp
        // LEFT JOIN Employee as manager ON emp.ManagerId = manager.Id
        // JOIN Location ON emp.LocationId = Location.Id
        // JOIN Roles ON emp.RoleId = Roles.Id
        // JOIN Department ON emp.DepartmentId = Department.Id
        // JOIN Project ON emp.ProjectId = Project.Id";

        // var conditions = new List<string>();

        // if (employee != null)
        // {
        //     if (!string.IsNullOrEmpty(employee.EmployeeName))
        //         conditions.Add($"emp.FirstName LIKE '{employee.EmployeeName}%'");

        //     if (employee.Location != null)
        //         conditions.Add($"Location.Name = '{employee.Location.Name}'");

        //     if (employee.JobTitle != null)
        //         conditions.Add($"Roles.Name = '{employee.JobTitle.Name}'");

        //     if (employee.Manager != null)
        //         conditions.Add($"(manager.FirstName + ' ' + manager.LastName) = '{employee.Manager.Name}'");

        //     if (employee.Project != null)
        //         conditions.Add($"Project.Name = '{employee.Project.Name}'");
        // }

        // if (conditions.Any())
        //     sqlSelect += " WHERE " + string.Join(" AND ", conditions);
        // try
        // {
        //     var filteredEmployees = _context.EmployeeDetails.FromSqlRaw(sqlSelect).ToList();
        //     return filteredEmployees;
        // }
        // catch (Exception ex)
        // {
        //     return null;
        // }
        try
        {
            var query = _context.EmployeeDetails.AsQueryable();

            if (employee != null)
            {
                if (!string.IsNullOrEmpty(employee.EmployeeName))
                    query = query.Where(emp => emp.FirstName.StartsWith(employee.EmployeeName));

                if (employee.Location != null)
                    query = query.Where(emp => emp.Location == employee.Location.Name);

                if (employee.JobTitle != null)
                    query = query.Where(emp => emp.Role == employee.JobTitle.Name);

                if (employee.Manager != null)
                    query = query.Where(emp => (emp.Manager == employee.Manager.Name));

                if (employee.Project != null)
                    query = query.Where(emp => emp.Project == employee.Project.Name);
            }

            var filteredEmployees = query.ToList();
            return filteredEmployees;
        }
        catch (Exception ex)
        {
            // Handle the exception
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
