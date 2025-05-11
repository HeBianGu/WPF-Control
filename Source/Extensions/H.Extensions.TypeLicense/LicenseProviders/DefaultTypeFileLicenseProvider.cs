// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.TypeLicense.LicenseProviders;

public class DefaultTypeFileLicenseProvider : TypeFileLicenseProviderBase
{
    const string Format = "{0} is a licensed component.";
    protected override License CreateLicense(string key, Type type)
    {
        if (key.StartsWith(string.Format(Format, type.Name)))
            return new TypeLicense(key);
        return null;
    }
}
