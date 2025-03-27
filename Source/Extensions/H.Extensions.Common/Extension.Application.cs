using System.Reflection;

namespace H.Extensions.Common;

public class ApplicationProvider
{
    //  Do ：A human-friendly title of the package, typically used in UI displays as on nuget.org and the Package Manager in Visual Studio. If not specified, the package ID is used instead.
    public static string Title => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
    //  Do ：程序集清单的产品名信息
    public static string Product => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product;
    //  Do ：长说明
    public static string Description => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
    //  Do ：程序集清单的公司名
    public static string Company => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
    public static string Culture => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCultureAttribute>()?.Culture;
    public static string Trademark => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTrademarkAttribute>()?.Trademark;
    public static string Configuration => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
    //  Do ：版权详细信息
    public static string Copyright => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
    //  Do ：major.minor.patch 形式的程序集清单版本数值(如 2.4.0)
    public static string Version => Assembly.GetEntryAssembly().GetName().Version.ToString();
    ////  Do ：major.minor.patch 形式的程序集清单版本数值(如 2.4.0)
    //public static string Version => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyVersionAttribute>()?.Version;

    //  Do ：major.minor.build.revision 形式的程序集清单版本数值(如 2.4.0.1)
    public static string FileVersion => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
    //public static string Authors => Assembly.GetEntryAssembly().GetCustomAttribute<AuthorsAttribute>()?.Version;
    //  Do ：UI 显示的程序集产品版本(如 1.0 Beta)
    //public static string InformationalVersion => Assembly.GetEntryAssembly().GetCustomAttribute<InformationalVersionAttribute>()?.Version;

}
