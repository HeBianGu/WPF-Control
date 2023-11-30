// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public interface IAuthority
    {
        string ID { get; }
        string Name { get; }
        string GroupName { get; }
        bool IsAuthority { get; }
    }
}