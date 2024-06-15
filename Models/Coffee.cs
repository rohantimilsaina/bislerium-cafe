namespace bislerium_cafe_pos.Models
{
    public class Coffee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CoffeeType { get; set; }
        public double Price { get; set; }

    }
}

