global using H.Mvvm.ViewModels.Base;
using H.Controls.Adorner.Adorner;
using H.Controls.Form;
using H.Extensions.Behvaiors.Adorners;
using H.Extensions.Command;
using H.Extensions.ValueConverter;
using H.Extensions.ValueConverter.IEnumerables;
using H.Services.Common;
using H.Services.Common.About;
using H.Services.Identity;
using H.Services.Message;
using H.Services.Project;
using H.Services.Setting;
using H.Styles;
using H.Themes.Colors;
using H.Themes.FontSizes;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H.Tools.AssemblyInfos;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        foreach (var assembly in GetAssemblies())
        {
            System.Diagnostics.Debug.WriteLine($"----------------{assembly.GetName().Name}---------------------");
            GenerateGlobalUsing(assembly);
            Generate(assembly, "QQ:908293466");
            Generate(assembly, "https://github.com/HeBianGu");
            Generate(assembly);
        }
    }


    public void Generate(Assembly assembly, string uri = "http://schemas.microsoft.com/winfx/2006/xaml/presentation")
    {
        string assemblyName = assembly.GetName().Name;
        var namespaces = this.GetNamespaces(assembly);
        var code = $@"
{string.Join("\n", namespaces.Select(ns => $"[assembly: XmlnsDefinition(\"{uri}\", \"{ns}\")]"))}
[assembly: XmlnsPrefix(""{uri}"", ""h"")]";
        System.Diagnostics.Debug.WriteLine(code);
    }

    public IEnumerable<string> GetNamespaces(Assembly assembly)
    {
        // 获取所有公开（public）类型的命名空间（去重）
        var namespaces = assembly.GetExportedTypes().Where(x => !x.IsAbstract || x.IsInterface)  // 仅获取公开类型（public）
            .Select(t => t.Namespace)
            .Where(ns => ns != null)  // 过滤掉没有命名空间的类型（如匿名类型）
            .Distinct()               // 去重
            .OrderBy(ns => ns);       // 按名称排序
        return namespaces;
    }

    public void GenerateGlobalUsing(Assembly assembly)
    {
        System.Diagnostics.Debug.WriteLine("global using System.Windows.Markup;");
        System.Diagnostics.Debug.WriteLine("global using System.Windows;");
        ;
        var namespaces = this.GetDefaultGlobalUsings().Concat(this.GetGlobalUsing(assembly));
        var code = string.Join("\n", namespaces.Select(ns => $"global using {ns};"));
        System.Diagnostics.Debug.WriteLine(code);
    }

    public IEnumerable<string> GetDefaultGlobalUsings()
    {
        yield return "System.Windows.Markup";
        yield return "System.Windows";
        yield return "System.Collections.Generic";
        yield return "System.Linq";
        yield return "System.Threading.Tasks";
        yield return "System.Text";
        yield return "System.Windows.Controls";
        yield return "System.Windows.Data";
        yield return "System.Windows.Documents";
        yield return "System.Windows.Input";
        yield return "System.Windows.Media";
        yield return "System.Windows.Media.Imaging";
        yield return "System.Windows.Navigation";
        yield return "System.Windows.Shapes";
        yield return "System.Collections.ObjectModel";
        yield return "System.ComponentModel.DataAnnotations";
        yield return "System.ComponentModel";
        yield return "System.IO";
        yield return "System.Windows.Controls.Primitives";
    }

    public IEnumerable<string> GetGlobalUsing(Assembly assembly)
    {
        foreach (var it in this.GetNamespaces(assembly))
        {
            yield return it;
        }
        var referencedAssemblies = assembly.GetReferencedAssemblies().Where(x => x.FullName.StartsWith("H."));
        foreach (var item in referencedAssemblies)
        {
            var ass = Assembly.Load(item);

            foreach (var n in GetNamespaces(ass))
            {
                yield return n;
            }
        }
    }


    public IEnumerable<Assembly> GetAssemblies()
    {
        yield return typeof(BindableBase).Assembly;
        yield return typeof(IocMessage).Assembly;
        yield return typeof(IocProject).Assembly;
        yield return typeof(ILoginService).Assembly;
        yield return typeof(IApplicationBuilder).Assembly;
        yield return typeof(GetTrueForAllConverter).Assembly;
        yield return typeof(DeleteCommand).Assembly;
        yield return typeof(AdornerBehaviorBase).Assembly;
        yield return typeof(FontSizeKeys).Assembly;
        yield return typeof(LogoDataProvider).Assembly;
        yield return typeof(LineAdorner).Assembly;
        yield return typeof(Form).Assembly;
        yield return typeof(IAboutViewPresenter).Assembly;

        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
        //yield return typeof(IApplicationBuilder).Assembly;
    }
}