using H.Themes.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace H.Modules.SplashScreen
{
    public class SplashScreenWindow : System.Windows.Window
    {
        static SplashScreenWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplashScreenWindow), new FrameworkPropertyMetadata(typeof(SplashScreenWindow)));
        }

        public ControlTemplate BottomTemplate
        {
            get { return (ControlTemplate)GetValue(BottomTemplateProperty); }
            set { SetValue(BottomTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomTemplateProperty =
            DependencyProperty.Register("BottomTemplate", typeof(ControlTemplate), typeof(SplashScreenWindow), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
            {
                SplashScreenWindow control = d as SplashScreenWindow;

                if (control == null) return;

                if (e.OldValue is ControlTemplate o)
                {

                }

                if (e.NewValue is ControlTemplate n)
                {

                }

            }));



        public static bool? ShowMessage(string message, string title = "提示", bool ownerMainWindow = true, Action<System.Windows.Window> action = null)
        {
            Action<System.Windows.Window> build = x =>
            {
                x.Width = 400;
                x.Height = 200;
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.Padding = new Thickness(10, 6, 10, 6);
                action?.Invoke(x);
            };

            return ShowData(message, build, title, ownerMainWindow);
        }

        public static bool? ShowData(object data, Action<System.Windows.Window> action = null, string title = "提示", bool ownerMainWindow = true)
        {
            SplashScreenWindow dialog = new SplashScreenWindow();
            dialog.Title = title;
            dialog.Content = data;
            if (ownerMainWindow)
            {
                dialog.Owner = Application.Current.MainWindow;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            action?.Invoke(dialog);
            var r = dialog.ShowDialog();
            return r;
        }


        public static bool? ShowData(object data, string title)
        {
            return ShowData(data, null, title);
        }
    }

    public class DialogKeys
    {
        public static ComponentResourceKey ShowMessage => new ComponentResourceKey(typeof(DialogKeys), "S.SplashScreenWindow.ShowMessage");

    }
}
