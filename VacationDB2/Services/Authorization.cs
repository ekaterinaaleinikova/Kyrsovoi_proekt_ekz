using Microsoft.AspNetCore.Http.HttpResults;
using VacationDB2.Data;
using VacationDB2.Models;

namespace VacationDB2.Services
{
	class Authorization
	{
		private readonly ApplicationDbContext dbContext;
		public Employee employee = new Employee();
		public Authorization(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public Employee Authorizations(string email, string password)
		{
			employee = dbContext.Employees.Find(email);
			if (employee == null)
			{
				if (employee.Password == password)
				{
					return employee;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}
	}
}
