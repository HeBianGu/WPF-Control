// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.License
{
    public interface ILicenseService
    {
        LicenseOption IsVail(out string error);
        LicenseOption IsVail(string module, out string error);
        LicenseOption TryActive(string module, string lic, out string error);
        string GetHostID();
    }

    public class LicenseProxy : Ioc<ILicenseService>
    {
    }
}