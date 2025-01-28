using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> ReadAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? optionalExpression = null);
    Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? optionalExpression = null);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> DoesItExistAsync(Expression<Func<TEntity, bool>> expression);
}