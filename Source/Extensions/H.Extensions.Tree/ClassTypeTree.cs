using System.Collections;

namespace H.Extensions.Tree;

public class ClassTypeTree : ITree, IParent
{
    private readonly Type _type;
    public ClassTypeTree(Type type)
    {
        this._type = type;
    }

    public IEnumerable GetChildren(object parent)
    {
        if (parent == null)
        {
            return new Type[] { this._type };
        }
        if (parent is Type type)
        {
            return type.Assembly.GetTypes().Where(x => x.BaseType == type);
        }
        return Enumerable.Empty<string>();
    }

    public object GetParent(object current)
    {
        if (current is Type type)
            return type.BaseType;
        return null;
    }
}
