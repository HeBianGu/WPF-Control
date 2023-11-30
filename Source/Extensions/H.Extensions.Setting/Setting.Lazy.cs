// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Extensions.Setting
{
    public abstract class LazySettingInstance<T> : SettingBase where T : new()
    {
        public static T Instance = new Lazy<T>(() => new T()).Value;
    }
}
