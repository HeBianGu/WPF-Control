// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace H.Extensions.Setting;

public abstract class IocOptionInstance<Setting> : SettableBase, IOptions<Setting> where Setting : class, new()
{
    public static Setting Instance => Ioc.GetService<IOptions<Setting>>().Value;

    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]

    [System.Xml.Serialization.XmlIgnore]
    Setting IOptions<Setting>.Value
    {
        get { return Instance; }
    }
}
