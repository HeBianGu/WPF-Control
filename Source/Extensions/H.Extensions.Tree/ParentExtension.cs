namespace H.Extensions.Tree;

public static class ParentExtension
{
    public static IEnumerable<object> GetParent(this IParent tree, object current)
    {
        List<object> result = new List<object>();
        Action<object> getParent = null;
        getParent = x =>
         {
             object parent = tree.GetParent(x);
             result.Add(parent);
             if (parent != null)
                 getParent(parent);
         };
        getParent(current);
        return result;
    }
}
