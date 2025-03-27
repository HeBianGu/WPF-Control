namespace H.Extensions.DataBase;

public abstract class GuidEntityBase : EntityBase<Guid>
{
    public GuidEntityBase()
    {
        this.ID = Guid.NewGuid();
    }
}
