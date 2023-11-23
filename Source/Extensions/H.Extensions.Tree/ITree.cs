using System;
using System.Collections;
using System.Windows.Markup;

namespace H.Extensions.Tree
{
    public interface ITree
    {
        IEnumerable GetChildren(object parent);
    }
}
