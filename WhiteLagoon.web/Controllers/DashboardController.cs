    using Microsoft.AspNetCore.Mvc;
    using WhiteLagoon.Application.Common.Interfaces;
    using WhiteLagoon.Application.Common.Utility;
using WhiteLagoon.Application.Services.Implementation;
using WhiteLagoon.Application.Services.Interface;
using WhiteLagoon.web.ViewModels;

namespace WhiteLagoon.web.Controllers
{
    public class DashboardController : Controller
    {
       private readonly IDashboardService _dashboardService;
       public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> GetTotalBookingRedialchartData()
        {
           return Json(await _dashboardService.GetTotalBookingRedialchartData());
        }

        public async Task<IActionResult> GetRegisteredUserCharData()
        {
            return Json(await _dashboardService.GetRegisteredUserCharData());
        }

        public async Task<IActionResult> GetRevenueChartData()
        {
            return Json(await _dashboardService.GetRevenueChartData());
        }
        public async Task<IActionResult> GetBookingPieChartData()
        {
            return Json(await _dashboardService.GetBookingPieChartData());
        }

        public async Task<IActionResult> GetMemberAndBookingLineChartData()
        {
            return Json(await _dashboardService.GetMemberAndBookingLineChartData());
        }

    } 
}
