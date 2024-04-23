using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }
        [Required(ErrorMessage = "Product Name is required.")]
        public String? ProductName { get; init; } = String.Empty;

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; init; }

        public String? Summary { get; init; } = String.Empty; //initial value will be empty. i wrote init because i'll initialize once it's created.

        public String? ImageUrl { get; set; }

        public int? CategoryId { get; init; } //Foreign Key

    }
}