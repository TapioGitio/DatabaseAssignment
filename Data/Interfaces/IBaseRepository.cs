using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<bool> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> ReadAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? optionalExpression = null);
    Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? optionalExpression = null);
    Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> DoesItExistAsync(Expression<Func<TEntity, bool>> expression);
}