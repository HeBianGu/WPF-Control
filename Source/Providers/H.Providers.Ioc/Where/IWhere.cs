using System.Collections;
using System.Collections.Generic;

namespace H.Providers.Ioc
{
    public interface IWhere
    {
        IEnumerable Where(IEnumerable from);
    }
}
