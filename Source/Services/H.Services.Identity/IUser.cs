// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Identity;

public interface IUser
{
    string ID { get; }
    string Account { get; set; }
    string Password { get; set; }
    string Name { get; set; }
    IRole Role { get; }
    bool IsValid(string authorId);
}

public static class UserExtension
{
    public static bool IsInAuthor(this IUser user, string authorId)
    {
        if (user.Role?.Authors == null)
            return false;
        return user.Role.Authors.Any(x => x.ID == authorId);
    }

    public static bool IsInRole(this IUser user, string roleId)
    {
        if (user.Role == null)
            return false;
        return user.Role.ID == roleId;
    }
}