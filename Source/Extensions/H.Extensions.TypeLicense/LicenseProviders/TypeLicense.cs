// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json.Serialization;

namespace H.Extensions.TypeLicense.LicenseProviders;

public interface ITypeLicense
{
    string LicenseKey { get; }
}

public class TypeLicense : License, ITypeLicense
{
    public TypeLicense()
    {

    }
    public TypeLicense(string key)
    {
        this.LicenseKey = key;
    }

    [JsonIgnore]
    public override string LicenseKey { get; }

    public override void Dispose()
    {

    }
}
