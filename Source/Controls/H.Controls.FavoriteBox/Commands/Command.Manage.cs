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
    [Display(Name = "管理收藏夹", Description = "显示管理收藏夹页面")]
    public class ManageFavoriteCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var ioc = Ioc.GetService<IFavoriteService>();
            var favorite = new FavoritesPresenter();
            var temp = ioc.Collection.Where(x => x.GroupName == parameter?.ToString()).ToList();
            favorite.Collection = temp.ToObservable();
            var r = await IocMessage.Dialog.Show(favorite);
            if (r != true)
                return;
            foreach (var item in temp)
            {
                if (favorite.Collection.Contains(item))
                    continue;
                ioc.Delete(item);
            }
            ioc.Save(out string message);
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>();
        }
    }
}
