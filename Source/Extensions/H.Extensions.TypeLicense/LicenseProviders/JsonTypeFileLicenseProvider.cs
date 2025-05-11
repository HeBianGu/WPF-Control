// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json;

namespace H.Extensions.TypeLicense.LicenseProviders;

public class JsonTypeFileLicenseProvider<T> : TypeFileLicenseProviderBase where T : License
{
    protected override License CreateLicense(string key, Type type)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(key);
        }
        catch (Exception)
        {
            return null;
        }
    }
}

public class JsonTypeFileLicenseProvider : JsonTypeFileLicenseProvider<TypeLicense>
{

}
