// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message;

public static class FormMessageExtension
{
    public static Task<bool?> ShowEdit<T>(this IFormMessageService formMessage, T value, Predicate<T> match, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null)
    {
        return formMessage.ShowEdit(value, action, match, option, owner);
    }

}
