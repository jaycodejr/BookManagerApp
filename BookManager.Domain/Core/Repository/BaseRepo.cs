using BookManager.Domain.Core.IRepository;
using BookManager.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BookManager.Domain.Core.Repository
{
    public class BaseRepo<TEntity, TKey> : IBaseRepo<TEntity, TKey> where TEntity : class
    {
        protected BookDbContext _context;
        protected ILogger _logger;
        private readonly DbSet<TEntity> _table;

        public BaseRepo(BookDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _table = _context.Set<TEntity>();
        }
        public async Task<bool> Add(TEntity entity)
        {
            try
            {
                await _table.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(TEntity));
                return false;
            }
        }

        public async Task<bool> AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                await _table.AddRangeAsync(entities);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} AddRange method error", typeof(TEntity));
                return false;
            }
        }

        public async Task<TEntity?> Find(TKey? key)
        {
            try
            {
                return await _table.FindAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Find with key method error", typeof(TEntity));
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _table.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Find with pedicate method error", typeof(TEntity));
                return new List<TEntity>();
            }
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            try
            {
                return await _table.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FindAll method error", typeof(TEntity));
                return new List<TEntity>();
            }
        }

        public async Task<bool> Remove(TEntity entity)
        {
            try
            {
                await Task.Run(() => _table.Remove(entity));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Remove method error", typeof(TEntity));
                return false;
            }
        }

        public async Task<bool> RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                await Task.Run(() => _table.RemoveRange(entities));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} RemoveRange method error", typeof(TEntity));
                return false;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                await Task.Run(() => _table.Update(entity));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(TEntity));
                return false;
            }
        }
    }
}
