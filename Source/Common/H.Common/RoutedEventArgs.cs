// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
