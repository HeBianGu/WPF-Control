using H.Extensions.ApplicationBase;
using H.Modules.Messages.Dialog;
using H.Services.Common;
using H.Services.Message.Dialog;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace H.Test.Presenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDialogMessageService, AdornerDialogMessageService>();
            services.AddAbout();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
