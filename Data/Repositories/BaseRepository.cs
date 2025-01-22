﻿using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return null!;

        try
        {
            await _dbSet.AddAsync(entity);
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
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        return await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity)
    {
        if (newEntity == null)
            return null!;

        try
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(expression)
                ?? null!;

            if (oldEntity == null)
                return null!;

            _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
            return oldEntity!;

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
