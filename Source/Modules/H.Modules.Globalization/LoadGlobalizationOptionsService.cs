// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Common;
using H.Services.Logger;

namespace H.Modules.Globalization;

public class LoadGlobalizationOptionsService : ILoadGlobalizationOptionsService
{
    public bool Load(out string message)
    {
        try
        {
            return GlobalizationOptions.Instance.Load(out message);
        }
        catch (Exception ex)
        {
            message = ex.Message;
            IocLog.Error(ex);
            return false;
        }
    }
}
