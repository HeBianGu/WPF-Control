﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Extensions.Setting
{
    public abstract class IocOptionInstance<Setting> : SettingBase, IOptions<Setting> where Setting : class, new()
    {
        public static Setting Instance => Ioc.GetService<IOptions<Setting>>().Value;

        [Browsable(false)]
        [XmlIgnore]
        [JsonIgnore]
        Setting IOptions<Setting>.Value
        {
            get { return Instance; }
        }
    }
}