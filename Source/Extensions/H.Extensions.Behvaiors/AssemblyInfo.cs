// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Linq;
global using System.Text;
global using System.Windows.Controls.Primitives;
global using System.Windows.Data;
global using System.Windows.Documents;
global using System.Windows.Input;
global using System.Windows.Markup;
global using System.Windows.Media;
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.Adorners")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.ContextMenus")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.DataGrids")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.FrameworkElements")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.ItemsControls")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.PasswordBoxs")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.ScrollViewers")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.TextBlocks")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.TextBoxs")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.TreeViews")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.Triggers")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Behvaiors.Triggers.Action")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.Adorners")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.ContextMenus")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.DataGrids")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.FrameworkElements")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.ItemsControls")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.PasswordBoxs")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.ScrollViewers")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.TextBlocks")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.TextBoxs")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.TreeViews")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.Triggers")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Behvaiors.Triggers.Action")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.Adorners")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.ContextMenus")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.DataGrids")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.FrameworkElements")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.ItemsControls")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.PasswordBoxs")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.ScrollViewers")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.TextBlocks")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.TextBoxs")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.TreeViews")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.Triggers")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Behvaiors.Triggers.Action")]
[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]