using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
using EMS.DB;
namespace EMS.BAL;

public interface IDropDownBal
{
    int GetLocationId(string locationName);
    int GetManagerId(string managerName);
    int GetProjectId(string projectName);
    int GetDepartmentId(string departmentName);
    Location GetLocationByName(string userInput);
    Department GetDepartmentByName(string userInput);
    Manager GetManagerByName(string userInput);
    Project GetProjectByName(string userInput);
    string GetNameByLocationId(int id);
    string GetNameByDepartmentId(int id);
    string GetNameByManagerId(int id);
    string GetNameByProjectId(int id);
    List<Department> GetDepartmentOptions();
    List<Location> GetLocationOptions();
    List<Manager> GetManagerOptions();
    List<Project> GetProjectOptions();
}