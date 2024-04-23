using Entities.Models;
using Repositories;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories (bool trackChanges);
    }
} 