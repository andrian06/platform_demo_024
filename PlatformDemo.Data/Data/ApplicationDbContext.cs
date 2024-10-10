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

        // Configuring LocalDB as the database provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PlatformDemoDb;Trusted_Connection=True;");
            }
        }

        // Seeding data to the database on initial creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial service plans
            modelBuilder.Entity<ServicePlan>().HasData(
                new ServicePlan { ServicePlanId = 1, DateOfPurchase = DateTime.Now.AddDays(-30) },
                new ServicePlan { ServicePlanId = 2, DateOfPurchase = DateTime.Now.AddDays(-25) },
                new ServicePlan { ServicePlanId = 3, DateOfPurchase = DateTime.Now.AddDays(-20) },
                new ServicePlan { ServicePlanId = 4, DateOfPurchase = DateTime.Now.AddDays(-15) }
            );

            // Seed initial timesheets
            modelBuilder.Entity<Timesheet>().HasData(
                new Timesheet { TimesheetId = 1, ServicePlanId = 1, StartDateTime = DateTime.Now.AddDays(-10), EndDateTime = DateTime.Now.AddDays(-9), Description = "Initial Setup" },
                new Timesheet { TimesheetId = 2, ServicePlanId = 1, StartDateTime = DateTime.Now.AddDays(-9), EndDateTime = DateTime.Now.AddDays(-8), Description = "Configuration" },
                new Timesheet { TimesheetId = 3, ServicePlanId = 2, StartDateTime = DateTime.Now.AddDays(-8), EndDateTime = DateTime.Now.AddDays(-7), Description = "Update" },
                // Additional timesheet entries
                new Timesheet { TimesheetId = 4, ServicePlanId = 2, StartDateTime = DateTime.Now.AddDays(-7), EndDateTime = DateTime.Now.AddDays(-6), Description = "Bug Fix" },
                new Timesheet { TimesheetId = 5, ServicePlanId = 3, StartDateTime = DateTime.Now.AddDays(-6), EndDateTime = DateTime.Now.AddDays(-5), Description = "Testing" }
            );
        }
    }
}