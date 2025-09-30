// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Presenters.Common;

public interface IPasswordBoxPresenter
{
    string Password { get; set; }
}

[Icon("\xE875")]
public class PasswordBoxPresenter : DisplayBindableBase, IPasswordBoxPresenter
{
    public string Password { get; set; }
}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowPasswordBox(this IDialogMessageService service, Action<IPasswordBoxPresenter> option, Action<IPasswordBoxPresenter> sumitAction, Action<IDialog> builder = null, Func<IPasswordBoxPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog<PasswordBoxPresenter>(option, sumitAction, builder, canSumit);
    }

    public static async Task<bool?> ShowPasswordBox(this IDialogMessageService service, string password, Action<string> sumitAction, Action<IDialog> builder = null, Func<IPasswordBoxPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowPasswordBox(x => x.Password = password, x => sumitAction?.Invoke(x.Password), builder, canSumit);
    }
}
