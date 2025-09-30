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
    [Display(Name = "插入", Description = "显示插入收藏页面")]
    public class InsertFavoriteCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is IFavoriteItem favoriteItem)
            {
                string parentPath = favoriteItem.Path;
                var service = Ioc.GetService<IFavoriteService>();
                var favorite = service.Create();
                var r = await IocMessage.Form.ShowEdit(favorite);
                if (r != true)
                    return;
                if (parentPath != null)
                    favorite.Path = Path.Combine(parentPath, favorite.Path);
                service.Add(favorite);
                service.Save(out string message);
            }

        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>() && parameter is IFavoriteItem;
        }
    }
}
