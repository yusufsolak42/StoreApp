using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T> //for all repositories. 
    {
        IQueryable<T>FindAll(bool trackChanges); //to increase the performance of ef (trackChanges) .This method will be used by all repos.It's generic because it can be used by different classes, different data types.
        T? FindByCondition(Expression<Func<T,bool>> expression, bool trackChanges); //T? because it can be category or product, so, generic.

        void Create(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
 

}