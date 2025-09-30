// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Mvvm")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Mvvm.Commands")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Mvvm.ViewModels")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Mvvm.ViewModels.Tree")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Mvvm")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Mvvm.Commands")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Mvvm.ViewModels")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Mvvm.ViewModels.Tree")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Mvvm")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Mvvm.Commands")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Mvvm.ViewModels")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Mvvm.ViewModels.Tree")]
[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]
