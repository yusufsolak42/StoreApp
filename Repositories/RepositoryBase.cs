using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new()
    {
        protected readonly RepositoryContext _context; //depedency injection

        protected RepositoryBase(RepositoryContext context) //we inject the dependency to the constructor, so when an instance is created, we need to give a "RepositoryContext" object.
        {
            _context = context; //this will connect us to the database.
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity); 
        }

        public IQueryable<T> FindAll(bool trackChanges) //brings all the records.
        {
            return trackChanges
                 ? _context.Set<T>() //if true, if we want to track the changes
                 : _context.Set<T>().AsNoTracking(); //if false, if we wont track the changes, we use the key word AsNoTracking()
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
              ? _context.Set<T>().Where(expression).SingleOrDefault()
              : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
               
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}