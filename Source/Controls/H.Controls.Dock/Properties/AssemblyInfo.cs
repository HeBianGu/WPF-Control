








using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Dock")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Dock.Controls")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Dock.Converters")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Dock.Layout")]
[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.Controls.Dock.Themes")]

[assembly: XmlnsPrefix("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "h")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Dock")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Dock.Controls")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Dock.Converters")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Dock.Layout")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "H.Controls.Dock.Themes")]