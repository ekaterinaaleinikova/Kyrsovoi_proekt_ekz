using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationDB2.Data;
using VacationDB2.Models;

namespace VacationDB2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;
		private Employee employee = new Employee();
		public EmployeeController(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		[HttpPost]
		public IActionResult PostEmployee(string name, string surname, string patronymic, int salary, int NumberOfDaysOnLeave, string email, string password, int departmentId)
		{
			try
			{
				//var fndemployee = dbContext.Employees.Find(email);
				//if (email == fndemployee.Email)
				//{
				//	return BadRequest();
				//}
				//else
				//{
					employee.Name = name;
					employee.Email = email;
					employee.Salary = salary;
					employee.Password = password;
					employee.Lastname = surname;
					employee.FatherName = patronymic;
					employee.NumberOfDaysOnLeave = NumberOfDaysOnLeave;
					employee.DateOfHire = DateTime.Now;
					employee.DepartmentId = departmentId;
					dbContext.Employees.Add(employee);
					dbContext.SaveChanges();

					return Ok(employee);
				//}
				//throw new Exception("test err");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet]
		public async Task<IActionResult> Authorizations(string email, string password)
		{
			var employee = await dbContext.Employees.FindAsync(email);
			if (employee == null)
			{
				return BadRequest();
			}
			else
			{
				if (employee.Password == password)
				{
					return Ok(employee);
				}
				else
				{
					return BadRequest();
				}
			}
		}
	}
}
