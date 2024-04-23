using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p=>p.ProductName).IsRequired();
            builder.Property(p=>p.Price).IsRequired();



            builder.HasData( //Seed data, when the database is created, this table will be inserted initially.
                new Product() { ProductId = 1, CategoryId = 2, ImageUrl="/images/1.jpg", ProductName = "Computer", Price = 17_000, ShowCase = false},
                new Product() { ProductId = 2, CategoryId = 2, ImageUrl="/images/2.jpg", ProductName = "Keyboard", Price = 9_000, ShowCase = false},
                new Product() { ProductId = 3, CategoryId = 2, ImageUrl="/images/3.jpg", ProductName = "Monitor", Price = 3_000, ShowCase = false},
                new Product() { ProductId = 4, CategoryId = 2, ImageUrl="/images/4.jpg", ProductName = "Mouse", Price = 4_000, ShowCase = false},
                new Product() { ProductId = 5, CategoryId = 2, ImageUrl="/images/5.jpg", ProductName = "Deck", Price = 1_000, ShowCase = false},
                new Product() { ProductId = 6, CategoryId = 1, ImageUrl="/images/6.jpg", ProductName = "Harry Potter", Price = 400, ShowCase = false},
                new Product() { ProductId = 7, CategoryId = 1, ImageUrl="/images/7.jpg", ProductName = "Actualized", Price = 250, ShowCase = false},
                new Product() { ProductId = 8, CategoryId = 1, ImageUrl="/images/8.jpg", ProductName = "VR glasses", Price = 550, ShowCase = true},
                new Product() { ProductId = 9, CategoryId = 2, ImageUrl="/images/9.jpg", ProductName = "Reality Key", Price = 7800, ShowCase = true},
                new Product() { ProductId = 10, CategoryId = 1, ImageUrl="/images/10.jpg", ProductName = "Battery X", Price = 9000, ShowCase = true}
            );
        }
    }
}