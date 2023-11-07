using H.Presenters.Common;
using H.Providers.Ioc;
using H.Themes.Default;
using System;
using System.ComponentModel;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.CanSumit?.Invoke() == false)
            {
                e.Cancel = true;
                return;
            }
            base.OnClosing(e);
        }

        public Func<bool> CanSumit { get; set; }

        public bool IsCancel => this.Dispatcher.Invoke(() => this.DialogResult == false);

        public ControlTemplate BottomTemplate
        {
            get { return (ControlTemplate)GetValue(BottomTemplateProperty); }
            set { SetValue(BottomTemplateProperty, value); }
        }

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
        public static bool? ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit, Window owner = null, Action<System.Windows.Window> action = null)
        {
            Action<Window> build = x =>
            {
                x.Width = 400;
                x.Height = 200;
                x.MinHeight = 150;
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.Style = Application.Current.FindResource(GetResourceKey(dialogButton)) as Style;
                action?.Invoke(x);
            };

            return ShowPresenter(new MessagePresenter() { Value = message }, build, DialogButton.Sumit, title, null, owner);
        }

        public static ResourceKey GetResourceKey(DialogButton dialogButton)
        {
            switch (dialogButton)
            {
                case DialogButton.Sumit:
                    return DialogKeys.Sumit;
                case DialogButton.None:
                    return DialogKeys.None;
                case DialogButton.Cancel:
                    return DialogKeys.Cancel;
                case DialogButton.SumitAndCancel:
                    return DialogKeys.SumitAndCancel;
                default:
                    return DialogKeys.Sumit;
            }
        }

        public static bool? ShowPresenter(object data, Action<DialogWindow> action = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = data;
            dialog.Title = title ?? data.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            dialog.Width = 500;
            dialog.SizeToContent = SizeToContent.Height;
            dialog.CanSumit = canSumit;
            var key = GetResourceKey(dialogButton);
            dialog.Style = Application.Current.FindResource(key) as Style;
            if (owner?.IsLoaded == true)
            {
                dialog.Owner = owner;
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

        public static T ShowAction<T>(object data, Func<ICancelable, T> func, Action<DialogWindow> action = null, string title = null, Window owner = null)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = data;
            dialog.Title = title ?? data.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            dialog.Width = 500;
            dialog.MinHeight = 150;
            dialog.SizeToContent = SizeToContent.Height;
            dialog.Style = Application.Current.FindResource(DialogKeys.Cancel) as Style;
            if (owner?.IsLoaded == true)
            {
                dialog.Owner = owner;
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
            return ShowPresenter(data, null, DialogButton.Sumit, title);
        }

        public static bool? ShowIoc<T>(Action<DialogWindow> action = null, string title = null, Window owner = null)
        {
            var about = Ioc.Services.GetService(typeof(T));
            return DialogWindow.ShowIoc(typeof(T), action, title, owner);
        }

        public static bool? ShowIoc(Type type, Action<DialogWindow> action = null, string title = null, Window owner = null)
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
