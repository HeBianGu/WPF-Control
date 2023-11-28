using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace H.Providers.Ioc
{
    public interface IEntityBase<TPrimaryKey>
    {
        TPrimaryKey ID { get; set; }
    }
}