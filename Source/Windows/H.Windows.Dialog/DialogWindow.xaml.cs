using H.Providers.Ioc;
using H.Themes.Default;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Windows.Dialog
{
    public partial class DialogWindow : Window, ICancelable
    {
        static DialogWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(typeof(DialogWindow)));
        }

        public ControlTemplate BottomTemplate
        {
            get { return (ControlTemplate)GetValue(BottomTemplateProperty); }
            set { SetValue(BottomTemplateProperty, value); }
        }

        public bool IsCancel => this.Dispatcher.Invoke(() => this.DialogResult == false);


        public static readonly DependencyProperty BottomTemplateProperty =
            DependencyProperty.Register("BottomTemplate", typeof(ControlTemplate), typeof(DialogWindow), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
            {
                DialogWindow control = d as DialogWindow;

                if (control == null) return;

                if (e.OldValue is ControlTemplate o)
                {

                }

                if (e.NewValue is ControlTemplate n)
                {

                }

            }));
    }

    partial class DialogWindow : Window
    {
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

            return ShowPresenter(message, build, title, ownerMainWindow);
        }

        public static bool? ShowPresenter(object data, Action<DialogWindow> action = null, string title = null, bool ownerMainWindow = true)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = data;
            dialog.Title = title ?? data.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
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

        public static T ShowPresenter<T>(object data, Func<ICancelable, T> func, Action<DialogWindow> action = null, string title = null, bool ownerMainWindow = true)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = data;
            dialog.Title = title ?? data.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            dialog.Width = 500;
            dialog.SizeToContent = SizeToContent.Height;
            dialog.Style = Application.Current.FindResource(DialogKeys.Cancel) as Style;
            if (ownerMainWindow)
            {
                dialog.Owner = Application.Current.MainWindow;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            T result = default;
            dialog.Loaded += (l, k) =>
            {
                Task.Run(() =>
                {
                    result = func.Invoke(dialog);
                    dialog.Dispatcher.Invoke(() =>
                    {
                        if (dialog.DialogResult == null)
                            dialog.DialogResult = true;
                    });
                });
            };
            action?.Invoke(dialog);
            dialog.ShowDialog();
            return result;
        }

        public static bool? ShowPresenter(object data, string title)
        {
            return ShowPresenter(data, null, title);
        }

        public static bool? ShowIoc<T>(Action<DialogWindow> action = null, string title = null, bool ownerMainWindow = true)
        {
            var about = Ioc.Services.GetService(typeof(T));
            return DialogWindow.ShowIoc(typeof(T), action, title, ownerMainWindow);
        }

        public static bool? ShowIoc(Type type, Action<DialogWindow> action = null, string title = null, bool ownerMainWindow = true)
        {
            var about = Ioc.Services.GetService(type);
            return DialogWindow.ShowPresenter(about, x =>
            {
                x.Width = 500;
                x.Height = 300;
            });
        }
    }
}
