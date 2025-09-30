// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message;

public static class DialogExtension
{
    public static string Success(this IDialog dialog)
    {
        return "Success";
    }
    public static string Error(this IDialog dialog)
    {
        return "Error";
    }

    public static string Sumit(this IDialog dialog)
    {
        return "Sumit";
    }

    public static string Cancel(this IDialog dialog)
    {
        return "Cancel";
    }

    public static string Close(this IDialog dialog)
    {
        return "Close";
    }
}
