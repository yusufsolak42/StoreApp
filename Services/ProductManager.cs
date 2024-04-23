using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager; //dependency Injection, so we can use the repos.
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper) // we ask for a repositoryManager object to construct product manager.
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Product product = _mapper.Map<Product>(productDto); 

            _manager.Product.Create(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            Product product = GetOneProduct(id, false); //we get one product (we call with the ID we have.), and delete it
            if (product is not null)
            {
                _manager.Product.DeleteOneProduct(product); //we go to the repository manager and use DeleteOneProduct method.
                _manager.Save();
            }
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _manager.Product.GetAllProductsWithDetails(p);
        }

        public IEnumerable<Product> GetLatestProducts(int n, bool trackChanges)
        {
            return _manager.Product
                .FindAll(trackChanges)
                .OrderByDescending(prd => prd.ProductId)
                .Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
                throw new Exception("Product Not Found");
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product = GetOneProduct(id, trackChanges);
            var productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;

        }

        public IEnumerable<Product> GetShowCaseProducts(bool trackChanges)
        {
            var products = _manager.Product.GetShowCaseProducts(trackChanges);
            return products;
        }

        public void UpdateOneProduct(ProductDtoForUpdate productDto)
        {
            //var entity = _manager.Product.GetOneProduct(productDto.ProductId, true); //entity we fetched from our database.
            //entity.ProductName = productDto.ProductName;
            //entity.Price = productDto.Price;
            //entity.CategoryId = productDto.CategoryId;
            var entity = _mapper.Map<Product>(productDto); //the source, and destination. 
            _manager.Product.UpdateOneProduct(entity);  //EF wont track it so we need to define a save method in repo level.
            _manager.Save();                            //we create another update method in repo level.
        }
    }
}