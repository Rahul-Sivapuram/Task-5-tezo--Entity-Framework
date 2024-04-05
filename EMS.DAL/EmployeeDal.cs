using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EMS.DB;
using Microsoft.EntityFrameworkCore;
namespace EMS.DAL;

public class EmployeeDal : IEmployeeDal
{
    private readonly RahulContext _context;

    public EmployeeDal(RahulContext context)
    {
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
            return null;
        }
    }

    public int Insert(Employee employee)
    {
        try
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return -1;
        }
    }

    public int Update(int employeeId, Employee employee)
    {
        try
        {
            var existingEmp = _context.Employees.Find(employeeId);
            if (existingEmp != null)
            {
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
            return existingEmp.Id;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public int Delete(int employeeId)
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
            return -1;
        }
    }

    public List<EmployeeDetail> Filter(EmployeeFilter? employee)
    {
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
            return null;
        }
    }
}
