using System.Linq.Expressions;

namespace BookManager.Domain.Core.IRepository
{
    public interface IBaseRepo<TEntity,TKey> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<bool> AddRange(IEnumerable<TEntity> entities);

        Task<TEntity?> Find(TKey? key);
        Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity,bool>> predicate);
        Task<IEnumerable<TEntity>> FindAll();

        Task<bool> Remove(TEntity entity);
        Task<bool> RemoveRange(IEnumerable<TEntity> entities);

        Task<bool> Update(TEntity entity);
    }
}
