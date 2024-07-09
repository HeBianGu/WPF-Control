// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class IocUpgrade : Ioc<IUpgradeService>
    {
        public static string UpgradeVersion { get; set; } = Instance?.UpgradeVersion;
        public static bool HasNewVersion { get; set; } = Instance?.CanUpgrade(out string message) != null;
        public static string CurrentVersion { get; set; } = Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}