// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.TypeLicense.LicenseProviders;

public interface IValidLicense
{
    bool IsValid<T>(out string message);
}
public class ValidTypeLicense : TypeLicense, IValidLicense
{
    public ValidTypeLicense()
    {

    }
    public ValidTypeLicense(string key) : base(key)
    {

    }

    public string TypeName { get; set; }

    public virtual bool IsValid<T>(out string message)
    {
        message = null;
        if (typeof(T).Name != this.TypeName)
        {
            message = "许可验证失败，许可类型不匹配";
            return false;
        }
        return true;
    }
}
