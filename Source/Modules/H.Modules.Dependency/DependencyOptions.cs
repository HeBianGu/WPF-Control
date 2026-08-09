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
            H.Modules.Dependency.DependencyItems.OpenCvSharp4,
            H.Modules.Dependency.DependencyItems.EntityFrameworkCore,
            H.Modules.Dependency.DependencyItems.NewtonsoftJson,
            H.Modules.Dependency.DependencyItems.Log4Net,
            H.Modules.Dependency.DependencyItems.MvCameraControl,
            H.Modules.Dependency.DependencyItems.MicrosoftExtensionsDependencyInjection,
            H.Modules.Dependency.DependencyItems.CommunityToolkitMvvm,
            H.Modules.Dependency.DependencyItems.MicrosoftXamlBehaviorsWpf,
            H.Modules.Dependency.DependencyItems.AvalonDock,
            H.Modules.Dependency.DependencyItems.WpfToolkit,
            H.Modules.Dependency.DependencyItems.DataGridFilter,
            H.Modules.Dependency.DependencyItems.PdfiumViewer,
            H.Modules.Dependency.DependencyItems.QRCoder,
            H.Modules.Dependency.DependencyItems.Quartz,
            H.Modules.Dependency.DependencyItems.ColorPicker,
            H.Modules.Dependency.DependencyItems.VlcDotNet,
            H.Modules.Dependency.DependencyItems.CSCore,
            H.Modules.Dependency.DependencyItems.OdysseyWpf,
            H.Modules.Dependency.DependencyItems.WpfControlBase,
            H.Modules.Dependency.DependencyItems.AutoUpdaterNet
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
    [Display(Name = "地址")]
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

[Display(Name = "OpenCvSharp4", GroupName = "视觉与相机", Description = "OpenCV 的 .NET 封装，用于图像处理。")]
public class OpenCvSharp4DependencyItem : DependencyItemBase
{
    public OpenCvSharp4DependencyItem()
    {
        Name = "OpenCvSharp4";
        Uri = "https://github.com/shimat/opencvsharp";
        Version = "4.13.0.20260222";
        Licence = "Apache License 2.0";
        Description = "OpenCV 的 .NET 封装，用于图像处理。";
    }
}

[Display(Name = "Entity Framework Core", GroupName = "数据与日志", Description = "用于 SQLite 和 SQL Server 的数据访问。")]
public class EntityFrameworkCoreDependencyItem : DependencyItemBase
{
    public EntityFrameworkCoreDependencyItem()
    {
        Name = "Entity Framework Core";
        Uri = "https://github.com/dotnet/efcore";
        Version = "8.0.16";
        Licence = "MIT License";
        Description = "用于 SQLite 和 SQL Server 的数据访问。";
    }
}

[Display(Name = "Newtonsoft.Json", GroupName = "数据与日志", Description = "用于项目数据的 JSON 序列化与反序列化。")]
public class NewtonsoftJsonDependencyItem : DependencyItemBase
{
    public NewtonsoftJsonDependencyItem()
    {
        Name = "Newtonsoft.Json";
        Uri = "https://www.newtonsoft.com/json";
        Version = "13.0.3";
        Licence = "MIT License";
        Description = "用于项目数据的 JSON 序列化与反序列化。";
    }
}

[Display(Name = "log4net", GroupName = "数据与日志", Description = "用于应用日志记录。")]
public class Log4NetDependencyItem : DependencyItemBase
{
    public Log4NetDependencyItem()
    {
        Name = "log4net";
        Uri = "https://logging.apache.org/log4net/";
        Version = "2.0.15";
        Licence = "Apache License 2.0";
        Description = "用于应用日志记录。";
    }
}

[Display(Name = "MvCameraControl.Net", GroupName = "视觉与相机", Description = "用于接入海康工业相机。")]
public class MvCameraControlDependencyItem : DependencyItemBase
{
    public MvCameraControlDependencyItem()
    {
        Name = "MvCameraControl.Net";
        Uri = "https://www.hikrobotics.com/cn/machinevision/service/download";
        Version = "由海康机器视觉 SDK 提供";
        Licence = "海康机器视觉 SDK 许可";
        Description = "用于接入海康工业相机。";
    }
}

