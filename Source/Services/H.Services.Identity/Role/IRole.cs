// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Identity.Role;

public interface IRole
{
    string ID { get; }
    string Name { get; set; }
    bool IsValid(string authorId);
}