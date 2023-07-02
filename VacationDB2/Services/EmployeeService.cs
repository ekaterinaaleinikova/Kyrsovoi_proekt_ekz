using Microsoft.EntityFrameworkCore;
using VacationDB2.Data;

namespace VacationDB2.Services
{
	public class EmployeeService
	{
		private readonly ApplicationDbContext dbContext = new ApplicationDbContext();
		public bool IsOnVacation(int emplyeeId)
		{
			try
			{
				var employee = dbContext.Employees.Find(emplyeeId);
				if (employee.IsCurrentlyEmployed == true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				var message = ex.Message;
				return false;
			}
		}

		private bool ShowMessage(string message)
		{
			throw new NotImplementedException();
		}

		public bool AddMonth(int emloyeeId)
		{
			var employee = dbContext.Employees.Find(emloyeeId);
			DateTime date = employee.DateOfHire.Value;
			DateTime dateTime = date.AddMonths(6);
			if (dateTime <= DateTime.Now)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool NumberOfDaysOnLeave(int employeeId, int wantDays)
		{
			var employee = dbContext.Employees.Find(employeeId);
			if (employee.NumberOfDaysOnLeave >= wantDays)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool CanGoOnVacation(int employeeId)
		{
			var employee = dbContext.Employees.Find(employeeId);
			if (employee.CanGoOnVacation == true)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