[Display(Name = "Microsoft.Extensions.DependencyInjection", GroupName = "基础框架", Description = "用于 IOC 与依赖注入。")]
public class MicrosoftExtensionsDependencyInjectionDependencyItem : DependencyItemBase
{
    public MicrosoftExtensionsDependencyInjectionDependencyItem()
    {
        Name = "Microsoft.Extensions.DependencyInjection";
        Uri = "https://learn.microsoft.com/dotnet/core/extensions/dependency-injection";
        Version = "8.0.1";
        Licence = "MIT License";
        Description = "用于 IOC 与依赖注入。";
    }
}

[Display(Name = "CommunityToolkit.Mvvm", GroupName = "基础框架", Description = "提供 MVVM 基础功能。")]
public class CommunityToolkitMvvmDependencyItem : DependencyItemBase
{
    public CommunityToolkitMvvmDependencyItem()
    {
        Name = "CommunityToolkit.Mvvm";
        Uri = "https://github.com/CommunityToolkit/dotnet";
        Version = "README 未注明";
        Licence = "MIT License";
        Description = "提供 MVVM 基础功能。";
    }
}

[Display(Name = "Microsoft.Xaml.Behaviors.Wpf", GroupName = "基础框架", Description = "提供 WPF 行为支持。")]
public class MicrosoftXamlBehaviorsWpfDependencyItem : DependencyItemBase
{
    public MicrosoftXamlBehaviorsWpfDependencyItem()
    {
        Name = "Microsoft.Xaml.Behaviors.Wpf";
        Uri = "https://github.com/microsoft/XamlBehaviorsWpf";
        Version = "1.1.77";
        Licence = "MIT License";
        Description = "提供 WPF 行为支持。";
    }
}

[Display(Name = "AvalonDock", GroupName = "开源控件库", Description = "提供可停靠窗口布局功能。")]
public class AvalonDockDependencyItem : DependencyItemBase
{
    public AvalonDockDependencyItem()
    {
        Name = "AvalonDock";
        Uri = "https://github.com/Dirkster99/AvalonDock";
        Version = "README 未注明";
        Licence = "MIT License";
        Description = "提供可停靠窗口布局功能。";
    }
}

[Display(Name = "Extended WPF Toolkit", GroupName = "开源控件库", Description = "License (Ms-PL)许可的旧版本")]
public class WpfToolkitDependencyItem : DependencyItemBase
{
    public WpfToolkitDependencyItem()
    {
        Name = "Extended WPF Toolkit";
        Uri = "http://xceed.com/wpf_toolkit";
        Version = "old version";
        Licence = "License (Ms-PL)";
        Description = "License (Ms-PL)许可的旧版本";
    }
}

[Display(Name = "DataGridFilter", GroupName = "开源控件库", Description = "提供 DataGrid 筛选功能。")]
public class DataGridFilterDependencyItem : DependencyItemBase
{
    public DataGridFilterDependencyItem()
    {
        Name = "DataGridFilter";
        Uri = "https://github.com/macgile/DataGridFilter";
        Version = "README 未注明";
        Licence = "请参阅组件仓库";
        Description = "提供 DataGrid 筛选功能。";
    }
}

[Display(Name = "PdfiumViewer", GroupName = "开源控件库", Description = "用于 PDF 文档查看。")]
public class PdfiumViewerDependencyItem : DependencyItemBase
{
    public PdfiumViewerDependencyItem()
    {
        Name = "PdfiumViewer";
        Uri = "https://github.com/bezzad/PdfiumViewer";
        Version = "1.0.6";
        Licence = "Apache License 2.0";
        Description = "用于 PDF 文档查看。";
    }
}

[Display(Name = "QRCoder", GroupName = "开源控件库", Description = "用于二维码生成。")]
public class QRCoderDependencyItem : DependencyItemBase
{
    public QRCoderDependencyItem()
    {
        Name = "QRCoder";
        Uri = "https://github.com/codebude/QRCoder/";
        Version = "1.4.3";
        Licence = "MIT License";
        Description = "用于二维码生成。";
    }
}

[Display(Name = "Quartz", GroupName = "任务与更新", Description = "用于计划任务调度。")]
public class QuartzDependencyItem : DependencyItemBase
{
    public QuartzDependencyItem()
    {
        Name = "Quartz";
        Uri = "https://github.com/quartznet/quartznet";
        Version = "3.14.0";
        Licence = "Apache License 2.0";
        Description = "用于计划任务调度。";
    }
}

[Display(Name = "ColorPicker", GroupName = "开源控件库", Description = "提供颜色选择控件。")]
public class ColorPickerDependencyItem : DependencyItemBase
{
    public ColorPickerDependencyItem()
    {
        Name = "ColorPicker";
        Uri = "https://github.com/PixiEditor/ColorPicker";
        Version = "README 未注明";
        Licence = "MIT License";
        Description = "提供颜色选择控件。";
    }
}

