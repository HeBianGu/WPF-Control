// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.ViewModels.Base;
using H.Extensions.Setting;
using H.Modules.Dependency;
using H.Mvvm.ViewModels.Base;
using H.Services.Setting;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace H.Modules.Dependency;

[Icon(FontIcons.Info)]
[Display(Name = "第三方依赖项", GroupName = SettingGroupNames.GroupSystem, Description = "配置第三方依赖项相关参数")]
public class DependencyOptions : IocOptionInstance<DependencyOptions>, IDependencyOptions
{
    private ObservableCollection<IDependencyItem> _DependencyItems = new ObservableCollection<IDependencyItem>();
    [JsonIgnore]
    [ReadOnly(true)]
    [Display(Name = "第三方依赖项")]
    public ObservableCollection<IDependencyItem> DependencyItems
    {
        get { return _DependencyItems; }
        set
        {
            _DependencyItems = value;
        }
    }


}

public interface IDependencyItem
{

}
public class DependencyItemBase : DisplayBindableBase, IDependencyItem
{
    private string _Uri;
    public string Uri
    {
        get { return _Uri; }
        set
        {
            _Uri = value;
            RaisePropertyChanged();
        }
    }


    private string _Version;

    public string Version
    {
        get { return _Version; }
        set
        {
            _Version = value;
            RaisePropertyChanged();
        }
    }


    private string _Licence;
    public string Licence
    {
        get { return _Licence; }
        set
        {
            _Licence = value;
            RaisePropertyChanged();
        }
    }

}

public class DependencyItem : DependencyItemBase
{
    

}

public class XceedDependencyItem: DependencyItemBase
{
    public XceedDependencyItem()
    {
        this.Name = "Extended WPF Toolkit";
        this.Uri= "http://xceed.com/wpf_toolkit";
        this.Version = "old version";
        this.Licence = "License (Ms-PL)";

    }
}