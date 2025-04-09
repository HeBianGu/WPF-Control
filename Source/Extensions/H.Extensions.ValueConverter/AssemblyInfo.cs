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
global using H.Extensions.ValueConverter;
global using H.Extensions.ValueConverter.Bools;
global using H.Extensions.ValueConverter.Brushs;
global using H.Extensions.ValueConverter.Doubles;
global using H.Extensions.ValueConverter.Files;
global using H.Extensions.ValueConverter.IEnumerables;
global using H.Extensions.ValueConverter.Images;
global using H.Extensions.ValueConverter.Ints;
global using H.Extensions.ValueConverter.ItemsControls;
global using H.Extensions.ValueConverter.Types;
global using H.Extensions.ValueConverter.Visiblilitys;
global using H.Mvvm.Commands;
global using H.Mvvm.ViewModels;
global using H.Mvvm.ViewModels.Tree;
global using H.Extensions.Common;
global using System;

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