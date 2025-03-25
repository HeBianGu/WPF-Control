namespace H.Extensions.DataBase;

public interface IEntityBase<TPrimaryKey>
{
    TPrimaryKey ID { get; set; }
}