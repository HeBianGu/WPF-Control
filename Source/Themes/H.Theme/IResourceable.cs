// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Themes;

public interface IResourceable
{
    string Name { get; }
    ResourceDictionary Resource { get; }
}
