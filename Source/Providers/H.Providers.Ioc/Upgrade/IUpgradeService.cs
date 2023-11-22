// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public interface IUpgradeService
    {
        bool CanUpgrade(out string message);
        bool Upgrade(out string message);
        string UpgradeVersion { get; }
    }
}