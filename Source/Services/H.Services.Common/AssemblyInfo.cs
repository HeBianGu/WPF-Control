// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;
global using H.Iocable;
global using System.Collections.Generic;
global using System.Windows;
global using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.About")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Crypt")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.DataBase")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Excel")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Feedback")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Guide")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.MainWindow")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Schedule")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Serialize.Meta")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.SplashScreen")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Theme")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Services.Common.Upgrade")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.About")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Crypt")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.DataBase")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Excel")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Feedback")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Guide")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.MainWindow")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Schedule")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Serialize.Meta")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.SplashScreen")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Theme")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Services.Common.Upgrade")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.About")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Crypt")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.DataBase")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Excel")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Feedback")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Guide")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.MainWindow")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Schedule")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Serialize.Meta")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.SplashScreen")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Theme")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Services.Common.Upgrade")]
[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]

