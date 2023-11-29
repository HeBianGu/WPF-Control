using System;
using System.Collections.Generic;

namespace H.Extensions.Tree
{
    public static class ParentExtension
    {
        public static IEnumerable<object> GetParent(this IParent tree, object current)
        {
            var result = new List<object>();
            Action<object> getParent = null;
            getParent = x =>
             {
                 var parent = tree.GetParent(x);
                 result.Add(parent);
                 if (parent != null)
                     getParent(parent);
             };
            getParent(current);
            return result;
        }
    }

}
