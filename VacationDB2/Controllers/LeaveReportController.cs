using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationDB2.Data;
using VacationDB2.Models;
using VacationDB2.Services;

namespace VacationDB2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveReportController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;
		private EmployeeService em = new EmployeeService();
		private Employee employee = new Employee();
		private LeaveReport leaveReportt = new LeaveReport();
		public LeaveReportController(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		[HttpPost("PostLeaveReportMal")]
		public IActionResult PostLeaveReport(string Reason, DateTime LeaveStartDate, DateTime VacationEndDate, int WantDays, int EmployeeId)
		{
			try
			{
				LeaveReport leaveReport = new LeaveReport();
				if (em.IsOnVacation(EmployeeId))
				{
					if (em.AddMonth(EmployeeId))
					{
						if (em.NumberOfDaysOnLeave(EmployeeId, WantDays))
						{
							if (em.CanGoOnVacation(EmployeeId))
							{
								leaveReport.AdminId = 1;
								leaveReport.IsLeaveApproved = true;
								leaveReport.LeaveStartDate = LeaveStartDate;
								leaveReport.LeaveEndDate = VacationEndDate;
								leaveReport.Reason = Reason;
								leaveReport.EmployeeId = EmployeeId;
								dbContext.Add(leaveReport);
								dbContext.SaveChanges();
							}
							else
							{
								return BadRequest("Работай");
							}
						}
						else
						{
							return BadRequest("Перехочешь");
						}
					}
					else
					{
						return BadRequest("Kirpich");
					}
				}
				else
				{
					return BadRequest("Работай салага");
				}
				dbContext.SaveChanges();
				return Ok();
			}
			catch (Exception ex)
			{
				//dbContext.VacationRequestSubmissions.Add();
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("GetLeaveReport")]
		public async Task<IEnumerable<LeaveReport>> GetLeaveReportAsync()
		{
			return await dbContext.LeaveReports.ToListAsync();
		}
		[HttpGet("GetLeaveReportById/{id}")]
		public async Task<IActionResult> GetLeaveReportById(int id)
		{
			try
			{
				//throw new Exception("test errror");
				var employee = await dbContext.LeaveReports.FindAsync(id);
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
		[HttpPut]
		public async Task<IActionResult> PutLeaveReport(int leaveReport, bool isleaveapproved)
		{
			var findEmp = dbContext.LeaveReports.Find(leaveReport);
			if (leaveReport == null)
			{
				return BadRequest("Такого отчета нету");
			}
			else
			{
				try
				{
					findEmp.IsLeaveApproved = isleaveapproved;
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
