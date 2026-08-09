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
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace H.Modules.Dependency;

[Icon(FontIcons.Info)]
[Display(Name = "第三方依赖项", GroupName = SettingGroupNames.GroupSystem, Description = "配置第三方依赖项相关参数")]
public class DependencyOptions : IocOptionInstance<DependencyOptions>, IDependencyOptions
{
    private ObservableCollection<IDependencyItem> _DependencyItems = new ObservableCollection<IDependencyItem>();

    public DependencyOptions()
    {
        DependencyItems = new ObservableCollection<IDependencyItem>
        {
            new DependencyItem
            {
                Name = "OpenCvSharp4",
                Uri = "https://github.com/shimat/opencvsharp",
                Version = "4.13.0.20260222",
                Licence = "Apache License 2.0",
                Description = "OpenCV 的 .NET 封装，用于图像处理。"
            },
            new DependencyItem
            {
                Name = "Entity Framework Core",
                Uri = "https://github.com/dotnet/efcore",
                Version = "8.0.16",
                Licence = "MIT License",
                Description = "用于 SQLite 和 SQL Server 的数据访问。"
            },
            new DependencyItem
            {
                Name = "Newtonsoft.Json",
                Uri = "https://www.newtonsoft.com/json",
                Version = "13.0.3",
                Licence = "MIT License",
                Description = "用于项目数据的 JSON 序列化与反序列化。"
            },
            new DependencyItem
            {
                Name = "log4net",
                Uri = "https://logging.apache.org/log4net/",
                Version = "2.0.15",
                Licence = "Apache License 2.0",
                Description = "用于应用日志记录。"
            },
            new DependencyItem
            {
                Name = "MvCameraControl.Net",
                Uri = "https://www.hikrobotics.com/cn/machinevision/service/download",
                Version = "由海康机器视觉 SDK 提供",
                Licence = "海康机器视觉 SDK 许可",
                Description = "用于接入海康工业相机。"
            },
            new XceedDependencyItem()
        };
    }

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
    private string _name;
    /// <summary>
    /// 获取或设置对象的名称。
    /// </summary>
    [Display(Name = "名称")]
    [Browsable(true)]
    public override string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    private string _description;
    /// <summary>
    /// 获取或设置对象的描述。
    /// </summary>
    [Display(Name = "说明")]
    [Browsable(true)]
    public override string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            RaisePropertyChanged();
        }
    }
    private string _Uri;
    [Display(Name ="地址")]
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
    [Display(Name = "版本")]
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
    [Display(Name = "许可")]
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
        this.Description = "License (Ms-PL)许可的旧版本";

    }
}

//  ToDo：说明
