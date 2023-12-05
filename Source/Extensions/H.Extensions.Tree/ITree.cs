using System.Collections;

namespace H.Extensions.Tree
{
    public interface ITree
    {
        IEnumerable GetChildren(object parent);
    }

    public interface IParent
    {
        object GetParent(object current);
    }
}
