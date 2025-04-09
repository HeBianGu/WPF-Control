// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using System.Windows.Markup;
global using System.Windows;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using System.Text;
global using System.Windows.Controls;
global using System.Windows.Data;
global using System.Windows.Documents;
global using System.Windows.Input;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
global using System.Windows.Navigation;
global using System.Windows.Shapes;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel;
global using System.IO;
global using System.Windows.Controls.Primitives;
global using H.Controls.Adorner;
global using H.Controls.Adorner.Adorner;
global using H.Controls.Adorner.Adorner.ControlTemplateAdorners;
global using H.Controls.Adorner.Adorner.DataTemplateAdorners;
global using H.Controls.Adorner.Draggable;
global using H.Controls.Adorner.Draggable.Bevhavior;
global using H.Services.Setting;
global using H.Mvvm.Commands;
global using H.Mvvm.ViewModels;
global using H.Mvvm.ViewModels.Tree;
global using H.Iocable;
global using H.Extensions.Common;
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]


[assembly: XmlnsDefinition("QQ:908293466", "H.Controls.Adorner")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Controls.Adorner.Adorner")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Controls.Adorner.Adorner.ControlTemplateAdorners")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Controls.Adorner.Adorner.DataTemplateAdorners")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Controls.Adorner.Draggable")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Controls.Adorner.Draggable.Bevhavior")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Adorner")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Adorner.Adorner")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Adorner.Adorner.ControlTemplateAdorners")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Adorner.Adorner.DataTemplateAdorners")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Adorner.Draggable")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Adorner.Draggable.Bevhavior")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Adorner")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Adorner.Adorner")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Adorner.Adorner.ControlTemplateAdorners")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Adorner.Adorner.DataTemplateAdorners")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Adorner.Draggable")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Adorner.Draggable.Bevhavior")]
[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]
