// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Common.Interfaces;

/// <summary>
/// 项目启动会预加载的接口
/// </summary>
public interface ISplashLoad : ILoadable
{
    string Name { get; }
}