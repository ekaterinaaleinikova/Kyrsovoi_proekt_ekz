using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationDB2.Data;
using VacationDB2.Models;
using VacationDB2.Services;

namespace VacationDB2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;
		private EmployeeService em = new EmployeeService();
		private Employee employee = new Employee();
		public AdminController(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet("Chlen")]
		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			return await dbContext.Employees.ToListAsync();
			
		}

		[HttpGet("GetEmployeeById/{id}")]
		public async Task<IActionResult> GetEmployeeById(int id)
		{
			try
			{
				var employee = await dbContext.Employees.FindAsync(id);
				if (employee != null)
				{
					return Ok(employee);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("FiredEmployee")]
		public async Task<IActionResult> PutEmployee(int employeeId, bool IsCurrentlyEmployed)
		{
			var findEmp = dbContext.Employees.Find(employeeId);

			if (employee == null)
			{
				return BadRequest("Такого сотрудника не существует");
			}
			else
			{
				try
				{
					findEmp.IsCurrentlyEmployed = IsCurrentlyEmployed;

					dbContext.Update(findEmp);
					dbContext.SaveChanges();
					return Ok();
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
		}

	}
}







//[HttpGet("GetVacation")]
//public async Task<IEnumerable<VacationRequestSubmission>> GetVacationAsync()
//{
//	return await dbContext.VacationRequestSubmissions.ToListAsync();
//}
//[HttpGet("GetVacationById/{id}")]
//public async Task<IActionResult> GetVacationById(int id)
//{
//	try
//	{
//		//throw new Exception("test errror");
//		var employee = await dbContext.VacationRequestSubmissions.FindAsync(id);
//		if (employee != null)
//		{
//			return Ok(employee);
//		}
//		else
//		{
//			return NotFound();
//		}
//	}
//	catch (Exception ex)
//	{
//		return BadRequest(ex.Message);
//	}
//}
//[HttpGet("GetLeaveReport")]
//public async Task<IEnumerable<LeaveReport>> GetLeaveReportAsync()
//{
//	return await dbContext.LeaveReports.ToListAsync();
//}
//[HttpGet("GetLeaveReportById/{id}")]
//public async Task<IActionResult> GetLeaveReportById(int id)
//{
//	try
//	{
//		//throw new Exception("test errror");
//		var employee = await dbContext.LeaveReports.FindAsync(id);
//		if (employee != null)
//		{
//			return Ok(employee);
//		}
//		else
//		{
//			return NotFound();
//		}
//	}
//	catch (Exception ex)
//	{
//		return BadRequest(ex.Message);
//	}
//}
//[HttpPut("PutLeaveReport")]
//public async Task<IActionResult> PutLeaveReport([FromBody] LeaveReport leaveReport)
//{
//	var findEmp = dbContext.LeaveReports.Find(leaveReport.Id);
//	if (leaveReport == null)
//	{
//		return BadRequest("Такого отчета нету");
//	}
//	else
//	{
//		try
//		{
//			findEmp.IsLeaveApproved = leaveReport.IsLeaveApproved;

//			dbContext.SaveChanges();
//			return Ok();
//		}
//		catch (Exception ex)
//		{
//			return BadRequest(ex.Message);
//		}
//	}
//}