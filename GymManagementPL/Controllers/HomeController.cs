using System.Diagnostics;
using GymManagementBLL.Services.Interfaces;
using GymManagementPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var analytics = _analyticsService.GetAnalyticsData();
            return View(analytics);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
