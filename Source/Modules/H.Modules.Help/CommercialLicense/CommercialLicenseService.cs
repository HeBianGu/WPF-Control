// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Help.Base;
using H.Services.AppPath;
using System.IO;

namespace H.Modules.Help.CommercialLicense;

public class CommercialLicenseService : ICommercialLicenseService
{
    public void Show()
    {
        string filePath= Path.Combine(AppDomianPaths.Assets, "LICENSE.txt");
        if (!File.Exists(filePath))
            return;
        filePath.ShowProcess();
    }
}