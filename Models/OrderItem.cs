namespace bislerium_cafe_pos.Models
{
    // Represents an item within a cafe order.
    public class OrderItem
    {
        // Gets or sets a unique identifier for the order item.
        public Guid OrderItemID { get; set; } = Guid.NewGuid();

        // Gets or sets the identifier of the associated item.
        public Guid ItemID { get; set; }

        // Gets or sets the name of the item.
        public string ItemName { get; set; }

        // Gets or sets the type of the item (e.g., coffee, add-in).
        public string ItemType { get; set; }

        // Gets or sets the quantity of the item in the order.
        public int Quantity { get; set; }

        // Gets or sets the price of a single unit of the item.
        public double Price { get; set; }

        // Gets or sets the total price of the item in the order (Quantity * Price).
        public double TotalPrice { get; set; }
    }
}
