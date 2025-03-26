// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Setting;

public abstract class LazySettableInstance<T> : SettableBase where T : new()
{
    public static T Instance = new Lazy<T>(() => new T()).Value;
}
