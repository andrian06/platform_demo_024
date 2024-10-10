namespace PlatformDemo.Data.Entities
{
	/// <summary>
	/// Represents a timesheet entry associated with a service plan.
	/// </summary>
	public class Timesheet
    {
        public int Id { get; set; }
        public int ServicePlanId { get; set; }
        public ServicePlan ServicePlan { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
    }
}
