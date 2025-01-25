namespace WhiteLagoon.web.ViewModels
{
    public class RedialBarChartDto
    {
        public decimal TotalCount { get; set; }
        public decimal CountInCurrentMonth { get; set; }
        public bool hasRationIncreased { get; set; }
        public int[] Series { get; set; }
    }
}
