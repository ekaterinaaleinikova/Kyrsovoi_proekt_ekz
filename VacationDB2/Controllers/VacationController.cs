using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacationDB2.Data;
using VacationDB2.Models;
using VacationDB2.Services;

namespace VacationDB2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacationController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;
		private EmployeeService em = new EmployeeService();
		private Employee employee = new Employee();
		private VacationRequestSubmission requestSubmission = new VacationRequestSubmission();
		public VacationController(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		[HttpGet("GetVacation")]
		public async Task<IEnumerable<VacationRequestSubmission>> GetVacationAsync()
		{
			return await dbContext.VacationRequestSubmissions.ToListAsync();
		}
		[HttpGet("GetVacationById/{id}")]
		public async Task<IActionResult> GetVacationById(int id)
		{
			try
			{
				//throw new Exception("test errror");
				var employee = await dbContext.VacationRequestSubmissions.FindAsync(id);
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
		[HttpPost("PostVacationRequest")]
		public IActionResult PostVacationRequest(string Reason, DateTime LeaveStartDate, DateTime VacationEndDate, int WantDays, int employeeId)
		{
			requestSubmission.LeaveStartDate = LeaveStartDate;
			requestSubmission.VacationEndDate = VacationEndDate;
			requestSubmission.Reason = Reason;
			requestSubmission.EmployeeId = employeeId;
			dbContext.VacationRequestSubmissions.Add(requestSubmission);
			dbContext.SaveChanges();
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> PutVacation(int vacationId, string Reason, DateTime LeaveStartDate, DateTime VacationEndDate, int WantDays)
		{
			requestSubmission = dbContext.VacationRequestSubmissions.Find(vacationId);
			if (vacationId == null)
			{
				return BadRequest("Такого отчета нету");
			}
			else
			{
				try
				{
					requestSubmission.LeaveStartDate = LeaveStartDate;
					requestSubmission.VacationEndDate = VacationEndDate;
					requestSubmission.Reason = Reason;
					requestSubmission.WantDays = WantDays;
					dbContext.Update(requestSubmission);
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
