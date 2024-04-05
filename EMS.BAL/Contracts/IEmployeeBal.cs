using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
using EMS.DB;
namespace EMS.BAL;

public interface IEmployeeBal
{
    int Add(Employee employee);
    int Delete(int employeeId);
    int Update(int employeeId, Employee employee);
    List<EmployeeDetail> Filter(EmployeeFilter employee);
    List<EmployeeDetail> Get(string? employeeNumber);
}
