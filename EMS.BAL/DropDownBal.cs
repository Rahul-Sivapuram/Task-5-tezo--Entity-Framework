using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using EMS.DAL;
using EMS.DB;
namespace EMS.BAL;

public class DropDownBal : IDropDownBal
{
    private readonly IDropDownDal _dropDownDal;

    public DropDownBal(IDropDownDal dropDownDalObject)
    {
        _dropDownDal = dropDownDalObject;
    }

    public int GetLocationId(string locationName)
    {
        List<Location> items = _dropDownDal.GetLocations();
        var item = items.FirstOrDefault(i => string.Equals(i.Name, locationName.ToUpper(), StringComparison.OrdinalIgnoreCase));
        return item?.Id ?? -1;
    }

    public int GetManagerId(string managerName)
    {
        var item = _dropDownDal.GetManagers().FirstOrDefault(i => string.Equals(i.Name, managerName, StringComparison.OrdinalIgnoreCase));
        return item?.Id ?? -1;
    }

    public int GetProjectId(string projectName)
    {
        List<Project> items = _dropDownDal.GetProjects();
        var item = items.FirstOrDefault(i => string.Equals(i.Name, projectName.ToUpper(), StringComparison.OrdinalIgnoreCase));
        return item?.Id ?? -1;
    }

    public int GetDepartmentId(string departmentName)
    {
        List<Department> departmentList = _dropDownDal.GetDepartments();
        bool ans = departmentList.Any(department => department.Name == departmentName.ToUpper());
        if (ans)
        {
            var item = departmentList.FirstOrDefault(i => string.Equals(i.Name, departmentName.ToUpper(), StringComparison.OrdinalIgnoreCase));
            return item?.Id ?? -1;
        }
        else
        {
            return -1;
        }
    }

    public Location GetLocationByName(string locationName)
    {
        List<Location> data = _dropDownDal.GetLocations();
        Location item = data.FirstOrDefault(item => locationName.ToUpper().Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public Department GetDepartmentByName(string departmentName)
    {
        List<Department> data = _dropDownDal.GetDepartments();
        Department item = data.FirstOrDefault(item => departmentName.ToUpper().Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public Manager GetManagerByName(string managerName)
    {
        List<Manager> data = _dropDownDal.GetManagers();
        Manager item = data.FirstOrDefault(item => managerName.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public Project GetProjectByName(string projectName)
    {
        List<Project> data = _dropDownDal.GetProjects();
        Project item = data.FirstOrDefault(item => projectName.ToUpper().Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public string GetNameByLocationId(int id)
    {
        var location = _dropDownDal.GetLocations().FirstOrDefault(x => x.Id == id);
        return location != null ? location.Name : null;
    }

    public string GetNameByDepartmentId(int id)
    {
        var department = _dropDownDal.GetDepartments().FirstOrDefault(x => x.Id == id);
        return department != null ? department.Name : null;
    }

    public string GetNameByManagerId(int id)
    {
        var manager = _dropDownDal.GetManagers().FirstOrDefault(x => x.Id == id);
        return manager != null ? manager.Name : null;
    }

    public string GetNameByProjectId(int id)
    {
        var project = _dropDownDal.GetProjects().FirstOrDefault(x => x.Id == id);
        return project != null ? project.Name : null;
    }

    public List<Department> GetDepartmentOptions()
    {
        return _dropDownDal.GetDepartments();
    }

    public List<Location> GetLocationOptions()
    {
        return _dropDownDal.GetLocations();
    }

    public List<Manager> GetManagerOptions()
    {
        return _dropDownDal.GetManagers();
    }

    public List<Project> GetProjectOptions()
    {
        return _dropDownDal.GetProjects();
    }
}
