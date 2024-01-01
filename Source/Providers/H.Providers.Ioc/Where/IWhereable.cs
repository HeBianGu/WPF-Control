using System.Collections;

namespace H.Providers.Ioc
{
    public interface IWhereable
    {
        IEnumerable Where(IEnumerable from);
    }
}
