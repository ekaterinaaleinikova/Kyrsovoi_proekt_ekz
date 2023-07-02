using System;
using System.Collections.Generic;

namespace VacationDB2.Models;

public partial class VacationRequestSubmission
{
    public int Id { get; set; }

    public string? Reason { get; set; }

    public DateTime? LeaveStartDate { get; set; }

    public DateTime? VacationEndDate { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; }
    public int WantDays { get; set; }
}
