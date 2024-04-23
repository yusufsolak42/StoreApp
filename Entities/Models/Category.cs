namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } = String.Empty;

        public ICollection<Product> Products { get; set; } //collection navigation property(like one collection has a collection of products)
    }
}