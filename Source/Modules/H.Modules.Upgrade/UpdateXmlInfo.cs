// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Xml.Serialization;

namespace H.Modules.Upgrade;

[XmlRoot("item")]
public class UpdateXmlInfo
{
    [XmlElement("url")]
    public string Url { get; set; }

    [XmlElement("changelog")]
    public string Changelog { get; set; }

    [XmlElement("version")]
    public string Version { get; set; }

    /// <summary>
    /// 是否强制更新，是则不更新程序推出
    /// </summary>
    [XmlElement("force")]
    public bool Force { get; set; } = false;
}