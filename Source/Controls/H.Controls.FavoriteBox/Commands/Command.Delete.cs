// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "删除", Description = "删除收藏")]
    public class DeleteFavoriteCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is IFavoriteItem favoriteItem)
            {
                var service = Ioc.GetService<IFavoriteService>();
                service.Delete(favoriteItem);
                var children = service.Collection.Where(x => Path.GetDirectoryName(x.Path) == favoriteItem.Path).ToArray();
                service.Delete(children);
                service.Save(out string message);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>() && parameter is IFavoriteItem;
        }
    }
}
