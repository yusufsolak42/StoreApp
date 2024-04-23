namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product {get; } //thats the way it talks to IProductRepository
        ICategoryRepository Category {get; }  //thats the way it talks to ICategoryRepository
        IOrderRepository Order {get; }  //thats the way it talks to IOrderRepository
        void Save(); //will be implemented in concrete class RepositoryManager
    }
}