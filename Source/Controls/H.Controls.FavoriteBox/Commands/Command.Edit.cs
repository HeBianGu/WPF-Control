// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Common.Commands;
global using H.Services.Message;
global using System.ComponentModel.DataAnnotations;
global using System.IO;
namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "编辑", Description = "显示编辑收藏夹页面")]
    public class EditFavoriteCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is IFavoriteItem favoriteItem)
            {
                var service = Ioc.GetService<IFavoriteService>();
                string parentPath = Path.GetDirectoryName(favoriteItem.Path);
                string name = Path.GetFileNameWithoutExtension(favoriteItem.Path);
                favoriteItem.Path = name;
                var r = await IocMessage.Form.ShowEdit(favoriteItem);
                if (r != true)
                {
                    favoriteItem.Path = Path.Combine(parentPath, name);
                    return;
                }
                if (!string.IsNullOrEmpty(parentPath))
                    favoriteItem.Path = Path.Combine(parentPath, favoriteItem.Path);
                service.Save(out string message);
            }

        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>() && parameter is IFavoriteItem;
        }
    }
}
