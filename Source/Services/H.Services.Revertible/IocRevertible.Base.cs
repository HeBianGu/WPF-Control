// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Revertible;

public abstract class IocRevertibleBase<T> : Ioc<T> where T : IRevertibleService
{
    public static void Commit(Action redo, Action undo, string name = null, object data = null, bool autoDo = true)
    {
        if (autoDo)
            redo?.Invoke();
        if (Instance == null)
            return;
        using (Instance.Begin(name, data))
        {
            Instance.Current.AddAction(redo, undo);
        }
    }
}