[Display(Name = "Vlc.DotNet", GroupName = "媒体与音频", Description = "用于 VLC 音视频播放。")]
public class VlcDotNetDependencyItem : DependencyItemBase
{
    public VlcDotNetDependencyItem()
    {
        Name = "Vlc.DotNet";
        Uri = "https://github.com/ZeBobo5/Vlc.DotNet";
        Version = "3.1.0";
        Licence = "LGPL License";
        Description = "用于 VLC 音视频播放。";
    }
}

[Display(Name = "CSCore", GroupName = "媒体与音频", Description = "提供音频处理功能。")]
public class CSCoreDependencyItem : DependencyItemBase
{
    public CSCoreDependencyItem()
    {
        Name = "CSCore";
        Uri = "https://github.com/filoe/cscore";
        Version = "README 未注明";
        Licence = "MS-PL";
        Description = "提供音频处理功能。";
    }
}

[Display(Name = "OdysseyWPF", GroupName = "开源控件库", Description = "提供 WPF 控件与主题支持。")]
public class OdysseyWpfDependencyItem : DependencyItemBase
{
    public OdysseyWpfDependencyItem()
    {
        Name = "OdysseyWPF";
        Uri = "https://github.com/jogibear9988/OdysseyWPF";
        Version = "README 未注明";
        Licence = "请参阅组件仓库";
        Description = "提供 WPF 控件与主题支持。";
    }
}

[Display(Name = "WPF-ControlBase", GroupName = "开源控件库", Description = "WPF-Control 的基础控件库。")]
public class WpfControlBaseDependencyItem : DependencyItemBase
{
    public WpfControlBaseDependencyItem()
    {
        Name = "WPF-ControlBase";
        Uri = "https://github.com/HeBianGu/WPF-ControlBase";
        Version = "README 未注明";
        Licence = "MIT License";
        Description = "WPF-Control 的基础控件库。";
    }
}

[Display(Name = "AutoUpdater.NET", GroupName = "任务与更新", Description = "用于应用自动更新。")]
public class AutoUpdaterNetDependencyItem : DependencyItemBase
{
    public AutoUpdaterNetDependencyItem()
    {
        Name = "AutoUpdater.NET";
        Uri = "https://github.com/ravibpatel/AutoUpdater.NET";
        Version = "README 未注明";
        Licence = "Apache License 2.0";
        Description = "用于应用自动更新。";
    }
}

public static class DependencyItems
{
    public static IDependencyItem OpenCvSharp4 => new OpenCvSharp4DependencyItem();
    public static IDependencyItem EntityFrameworkCore => new EntityFrameworkCoreDependencyItem();
    public static IDependencyItem NewtonsoftJson => new NewtonsoftJsonDependencyItem();
    public static IDependencyItem Log4Net => new Log4NetDependencyItem();
    public static IDependencyItem MvCameraControl => new MvCameraControlDependencyItem();
    public static IDependencyItem MicrosoftExtensionsDependencyInjection => new MicrosoftExtensionsDependencyInjectionDependencyItem();
    public static IDependencyItem CommunityToolkitMvvm => new CommunityToolkitMvvmDependencyItem();
    public static IDependencyItem MicrosoftXamlBehaviorsWpf => new MicrosoftXamlBehaviorsWpfDependencyItem();
    public static IDependencyItem AvalonDock => new AvalonDockDependencyItem();
    public static IDependencyItem WpfToolkit => new WpfToolkitDependencyItem();
    public static IDependencyItem DataGridFilter => new DataGridFilterDependencyItem();
    public static IDependencyItem PdfiumViewer => new PdfiumViewerDependencyItem();
    public static IDependencyItem QRCoder => new QRCoderDependencyItem();
    public static IDependencyItem Quartz => new QuartzDependencyItem();
    public static IDependencyItem ColorPicker => new ColorPickerDependencyItem();
    public static IDependencyItem VlcDotNet => new VlcDotNetDependencyItem();
    public static IDependencyItem CSCore => new CSCoreDependencyItem();
    public static IDependencyItem OdysseyWpf => new OdysseyWpfDependencyItem();
    public static IDependencyItem WpfControlBase => new WpfControlBaseDependencyItem();
    public static IDependencyItem AutoUpdaterNet => new AutoUpdaterNetDependencyItem();
}
