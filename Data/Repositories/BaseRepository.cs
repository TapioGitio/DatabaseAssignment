using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    //private readonly DataContext _context;
    //private readonly DbSet<TEntity> _dbset;

    //lägga in i ctor

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return null!;

        try
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failure creating this entity: {nameof(TEntity)}, {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        return await _dbset.FirstOrDefaultAsync(expression) ?? null!;
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity)
    {
        if (newEntity == null)
            return null!;

        try
        {
            var oldEntity = await _dbset.FirstOrDefault(expression) ?? null!;
            if (oldEntity == null)
                return null!;

            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
            return oldEntity;

        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Failure updating this entity: {nameof(TEntity)}, {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var oldEntity = await _dbset.FirstOrDefault(expression) ?? null!;
            if (oldEntity == null)
                return false;

            _dbset.Remove(oldEntity);
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
        return await _dbset.AnyAsync(expression);
    }

}
