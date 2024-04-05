using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DB;
namespace EMS.DAL;

public interface IEmployeeDal
{
    List<EmployeeDetail> GetAll();
    int Insert(Employee data);
    int Update(int employeeNumber, Employee employee);
    int Delete(int employeeId);
    List<EmployeeDetail> Filter(EmployeeFilter? employee);
}
