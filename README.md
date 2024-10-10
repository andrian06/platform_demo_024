```markdown
# PlatformDemo

## TEST TASK TO COMPLETE

- Share a link to a new GitHub repository with recruiting@logicimtech.com.
- The GitHub repository name is: platform_demo_024.
- The GitHub repository will contain a single .NET solution and a readme.md file.
  
### .NET Solution:
- The solution name is **PlatformDemo**. It will contain 2 projects:
  - **.NET 8 Class Library**: 
    - One Entity Framework DbContext using a localdb, including a set of ServicePlans and a set of Timesheets.
    - **ServicePlan class** (ServicePlan Id, Date of purchase).
    - **Timesheet class** (Timesheet Id, related service plan, Date and time of start, Date and time of end, Description).
    - A service plan can have 0 or more timesheets associated with it.

  - **ASP.NET Core 8 web app**:
    - Reference the class library and build a page that shows the list of service plans along with their date of purchase and number of timesheets. Each service plan must show on a single line, even if it does not have related timesheets.
    - Seed sample data (10-15 service plans, 0-5 timesheets per service plan).

## Overview

PlatformDemo is a .NET 8 application designed to manage service plans and associated timesheets. The application provides functionalities such as searching, sorting, and paginating service plans, as well as displaying associated timesheets in a user-friendly interface.

## Features

- **Search functionality for service plans**: Users can search service plans by date.
- **Sorting options**: Service plans can be sorted by date or the number of associated timesheets.
- **Pagination**: Handles large datasets by displaying a limited number of service plans per page.
- **Interactive timesheet display**: Clicking on the number of timesheets will show a popup with detailed timesheet information.

## Installation Instructions

To set up the project, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/andrian06/platform_demo_024.git
   ```

2. Navigate into the project directory:
   ```bash
   cd PlatformDemo.Web
   ```

3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

4. Update the database:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

## Usage

After running the application, navigate to `/ServicePlans` to view the service plans. Use the search bar to filter by date and utilize the sorting options to arrange the service plans as needed.

## Code Structure

The application follows a clean architecture, with the following key components:

- **Data Layer**: Contains the `ApplicationDbContext` and entity models.
- **Controllers**: Manage requests and responses, including `ServicePlansController`.
- **Views**: Contain the UI components, including Razor views and partial views for displaying timesheets.

## Example Controller Code

Here’s a snippet from the `ServicePlansController` demonstrating the search and sorting logic:

```csharp
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

```
 