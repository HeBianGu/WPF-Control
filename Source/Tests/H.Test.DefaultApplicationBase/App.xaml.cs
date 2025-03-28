using H.ApplicationBases.Default;
using System.Configuration;
using System.Data;
using System.Windows;

namespace H.Test.DefaultApplicationBase;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : DefaultModulesApplicationBase
{
    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}

