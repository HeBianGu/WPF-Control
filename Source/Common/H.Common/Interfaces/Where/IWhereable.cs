global using System.Collections;

namespace H.Common.Interfaces.Where;

public interface IWhereable
{
    IEnumerable Where(IEnumerable from);
}
