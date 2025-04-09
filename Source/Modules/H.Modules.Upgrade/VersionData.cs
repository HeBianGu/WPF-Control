// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Modules.Upgrade;

internal class VersionData
{
    public string Version { get; set; }
    public string Uri { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public List<string> Messages { get; set; } = new List<string>();
    public bool Force { get; set; } = false;
}