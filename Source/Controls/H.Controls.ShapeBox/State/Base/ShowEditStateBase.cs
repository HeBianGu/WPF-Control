// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;
using H.Services.Message;

namespace H.Controls.ShapeBox.State.Base
{
    public abstract class ShowEditStateBase : StateBase
    {
        public RelayCommand EditCommand => new RelayCommand(x =>
        {
            this.ShowEdit();
        });

        public virtual async void ShowEdit()
        {
           await IocMessage.Form?.ShowEdit(this);
        }
    }
}
