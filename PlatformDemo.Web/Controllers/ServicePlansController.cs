using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data;
using PlatformDemo.Data.ViewModels;
using System.Globalization;

namespace PlatformDemo.Web.Controllers
{
	/// <summary>
	/// Controller responsible for handling operations related to Service Plans,
	/// including listing, searching, sorting, pagination, and fetching related timesheets.
	/// </summary>
	public class ServicePlansController : Controller
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Constructor to inject the ApplicationDbContext for database access.
		/// </summary>
		/// <param name="context">Instance of the application's DbContext</param>
		public ServicePlansController(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Displays a paginated, searchable, and sortable list of service plans.
		/// </summary>
		/// <param name="searchTerm">The date to filter service plans (in dd/MM/yyyy format)</param>
		/// <param name="sortOrder">The sort order for service plans (by date or timesheets count)</param>
		/// <param name="pageNumber">The current page number for pagination</param>
		/// <returns>View with ServicePlanViewModel containing the filtered and sorted service plans</returns>
		public IActionResult Index(string searchTerm, string sortOrder, int pageNumber = 1)
		{
			// Start by retrieving all service plans and include related timesheets in the query
			var servicePlans = _context.ServicePlans
				.Include(sp => sp.Timesheets) // Include the related timesheets in the query
				.AsQueryable(); // Enable further filtering before executing the query

			// Search functionality: check if searchTerm matches a valid date in "dd/MM/yyyy" format
			DateTime parsedDate;
			if (DateTime.TryParseExact(searchTerm, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
			{
				// Filter service plans by the specified purchase date
				servicePlans = servicePlans.Where(sp => sp.DateOfPurchase.Date == parsedDate.Date);
			}

			// Sorting functionality: Sort by date or by the count of related timesheets
			servicePlans = sortOrder switch
			{
				"date_desc" => servicePlans.OrderByDescending(sp => sp.DateOfPurchase),
				"timesheets" => servicePlans.OrderBy(sp => sp.Timesheets.Count()), // Sort by number of timesheets (ascending)
				"timesheets_desc" => servicePlans.OrderByDescending(sp => sp.Timesheets.Count()), // Sort by number of timesheets (descending)
				_ => servicePlans.OrderBy(sp => sp.DateOfPurchase), // Default sort is by purchase date (ascending)
			};

			// Pagination logic: define the page size and skip the records for previous pages
			int pageSize = 10;
			var servicePlansPaged = servicePlans.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Fetch the current page's records
			var totalCount = servicePlans.Count(); // Get the total count of filtered service plans for pagination purposes

			// Prepare the view model with the paginated and filtered data
			var viewModel = new ServicePlanViewModel
			{
				ServicePlans = servicePlansPaged,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize,
				SearchTerm = searchTerm, // Pass the search term back to the view to persist it
				SortOrder = sortOrder 
			};

			return View(viewModel); 
		}

		/// <summary>
		/// Fetches and returns the timesheets associated with a specific service plan.
		/// This is used to dynamically load timesheets via partial view rendering.
		/// </summary>
		/// <param name="servicePlanId">The ID of the service plan</param>
		/// <returns>A PartialView with the list of timesheets for the given service plan</returns>
		public IActionResult GetTimesheets(int servicePlanId)
		{
			// Fetch all timesheets related to the given service plan ID
			var timesheets = _context.Timesheets
				.Where(t => t.ServicePlanId == servicePlanId)
				.ToList();

			// Return the timesheets as a partial view for client-side rendering
			return PartialView("_TimesheetPartial", timesheets);
		}
	}
}
