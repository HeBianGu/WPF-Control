using H.Extensions.ApplicationBase;
using H.Modules.Messages.Dialog;
using H.Modules.Messages.Form;
using H.Services.Common;
using H.Styles.Default;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Message
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IDialogMessage, WindowDialogMessage>();
            services.AddSingleton<IDialogMessageService, AdornerDialogMessageService>();
            services.AddSingleton<IFormMessageService, FormMessageService>();
            services.AddNoticeMessage();
            services.AddSnackMessage();
            services.AddAbout();


            //var sss= SystemColors.ControlDarkColor.ToString();
            // System.Diagnostics.Debug.WriteLine(sss);
         var ss=   SystemParameters.WindowCaptionButtonHeight;

        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
