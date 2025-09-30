// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Extensions.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace H.Modules.Help.Base;

public class UriHelpOptionsBase<T> : IocOptionInstance<T>, IUriHelpOptions where T : class, new()
{
    private string _uri;
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [Display(Name = "地址")]
    public string Uri
    {
        get { return _uri; }
        set
        {
            _uri = value;
            RaisePropertyChanged();
        }
    }
}

