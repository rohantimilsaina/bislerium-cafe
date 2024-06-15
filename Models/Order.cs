namespace bislerium_cafe_pos.Models
{
    // Represents a cafe order with associated details.
    public class Order
    {
        // Gets or sets a unique identifier for the order.
        public Guid OrderID { get; set; } = Guid.NewGuid();

        // Gets or sets the identifier of the customer placing the order.
        public Guid CustomerID { get; set; }

        // Gets or sets the name of the customer placing the order.
        public string CustomerName { get; set; }

        // Gets or sets the phone number of the customer placing the order.
        public string CustomerPhoneNum { get; set; }

        // Gets or sets the username of the employee processing the order.
        public string EmployeeUserName { get; set; }

        // Gets or sets the date and time when the order was placed.
        public DateTime OrderDateTime { get; set; } = DateTime.Now;

        // Gets or sets the list of individual items included in the order.
        public List<OrderItem> OrderItems { get; set; }

        // Gets or sets the total amount for the order before any discounts.
        public double OrderTotalAmount { get; set; }

        // Gets or sets the discount amount applied to the order.
        public double DiscountAmount { get; set; } = 0;
    }
}
