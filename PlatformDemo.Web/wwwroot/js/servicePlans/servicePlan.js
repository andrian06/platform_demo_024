$(document).on('click', '.view-timesheets', function (e) {
    e.preventDefault();
    var servicePlanId = $(this).data('serviceplan-id');

    // Perform an AJAX call to fetch timesheet details
    $.ajax({
        url: '/ServicePlans/GetTimesheets', // Controller action that returns timesheet data
        type: 'GET',
        data: { servicePlanId: servicePlanId },
        success: function (data) {
            // Populate the modal body with the returned data
            $('#timesheetDetails').html(data);
        },
        error: function () {
            $('#timesheetDetails').html('<p>Error loading timesheets</p>');
        }
    });
});
