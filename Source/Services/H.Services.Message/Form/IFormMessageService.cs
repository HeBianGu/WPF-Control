// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Form;
public interface IFormMessageService
{
    Task<bool?> ShowEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
    Task<bool?> ShowView<T>(T value, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null);
    Task<bool?> ShowTabEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
}
