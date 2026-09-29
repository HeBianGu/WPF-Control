// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json.Serialization;

namespace H.Modules.Upgrade.Base;

public class JsonUpgradeInfo : IUpgradeInfo
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("changelog")]
    public string[] Changelog { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    /// <summary>
    /// 是否强制更新，是则不更新程序推出
    /// </summary>
    [JsonPropertyName("force")]
    public bool Force { get; set; } = false;
}