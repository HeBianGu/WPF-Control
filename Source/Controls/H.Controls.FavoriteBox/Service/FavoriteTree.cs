using H.Extensions.Tree;
using System.Collections;
using System.IO;
using System.Linq;

namespace H.Controls.FavoriteBox
{
    public class FavoriteTree : ITree
    {
        public IEnumerable GetChildren(object parent)
        {
            if (parent == null)
                return IocFavoriteService.Instance.Collection.Where(x => !x.Path.Trim('\\').Contains(System.IO.Path.DirectorySeparatorChar));
            if (parent is IFavoriteItem directory)
                return IocFavoriteService.Instance.Collection.Where(x => Path.GetDirectoryName(x.Path) == directory.Path);
            return Enumerable.Empty<string>();
        }
    }
}
