using Microsoft.AspNetCore.Mvc;

namespace VacationWeb.Controllers
{
	public class StartPage : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
