using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data.Entities;


namespace PlatformDemo.Data
{
    /// <summary>
    /// The application database context used to manage data for ServicePlan and Timesheet entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        // DbSet for ServicePlan entity
        public DbSet<ServicePlan> ServicePlans { get; set; }
        // DbSet for Timesheet entity
        public DbSet<Timesheet> Timesheets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       

        // Seeding data to the database on initial creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding ServicePlans
            var servicePlans = new List<ServicePlan>
    {
        new ServicePlan { Id = 1, DateOfPurchase = new DateTime(2023, 1, 15) },
        new ServicePlan { Id = 2, DateOfPurchase = new DateTime(2023, 2, 10) },
        new ServicePlan { Id = 3, DateOfPurchase = new DateTime(2023, 3, 20) },
        new ServicePlan { Id = 4, DateOfPurchase = new DateTime(2023, 4, 5) },
        new ServicePlan { Id = 5, DateOfPurchase = new DateTime(2023, 4, 25) }
    };

            modelBuilder.Entity<ServicePlan>().HasData(servicePlans);

            // Seeding Timesheets with Foreign Key
            var timesheets = new List<Timesheet>
    {
        // Timesheets for ServicePlan 1
        new Timesheet { Id = 1, ServicePlanId = 1, StartDateTime = new DateTime(2023, 1, 16, 9, 0, 0), EndDateTime = new DateTime(2023, 1, 16, 17, 0, 0), Description = "Initial consultation" },
        new Timesheet { Id = 2, ServicePlanId = 1, StartDateTime = new DateTime(2023, 1, 17, 10, 0, 0), EndDateTime = new DateTime(2023, 1, 17, 18, 0, 0), Description = "Project kickoff" },

        // Timesheets for ServicePlan 2
        new Timesheet { Id = 3, ServicePlanId = 2, StartDateTime = new DateTime(2023, 2, 11, 9, 30, 0), EndDateTime = new DateTime(2023, 2, 11, 16, 30, 0), Description = "Requirements gathering" },
        new Timesheet { Id = 4, ServicePlanId = 2, StartDateTime = new DateTime(2023, 2, 12, 10, 0, 0), EndDateTime = new DateTime(2023, 2, 12, 18, 0, 0), Description = "Design discussion" },

        // Timesheets for ServicePlan 3
        new Timesheet { Id = 5, ServicePlanId = 3, StartDateTime = new DateTime(2023, 3, 21, 9, 0, 0), EndDateTime = new DateTime(2023, 3, 21, 17, 0, 0), Description = "Development sprint" },
        new Timesheet { Id = 6, ServicePlanId = 3, StartDateTime = new DateTime(2023, 3, 22, 10, 0, 0), EndDateTime = new DateTime(2023, 3, 22, 18, 0, 0), Description = "Code review" },

        // ServicePlan 4 has no Timesheets
        // ServicePlan 5 has no Timesheets
    };

            modelBuilder.Entity<Timesheet>().HasData(timesheets);
        }
    }
}