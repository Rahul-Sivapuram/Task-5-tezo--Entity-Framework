﻿using System;
using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using EMS.Common;
using EMS.BAL;
using EMS.DAL;
using EMS.DB;
namespace EMS;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class Options
{
    [Option('o', "operation", Required = false, HelpText = "Operation to perform: add, display, update, delete, filter, help")]
    public string Operation { get; set; }

    [Option('i', "identifier", Required = false, HelpText = "Identifier for operation (e.g., employee ID)")]
    public string Identifier { get; set; }

    [Option('h', "help", HelpText = "Display this help screen")]
    public bool Help { get; set; }
}

public static class Program
{
    private static IConfigurationRoot _configuration;
    private static IWriter _console;
    private static IEmployeeDal _employeeDal;
    private static IEmployeeBal _employeeBal;
    private static IRoleDal _roleDal;
    private static IRoleBal _roleBal;
    private static IDropDownBal _dropDownBal;
    private static IDropDownDal _dropDownDal;
    private static RahulContext _context;
    private static string connectionString;

    static Program()
    {
        _configuration = GetConfiguration();
        _console = new ConsoleWriter();
        connectionString = _configuration["ConnectionString"];
        _context = new RahulContext();
        _employeeDal = new EmployeeDal(_context);
        _dropDownDal = new DropDownDal(_context);
        _dropDownBal = new DropDownBal(_dropDownDal);
        _roleDal = new RoleDal(_context);
        _roleBal = new RoleBal(_roleDal);
        _employeeBal = new EmployeeBal(_employeeDal);
    }

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
        .WithParsed(options =>
        {
            if (options.Help)
            {
                Help();
                return;
            }
            switch (options.Operation.ToLower())
            {
                case "add":
                    HandleAddOperation();
                    break;

                case "display":
                    HandleDisplayOperation(options);
                    break;

                case "filter":
                    HandleFilterOperation();
                    break;

                case "delete":
                    HandleDeleteOperation(options);
                    break;

                case "update":
                    HandleUpdateOperation(options);
                    break;

                case "add-role":
                    HandleAddRoleOperation();
                    break;

                case "display-roles":
                    HandleDisplayRolesOperation();
                    break;

                default:
                    _console.ShowError(Constants.InvalidOperationMessage);
                    break;
            }
        })
        .WithNotParsed(errors =>
        {
            _console.ShowError(Constants.InvalidCommandLineArgsMessage);
        });
    }

    private static void HandleDisplayRolesOperation()
    {
        List<Role> roles = _roleBal.Get();
        foreach (var item in roles)
        {
            _console.ShowInfo(string.Format(Constants.RolesTemplate, item.Id, item.Name, _dropDownBal.GetNameByDepartmentId((int)item.DepartmentId)));
        }
    }

    private static IConfigurationRoot GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Users\rahul.sivapuram\OneDrive - Technovert\Documents\rahul\Task-5-tezo\EMS\bin\Release\net8.0\win-x64\publish\appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    private static void HandleAddOperation()
    {
        Employee employee = GetEmployeeInput();
        int isAddSuccessful = _employeeBal.Add(employee);
        if (isAddSuccessful != -1)
        {
            _console.ShowSuccess(string.Format(Constants.EmployeeAddedSuccessMessage, employee.Uid));
        }
        else
        {
            _console.ShowError(Constants.InsertionFailed);
        }
    }

    private static void HandleDisplayOperation(Options options)
    {
        List<EmployeeDetail> employees = _employeeBal.Get(options.Identifier);
        if (employees.Count > 0)
        {
            foreach (var item in employees)
            {
                if (string.IsNullOrEmpty(options.Identifier) || item.Uid == options.Identifier)
                {
                    _console.ShowInfo(string.Format(Constants.EmployeeDetailsTemplate, item.Id, item.Uid, item.FirstName, item.LastName, item.Dob, item.EmailId, item.MobileNumber, item.JoiningDate, item.Location, item.Department, item.Role, item.Manager, item.Project));
                }
            }
        }
        else
        {
            _console.ShowError(Constants.NoEmployeeFoundMessage);
        }
    }

    private static void HandleFilterOperation()
    {
        EmployeeFilter filterInput = GetEmployeeFilterInput();
        List<EmployeeDetail> filteredEmployeeData = _employeeBal.Filter(filterInput);
        if (filteredEmployeeData.Count > 0)
        {
            foreach (var item in filteredEmployeeData)
            {
                _console.ShowInfo(string.Format(Constants.EmployeeDetailsTemplate, item.Id, item.Uid, item.FirstName, item.LastName, item.Dob, item.EmailId, item.MobileNumber, item.JoiningDate, item.Location, item.Department, item.Role, item.Manager, item.Project));
            }
        }
        else
        {
            _console.ShowError(Constants.NoEmployeeFoundMessage);
        }
    }

    private static void HandleDeleteOperation(Options options)
    {
        int res = _employeeBal.Delete(int.Parse(options.Identifier));
        if (res != -1)
        {
            _console.ShowSuccess(Constants.EmployeeDeletedSuccessMessage);
        }
        else
        {
            _console.ShowError(Constants.DeletionFailedMessage);
        }
    }

    private static void HandleUpdateOperation(Options options)
    {
        Employee employeeToUpdate = GetEmployeeInput();
        int isUpdated = _employeeBal.Update(int.Parse(options.Identifier), employeeToUpdate);
        if (isUpdated != -1)
        {
            _console.ShowSuccess(string.Format(Constants.EmployeeUpdatedSuccessMessage, options.Identifier));
        }
        else
        {
            _console.ShowSuccess(string.Format(Constants.NoEmployeeWithIdMessage, options.Identifier));
        }
    }

    private static void HandleAddRoleOperation()
    {
        Role role = new Role();
        List<string> roleDetails = GetRoleInput();
        role.Name = roleDetails[0];
        role.DepartmentId = _dropDownBal.GetDepartmentId(roleDetails[1]);
        bool sample = _roleBal.Insert(role);
        if (sample)
        {
            _console.ShowSuccess(String.Format(Constants.EmployeeAddedSuccessMessage, roleDetails[0]));
        }
        else
        {
            _console.ShowError(Constants.InsertionFailed);
        }
    }

    private static List<string> GetRoleInput()
    {
        string roleName, departmentName;
        _console.ShowInfo("Enter RoleName: ");
        roleName = Console.ReadLine();

        _console.ShowInfo("Enter Department: ");
        departmentName = Console.ReadLine();
        return [roleName.ToUpper(), departmentName.ToUpper()];
    }

    private static Employee GetEmployeeInput()
    {
        Employee employee = new Employee();
        employee.Uid = ReadValidInput("Enter Employee Number", s => string.IsNullOrEmpty(s) || Regex.IsMatch(s, @"^TZ\d{4}$"));
        employee.FirstName = ReadValidInput("First Name", s => !string.IsNullOrEmpty(s.Trim()));
        employee.LastName = ReadValidInput("Last Name", s => !string.IsNullOrEmpty(s.Trim()));
        employee.Dob = DateOnly.FromDateTime(DateTime.Parse(ReadInput("Date Of Birth (y/m/d)")));

        employee.EmailId = ReadValidInput("Email ID", s => string.IsNullOrWhiteSpace(s) || Regex.IsMatch(s, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));

        long mobileNumber;
        bool isValidMobile = long.TryParse(ReadValidInput("Mobile Number (10 digits)", s => string.IsNullOrWhiteSpace(s) || (long.TryParse(s, out mobileNumber) && s.Length == 10)), out mobileNumber);
        employee.MobileNumber = isValidMobile ? mobileNumber : 0;
        employee.JoiningDate = DateOnly.FromDateTime(DateTime.Parse(ReadInput("Joining Date (y/m/d)")));

        PrintOptions(_dropDownBal.GetLocationOptions(),d => d.Name);
        employee.LocationId = _dropDownBal.GetLocationId(ReadInput("Location"));
        PrintOptions(_dropDownBal.GetDepartmentOptions(),d => d.Name);
        employee.DepartmentId = _dropDownBal.GetDepartmentId(ReadInput("Department"));

        List<string> rolesList = _roleBal.GetRoleNamesForDepartment((int)employee.DepartmentId);
        if (rolesList.Count == 0)
        {
            _console.ShowError("This Department has no roles");
        }
        else
        {
            _console.ShowInfo("\nOptions");
            foreach (var item in rolesList)
            {
                _console.ShowInfo(item);
            }
            _console.ShowInfo("Enter role:");
            string roleName = Console.ReadLine();
            employee.RoleId = _roleBal.GetRoleId(roleName);
        }
        Console.WriteLine("\nAre you  a Manager? Y/N");
        string isManager = Console.ReadLine();
        employee.IsManager = isManager.Equals("Y", StringComparison.OrdinalIgnoreCase) ? true : false;
        List<Manager> managersList = _dropDownBal.GetManagerOptions();
        if (!employee.IsManager)
        {
            _console.ShowInfo("\nOptions");
            foreach (var item in managersList)
            {
                _console.ShowInfo(item.Name);
            }
            employee.ManagerId = _dropDownBal.GetManagerId(ReadInput("Manager"));
        }

        PrintOptions(_dropDownBal.GetProjectOptions(),d => d.Name);
        employee.ProjectId = _dropDownBal.GetProjectId(ReadInput("Project"));
        return employee;
    }

    private static string ReadInput(string prompt)
    {
        _console.ShowInfo($"Enter {prompt}: ");
        return Console.ReadLine();
    }

    private static string ReadValidInput(string prompt, Func<string, bool>? validator)
    {
        string input;
        do
        {
            input = ReadInput(prompt);
            if (!validator(input))
            {
                _console.ShowError("Invalid input. Please try again.");
            }
        } while (!validator(input));

        return input;
    }

    private static void PrintOptions<T>(List<T> dataList, Func<T, string> propertySelector)
    {
        Console.WriteLine("\nOptions:");
        foreach (var item in dataList)
        {
            Console.WriteLine(propertySelector(item));
        }
    }

    private static EmployeeFilter GetEmployeeFilterInput()
    {
        EmployeeFilter employeeFilterObject = new EmployeeFilter();
        _console.ShowInfo("Enter an alphabet:");
        employeeFilterObject.EmployeeName = Console.ReadLine();
        _console.ShowInfo("Enter Location:");
        string locationInput = Console.ReadLine();
        employeeFilterObject.Location = _dropDownBal.GetLocationByName(locationInput);

        _console.ShowInfo("Enter JobTitle:");
        string jobTitleInput = Console.ReadLine();
        employeeFilterObject.JobTitle = _roleBal.GetRoleByName(jobTitleInput);

        _console.ShowInfo("Enter Manager:");
        string managerInput = Console.ReadLine();
        employeeFilterObject.Manager = _dropDownBal.GetManagerByName(managerInput);

        _console.ShowInfo("Enter Project:");
        string projectInput = Console.ReadLine();
        employeeFilterObject.Project = _dropDownBal.GetProjectByName(projectInput);
        return employeeFilterObject;
    }

    private static void Help()
    {
        _console.ShowInfo(Constants.OptionsMessage);
    }
}
