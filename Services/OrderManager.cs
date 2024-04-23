using Entities.Models;
using Repositories.Contracts;

namespace Services.Contracts
{
    public class OrderManager : IOrderService
    {

        private readonly IRepositoryManager _manager;

        public OrderManager(IRepositoryManager manager) //with dependency injection, we can use all methods defined in repository 
        {
            _manager = manager;
        }

        public IQueryable<Order> Orders => _manager.Order.Orders;

        public int NumberOfInProcess => _manager.Order.NumberOfInProcess;

        public void Complete(int id)
        {
            _manager.Order.Complete(id);
            _manager.Save();
        }

        public Order? GetOneOrder(int id)
        {
            return _manager.Order.GetOneOrder(id); //we go to the interface, access to the orderRepository, use the same method as here.(GetOneOrder)
        }

        public void SaveOrder(Order order)
        {
            _manager.Order.SaveOrder(order); //we give the coming parameter to the method in repo.
        }
    }
}