// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.AppPath;

public static class AppPathExtension
{
    public static bool ClearCache(this IAppPathServce servce, out string message)
    {
        try
        {
            Directory.Delete(servce.Cache, true);
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 清空设置
    /// </summary>
    /// <returns>清空是否成功</returns>
    public static bool ClearSetting(this IAppPathServce servce, out string message)
    {
        try
        {
            if (Directory.Exists(servce.Setting))
                Directory.Delete(servce.Setting, true);
            if (Directory.Exists(servce.UserSetting))
                Directory.Delete(servce.UserSetting, true);
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }
}
