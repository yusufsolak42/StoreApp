using System.Linq;
using Entities.Models;
using Entities.RequestParameters; //for parameters methods.
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context) //because the base class RepositoryBase<T> needs a parameter (DbContext) 
        {

        }

        public void CreateProduct(Product product) => Create(product); //we send it directly to baseRepo. No need to add something.

        public void DeleteOneProduct(Product product) => Remove(product);


        public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges); //same as  return FindAll(trackChanges);  => is same as return.. "FindAll" comes from RepositoryBase

        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p) //this comes from the index method to productController
        {

            return _context
                    .Products //we can find these methods there cuz we extended Product.
                    .FilteredByCategoryId(p.CategoryId) //this method can be found as extension method.if it's empty, it will return all the products
                    .FilteredBySearchTerm(p.SearchTerm) //extension method. if its empty, it will return all the products
                    .FilteredByPrice(p.MinPrice, p.MaxPrice, p.IsValidPrice) //extension method. if its empty, it will return all the products
                    .ToPaginate(p.PageNumber, p.PageSize);



            //old code, without extensions
            //return p.CategoryId is null
            //? _context //if true
            // .Products
            // .Include(prd => prd.Category) //if there's no parameter, go to the context, go to products, bring all categories one by one.
            // : _context  //if false, if there's a parameter (CategoryId), go to Products, take all categories of the products, where all the categories of the products are equal the one coming as request parameter (CategoryId as query string)
            //.Products
            //.Include(prd => prd.Category)
            // .Where(prd => prd.CategoryId.Equals(p.CategoryId));

        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return FindByCondition(p => p.ProductId.Equals(id), trackChanges); //it has the function in repobase.
        }

        public IQueryable<Product> GetShowCaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges)
                 .Where(p => p.ShowCase.Equals(true));
        }

        public void UpdateOneProduct(Product entity) => Update(entity); //we send directly to the base.

    }
}