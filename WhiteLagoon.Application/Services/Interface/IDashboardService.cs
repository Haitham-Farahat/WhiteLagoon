using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.web.ViewModels;

namespace WhiteLagoon.Application.Services.Interface
{
    public interface IDashboardService
    {
        Task<RedialBarChartDto> GetTotalBookingRedialchartData();
        Task<RedialBarChartDto> GetRegisteredUserCharData();
        Task<RedialBarChartDto> GetRevenueChartData();
        Task<PieChartDto> GetBookingPieChartData();
        Task<LineChartDto> GetMemberAndBookingLineChartData();
    }
}
