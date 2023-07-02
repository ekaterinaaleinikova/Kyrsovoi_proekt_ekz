using Azure.Core;
using exasp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using VacatiomWeb2.Models;
using VacationDB2.Models;

namespace exasp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetReport()
        {
            List<VacationRequestSubmission> vacationRequestSubmissions = new List<VacationRequestSubmission>();
            using (HttpClient client = new HttpClient())
            {
                using (var request = client.GetAsync("http://localhost:5237/api/Vacation/GetVacation"))
                {
                    var result = await request.Result.Content.ReadAsStringAsync();

                    vacationRequestSubmissions = JsonConvert.DeserializeObject<List<VacationRequestSubmission>>(result);

                }
            }

            return View(vacationRequestSubmissions);
        }
        public IActionResult LeaveRequest()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LeaveRequest(VacationRequestSubmission vacationRequestSubmission)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                var content = new StringContent(JsonConvert.SerializeObject(vacationRequestSubmission), Encoding.UTF8, "application/json");
                await httpClient.PostAsync("http://localhost:5237/api/Vacation/PostVacationRequest", content);
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
