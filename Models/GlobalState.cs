namespace bislerium_cafe_pos.Models
{
    public class GlobalState
    {
        public User CurrentUser { get; set; }
        public string AppBarTitle { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();

    }
}

