using Entities.Models;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId) //we extend the Products list
        {
            if(categoryId is null) //if categoryId is null, no queryString
                    return products; 
            else  //if there's query String (CategoryId)
                    return products.Where(prd =>prd.CategoryId.Equals(categoryId)); //go and bring the products where categoryId of each products is the same as the queryString categoryId.
        }

        public static IQueryable<Product> FilteredBySearchTerm (this IQueryable<Product> products, String? searchTerm) //we extend the Products list
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return products;
            else
                return products.Where(prd => prd.ProductName.ToLower()
                    .Contains(searchTerm.ToLower())); //check if the searchTerm contains any ProductName.
            

        }

        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int minPrice, int maxPrice, bool isValidPrice) //we extend the products and filter by price.
        {
            if(isValidPrice)
                return products.Where(prd => prd.Price>minPrice && prd.Price<= maxPrice); //check if the price coming from the form is in the range.
            else
            return products; //if not valid, then return all products
                //End points help us to talk with the Database, aka server.
        }
    
    
    public static IQueryable<Product> ToPaginate(this IQueryable<Product> products,int pageNumber, int pageSize)
    {
        return products
        .Skip((pageNumber-1)-pageSize)
        .Take(pageSize);
    }

    } 
}