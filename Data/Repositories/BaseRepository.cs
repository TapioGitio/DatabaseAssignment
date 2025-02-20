using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context, IMemoryCache cache) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected readonly IMemoryCache _cache = cache;
    protected IDbContextTransaction _transaction = null!;

    protected string GetCacheKey(string methodName, object? key = null) =>
        $"{nameof(TEntity)}_{methodName}_{key ?? "all"}";

    #region Transaction Logic
    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }
    public virtual async Task CommitTransactionAsync()
    {
        if(_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;

        }
    }
    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;

        }
    }
    #endregion

    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return false;

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            _cache.Remove(GetCacheKey(nameof(ReadAllAsync)));

            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failure creating this entity: {nameof(TEntity)}, {ex.Message}");
            return false;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
        IQueryable<TEntity> query = _dbSet;

        var cacheKey = GetCacheKey(nameof(ReadAllAsync));
        if (_cache.TryGetValue(cacheKey, out IEnumerable<TEntity>? cachedEntities))
            return cachedEntities!;


        if (includeExpression != null)
            query = includeExpression(query);

        var result = await query.ToListAsync();
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(4));

        return result;
    }

    public virtual async Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
        IQueryable<TEntity> query = _dbSet;

        var cacheKey = GetCacheKey(nameof(ReadAsync), expression.ToString());
        if (_cache.TryGetValue(cacheKey, out TEntity? cachedEntity))
            return cachedEntity!;

        if (includeExpression != null)
            query = includeExpression(query);

        var result = await query.FirstOrDefaultAsync(expression);
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(4));

        return result;
    }

    public virtual async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity)
    {
        if (newEntity == null)
            return false;

        try
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(expression)
                ?? null!;

            if (oldEntity == null)
                return false;

            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();

            _cache.Remove(GetCacheKey(nameof(ReadAllAsync)));

            return true;

        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Failure updating this entity: {nameof(TEntity)}, {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
            if (oldEntity == null)
                return false;

            _dbSet.Remove(oldEntity);
            await _context.SaveChangesAsync();

            _cache.Remove(GetCacheKey(nameof(ReadAllAsync)));

            return true;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Failure deleting this entity: {nameof(TEntity)}, {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> DoesItExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
}
