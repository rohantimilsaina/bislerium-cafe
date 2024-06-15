using System;
using System.Collections.Generic;

namespace bislerium_cafe_pos.Models
{
    // Represents a report containing information about cafe sales and transactions.
    public class Report
    {
        // Gets or sets the list of orders included in the report.
        public List<Order> Orders { get; set; }

        // Gets or sets the type of the report (e.g., Daily, Monthly).
        public string ReportType { get; set; }

        // Gets or sets the date for which the report is generated.
        public string ReportDate { get; set; }

        // Gets or sets the total revenue calculated from the orders in the report.
        public double TotalRevenue { get; set; } = 0;

        // Gets or sets the list of coffee items included in the report.
        public List<OrderItem> CoffeeList { get; set; }

        // Gets or sets the list of add-ins items included in the report.
        public List<OrderItem> AddInsList { get; set; }
    }
}
