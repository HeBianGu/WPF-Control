
global using System.Windows.Markup;
global using System.Windows;
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
global using H.Extensions.Command;
global using H.Extensions.Command.ScrollViewers;
global using H.Extensions.Command.TextBoxs;
global using H.Common;
global using H.Common.Attributes;
global using H.Common.Transitionable;
global using H.Mvvm.Commands;
global using H.Mvvm.ViewModels;
global using H.Mvvm.ViewModels.Tree;
global using H.Extensions.Common;
global using H.Services.Message.Dialog;
global using H.Services.Message.Dialog.Commands;
global using H.Services.Message.IODialog;
global using H.Services.Message.Notice;
global using H.Services.Message.Notify;
global using H.Services.Message.Snack;
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]


[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Command")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Command.ScrollViewers")]
[assembly: XmlnsDefinition("QQ:908293466", "H.Extensions.Command.TextBoxs")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Command")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Command.ScrollViewers")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Extensions.Command.TextBoxs")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Command")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Command.ScrollViewers")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Extensions.Command.TextBoxs")]
[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]