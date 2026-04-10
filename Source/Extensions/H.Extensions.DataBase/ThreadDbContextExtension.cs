// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.DataBase;

public static class ThreadDbContextExtension
{
    private static readonly object s_dbContextLock = new object();
    public static void LockInvoke<T>(this T repository, Action<T> action) where T : IRepository
    {
        lock (s_dbContextLock)
        {
            action?.Invoke(repository);
        }
    }
}