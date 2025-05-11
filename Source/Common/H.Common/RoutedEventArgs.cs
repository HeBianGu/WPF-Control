// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace System.Windows;

public class RoutedEventArgs<T> : RoutedEventArgs
{
    public RoutedEventArgs(T entity)
    {
        this.Entity = entity;
    }

    public RoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
    {
    }
    public RoutedEventArgs(RoutedEvent routedEvent, object source, T entity) : base(routedEvent, source)
    {
        this.Entity = entity;
    }

    public T Entity { get; set; }
}
