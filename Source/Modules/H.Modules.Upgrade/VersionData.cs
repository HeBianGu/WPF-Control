// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Upgrade;

internal class VersionData
{
    public string Version { get; set; }
    public string Uri { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public List<string> Messages { get; set; } = new List<string>();
    public bool Force { get; set; } = false;
}