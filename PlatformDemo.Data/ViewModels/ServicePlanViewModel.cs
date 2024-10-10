using PlatformDemo.Data.Entities;

namespace PlatformDemo.Data.ViewModels
{
	public class ServicePlanViewModel
    {
        public IEnumerable<ServicePlan> ServicePlans { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;  
        public string SearchTerm { get; set; }
        public string SortOrder { get; set; }
    }

}
