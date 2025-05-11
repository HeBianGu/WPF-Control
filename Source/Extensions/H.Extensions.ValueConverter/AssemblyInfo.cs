// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Common;
global using H.Mvvm.Commands;
global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.IO;
global using System.Linq;
global using System.Text;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Data;
global using System.Windows.Input;
global using System.Windows.Markup;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Bools")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Brushs")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Doubles")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Files")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.IEnumerables")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Images")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Ints")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.ItemsControls")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Types")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.ValueConverter.Visiblilitys")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Bools")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Brushs")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Doubles")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Files")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.IEnumerables")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Images")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Ints")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.ItemsControls")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Types")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.ValueConverter.Visiblilitys")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Bools")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Brushs")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Doubles")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Files")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.IEnumerables")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Images")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Ints")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.ItemsControls")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Types")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.ValueConverter.Visiblilitys")]
[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]