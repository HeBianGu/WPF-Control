// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Upgrade;

public interface IUpgradeOptions
{
    bool AutomaticUpgrade { get; set; }
    bool CheckUpdateOnStart { get; set; }
    string LoadFormat { get; set; }
    bool NotifyUpgrade { get; set; }
    string SavePath { get; set; }
    string Uri { get; set; }
    bool UseIEDownload { get; set; }
}