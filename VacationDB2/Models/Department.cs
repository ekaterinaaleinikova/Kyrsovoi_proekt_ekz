using System;
using System.Collections.Generic;

namespace VacationDB2.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? DepartmentName { get; set; }

    public int? NumberOfEmployees { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
