// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Dependency.Items;

namespace H.Modules.Dependency;

public static class DependencyItems
{
    public static IDependencyItem WPFControl => new WPFControlDependencyItem();
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
