using System;
using System.Collections.Generic;

namespace VacationDB2.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? FatherName { get; set; }

    public DateTime? DateOfHire { get; set; }

    public decimal? Salary { get; set; }

    public int? NumberOfDaysOnLeave { get; set; }

    public int? DepartmentId { get; set; }

    public bool? IsCurrentlyEmployed { get; set; }

    public bool? CanGoOnVacation { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual Department Department { get; set; }

    public virtual ICollection<LeaveReport> LeaveReports { get; set; } = new List<LeaveReport>();

    public virtual ICollection<VacationRequestSubmission> VacationRequestSubmissions { get; set; } = new List<VacationRequestSubmission>();
}
