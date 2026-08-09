// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
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
        //DependencyItems = new ObservableCollection<IDependencyItem>
        //{
        //    H.Modules.Dependency.DependencyItems.OpenCvSharp4,
        //    H.Modules.Dependency.DependencyItems.EntityFrameworkCore,
        //    H.Modules.Dependency.DependencyItems.NewtonsoftJson,
        //    H.Modules.Dependency.DependencyItems.Log4Net,
        //    H.Modules.Dependency.DependencyItems.MvCameraControl,
        //    H.Modules.Dependency.DependencyItems.MicrosoftExtensionsDependencyInjection,
        //    H.Modules.Dependency.DependencyItems.CommunityToolkitMvvm,
        //    H.Modules.Dependency.DependencyItems.MicrosoftXamlBehaviorsWpf,
        //    H.Modules.Dependency.DependencyItems.AvalonDock,
        //    H.Modules.Dependency.DependencyItems.WpfToolkit,
        //    H.Modules.Dependency.DependencyItems.DataGridFilter,
        //    H.Modules.Dependency.DependencyItems.PdfiumViewer,
        //    H.Modules.Dependency.DependencyItems.QRCoder,
        //    H.Modules.Dependency.DependencyItems.Quartz,
        //    H.Modules.Dependency.DependencyItems.ColorPicker,
        //    H.Modules.Dependency.DependencyItems.VlcDotNet,
        //    H.Modules.Dependency.DependencyItems.CSCore,
        //    H.Modules.Dependency.DependencyItems.OdysseyWpf,
        //    H.Modules.Dependency.DependencyItems.WpfControlBase,
        //    H.Modules.Dependency.DependencyItems.AutoUpdaterNet
        //};
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
