using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace H.Extensions.DataBase;

public abstract class EntityBase<TPrimaryKey> : IEntityBase<TPrimaryKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Browsable(false)]
    [ReadOnly(true)]
    [Column("id", Order = 0)]
    public virtual TPrimaryKey ID { get; set; }
}