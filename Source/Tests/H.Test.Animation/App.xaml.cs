using H.Extensions.ApplicationBase;
using H.Modules.Messages.Dialog;
using H.Modules.Messages.Form;
using H.Services.Common;
using H.Services.Message.Dialog;
using H.Services.Message.Form;
using H.Styles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Animation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDialogMessageService, AdornerDialogMessageService>();
            services.AddSingleton<IFormMessageService, FormMessageService>();
            services.AddNoticeMessage();
            services.AddSnackMessage();
            services.AddAbout();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
