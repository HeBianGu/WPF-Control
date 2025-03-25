namespace H.Extensions.DataBase;

public interface IStringRepository<TEntity> : IRepository<TEntity, string> where TEntity : StringEntityBase
{

}
