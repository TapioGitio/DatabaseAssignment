using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return false!;

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failure creating this entity: {nameof(TEntity)}, {ex.Message}");
            return false!;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        return await query.ToListAsync();
    }


    public virtual async Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        return await query.FirstOrDefaultAsync(expression);
    }   

    public virtual async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity)
    {
        if (newEntity == null)
            return false!;

        try
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(expression)
                ?? null!;

            if (oldEntity == null)
                return false!;

            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
            return true;

        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Failure updating this entity: {nameof(TEntity)}, {ex.Message}");
            return false!;
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
            return true;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Failure deleting this entity: {nameof(TEntity)}, {ex.Message}");
            return false!;
        }
    }

    public virtual async Task<bool> DoesItExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }


}
