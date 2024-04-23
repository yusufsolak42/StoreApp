using Entities.Models;
using Entities.RequestParameters;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product> //not generic anymore, only accepts product type.
    {
        IQueryable<Product> GetAllProducts(bool trackChanges); //one method, wants to return all products.
        IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p);
        IQueryable<Product> GetShowCaseProducts(bool trackChanges);
        Product? GetOneProduct(int id, bool trackChanges);

        void CreateProduct(Product product);
        void DeleteOneProduct(Product product);
        void UpdateOneProduct(Product entity);
    }
}