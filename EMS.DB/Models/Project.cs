using System;
using System.Collections.Generic;

namespace EMS.DB;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
