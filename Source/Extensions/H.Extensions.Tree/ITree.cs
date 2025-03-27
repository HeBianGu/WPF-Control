using System.Collections;

namespace H.Extensions.Tree;

public interface ITree
{
    IEnumerable GetChildren(object parent);
}
