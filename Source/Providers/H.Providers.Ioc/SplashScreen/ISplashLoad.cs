// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public interface ISplashLoad : ILoad
    {
        string Name { get; }
    }

    public interface ILoad
    {
        bool Load(out string message);
    }
}