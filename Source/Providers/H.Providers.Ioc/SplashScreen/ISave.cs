// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public interface ISave
    {
        string Name { get; }
        bool Save(out string message);
    }
}