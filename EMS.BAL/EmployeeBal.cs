using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using EMS.DAL;
using EMS.DB;
namespace EMS.BAL;

public class EmployeeBal : IEmployeeBal
{
    private readonly IEmployeeDal _employeeDal;
    
    public EmployeeBal(IEmployeeDal employeeDalObject)
    {
        _employeeDal = employeeDalObject;
    }

    public int Add(Employee employee)
    {
        return _employeeDal.Insert(employee);
    }

    public int Delete(int employeeId)
    {
        return _employeeDal.Delete(employeeId);
    }

    public int Update(int employeeId, Employee employee)
    {
        int res = _employeeDal.Update(employeeId, employee);
        return res;
    }

    public List<EmployeeDetail> Filter(EmployeeFilter employee)
    {
        return _employeeDal.Filter(employee);
    }

    public List<EmployeeDetail> Get(string employeeNumber = "")
    {
        List<EmployeeDetail> employeeData = _employeeDal.GetAll();
        if (!string.IsNullOrEmpty(employeeNumber))
        {
            return employeeData.Where(e => e.Uid == employeeNumber).ToList();
        }
        return employeeData;
    }
}
