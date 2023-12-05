using System.Collections;

namespace H.Providers.Ioc
{
    public interface IWhere
    {
        IEnumerable Where(IEnumerable from);
    }
}
