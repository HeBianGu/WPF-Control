using System.Linq.Expressions;

namespace H.Extensions.DataBase;

public interface IRepository
{

}

public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : EntityBase<TPrimaryKey>
{
    Task<List<TEntity>> GetListAsync();
    List<TEntity> GetList();
    List<TEntity> GetList(Action<IQueryable<TEntity>> action);
    Task<List<TEntity>> GetListAsync(params string[] includes);
    List<TEntity> GetList(params string[] includes);
    List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIDAsync(TPrimaryKey id);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> InsertAsync(TEntity entity, bool autoSave = true);
    int Insert(TEntity entity, bool autoSave = true);
    Task<int> InsertRangeAsync(params TEntity[] entity);
    Task<int> UpdateAsync(TEntity entity, bool autoSave = true);
    int Update(TEntity entity, bool autoSave = true);
    Task<int> InsertOrUpdateAsync(TEntity entity, bool autoSave = true);
    Task<int> DeleteAsync(TEntity entity, bool autoSave = true);
    int Delete(TEntity entity, bool autoSave = true);
    Task<int> ClearAsync(bool autoSave = true);
    int Clear(bool autoSave = true);
    Task<int> DeleteByIDAsync(TPrimaryKey id, bool autoSave = true);
    int DeleteByID(TPrimaryKey id, bool autoSave = true);
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true);
    Task<Tuple<List<TEntity>, int>> LoadPageList(int startPage, int pageSizet, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);
    Task<int> SaveAsync();
    int Save();

}

public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : GuidEntityBase
{

}
