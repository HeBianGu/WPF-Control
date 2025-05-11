// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.License;

public interface ILicenseOptions
{
    string Contract { get; set; }
    string FilePath { get; set; }
    int TrialDays { get; set; }
    bool UseTipTrialOnLoad { get; set; }
    bool UseTrial { get; set; }
    bool UseVailLicenceOnLoad { get; set; }
}