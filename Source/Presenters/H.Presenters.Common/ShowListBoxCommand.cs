// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.Common;

public class ShowListBoxCommand : ShowSourceCommandBase
{
    public string DisplayMemberPath { get; set; }
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowListBox(x =>
        {
            if (parameter is IEnumerable objects)
                x.ItemsSource = objects;
            else
                x.ItemsSource = this.ItemsSource;
            x.DisplayMemberPath = this.DisplayMemberPath;

            if (this._targetObject is FrameworkElement element)
            {
                var find = ItemsControlPresenter.GetItemTemplate(element);
                if (find != null)
                    x.ItemContentTemplate = find;
            }
        });
    }
}
