// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Setting;
global using H.Services.Setting;
global using System.Collections.ObjectModel;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class TagOptions : IocOptionInstance<TagOptions>, ITagOptions
    {
        [Browsable(false)]
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();

        [ReadOnly(true)]
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        public string SplitChars { get; set; } = ", \n \r";
    }
}
