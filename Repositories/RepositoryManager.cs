using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context; //the bottom of the DAL.
        private readonly IProductRepository _productRepository; //we can access to the repos.
        private readonly ICategoryRepository _categoryRepository; //we can access to the repos.
        private readonly IOrderRepository _orderRepository; //we can access to the repos.

        public RepositoryManager(IProductRepository productRepository,
        RepositoryContext context,
        ICategoryRepository categoryRepository,
        IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public IOrderRepository Order => _orderRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}