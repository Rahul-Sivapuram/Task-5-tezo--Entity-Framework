using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DAL;

public class EmployeeDetail
{   
    public int? Id { get; set; }
    public string? Uid {get; set; }
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? DOB {get; set;}
    public string? EmailId {get; set;}
    public long? MobileNumber {get; set;}
    public string? JoiningDate {get; set;}
    public string? Location {get; set;}
    public string? Role {get; set;}
    public string? Department {get; set;}
    public string? Manager {get; set;}
    public string? Project {get; set;}
}
