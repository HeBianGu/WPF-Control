// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Identity.Author;

public interface IAuthority
{
    string ID { get; }
    string Name { get; }
    string GroupName { get; }
    bool IsAuthority { get; }
}