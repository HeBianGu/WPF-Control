namespace H.Providers.Ioc
{
    public interface IEntityBase<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
    }
}