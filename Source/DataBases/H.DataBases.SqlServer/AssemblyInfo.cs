global using H.Iocable;
global using H.Services.Logger;
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("QQ:908293466", "H.DataBases.SqlServer")]
[assembly: XmlnsPrefix("QQ:908293466", "h")]

[assembly: XmlnsDefinition("https://github.com/HeBianGu", "H.DataBases.SqlServer")]
[assembly: XmlnsPrefix("https://github.com/HeBianGu", "h")]
