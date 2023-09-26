namespace VeggiTales.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // create child ref to Product model (1 Category / Many Products)
        public List<Product> Products { get; set; }
    }
}
