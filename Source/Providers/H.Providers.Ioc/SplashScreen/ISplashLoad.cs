// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public interface ISplashLoad
    {
        string Name { get; }
        bool Load(out string message);
    }
}