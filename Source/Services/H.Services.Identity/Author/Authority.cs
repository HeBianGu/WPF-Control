// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Identity.Author;

public class Authority : IAuthority
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string GroupName { get; set; }
    public bool IsAuthority { get; set; }
}