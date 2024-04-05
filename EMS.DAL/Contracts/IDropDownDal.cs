using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DB;
namespace EMS.DAL;

public interface IDropDownDal
{
    List<Location> GetLocations();
    List<Department> GetDepartments();
    List<Manager> GetManagers();
    List<Project> GetProjects();
}
