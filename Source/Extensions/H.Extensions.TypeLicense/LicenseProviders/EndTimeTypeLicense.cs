// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.TypeLicense.LicenseProviders;

public class EndTimeTypeLicense : ValidTypeLicense
{
    public EndTimeTypeLicense()
    {

    }

    public DateTime EndTime { get; set; }

    public override bool IsValid<T>(out string message)
    {
        if (this.EndTime < DateTime.Now)
        {
            message = $"该许可已于{this.EndTime}到期";
            return false;
        }
        return base.IsValid<T>(out message);
    }
}
