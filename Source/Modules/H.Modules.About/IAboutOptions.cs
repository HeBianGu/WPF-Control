// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Setting;

namespace H.Modules.About;

public interface IAboutOptions : ISettable
{
    string Agreement { get; set; }
    string AssemblyVersion { get; set; }
    string Company { get; set; }
    string Configuration { get; set; }
    string Copyright { get; set; }
    string Culture { get; set; }
    string FileVersion { get; set; }
    string Privacy { get; set; }
    string ProductDescription { get; set; }
    string ProductName { get; set; }
    string Trademark { get; set; }
    string Version { get; set; }
}