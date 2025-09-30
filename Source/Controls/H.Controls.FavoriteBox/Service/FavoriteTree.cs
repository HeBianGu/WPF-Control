// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

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
