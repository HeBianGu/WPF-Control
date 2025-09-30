// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.TypeLicense.LicenseProviders;

namespace H.Extensions.TypeLicense;

public class TypeLicenseService : ITypeLicenseService
{
    public bool IsValid<T>(out string message)
    {
        message = null;
        bool result = LicenseManager.IsValid(typeof(T), null, out License license);
        if (!result || license == null)
        {
            message = $"检查许可错误<{typeof(T).Name}>";
            return false;
        }
        if (license is IValidLicense validLicense)
            result = validLicense.IsValid<T>(out message);
        license.Dispose();
        return result;
    }
}
