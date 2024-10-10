namespace PlatformDemo.Data.Entities
{
    /// <summary>
    /// Represents a service plan with an associated collection of timesheets.
    /// </summary>
    public class ServicePlan
    {
        public int ServicePlanId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public ICollection<Timesheet> Timesheets { get; set; }
    }
}
