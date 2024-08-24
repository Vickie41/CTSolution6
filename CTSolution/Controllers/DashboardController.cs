using CTSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace CTSolution.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dashboardService.GetDashboardDataAsync();
            return View(model);
        }
    }

}
