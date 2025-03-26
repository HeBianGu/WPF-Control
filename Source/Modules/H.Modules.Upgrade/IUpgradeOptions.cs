// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


namespace H.Modules.Upgrade;

public interface IUpgradeOptions
{
    bool AutomaticUpgrade { get; set; }
    bool CheckUpdateOnStart { get; set; }
    string LoadFormat { get; set; }
    bool NotifyUpgrade { get; set; }
    string SavePath { get; set; }
    string Uri { get; set; }
    bool UseIEDownload { get; set; }
}