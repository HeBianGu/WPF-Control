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
global using H.Services.Common;
global using H.Services.Common.About;
global using H.Services.Common.Crypt;
global using H.Services.Common.DataBase;
global using H.Services.Common.Excel;
global using H.Services.Common.Feedback;
global using H.Services.Common.Guide;
global using H.Services.Common.MainWindow;
global using H.Services.Common.Schedule;
global using H.Services.Common.Serialize.Meta;
global using H.Services.Common.SplashScreen;
global using H.Services.Common.Theme;
global using H.Services.Common.Upgrade;
global using H.Iocable;
global using H.Common;
global using H.Common.Attributes;
global using H.Common.Interfaces;
global using H.Common.Interfaces.Where;
global using H.Common.Transitionable;

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

