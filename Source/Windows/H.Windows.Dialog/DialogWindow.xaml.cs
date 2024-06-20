using H.Providers.Ioc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace H.Windows.Dialog
{
    public partial class DialogWindow : Window, IDialog
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

        public void Sumit()
        {
            this.DialogResult = true;
            this.Close();
        }

        public Func<bool> CanSumit { get; set; }

        public bool IsCancel => this.Dispatcher.Invoke(() => this.DialogResult == false);

        public ControlTemplate BottomTemplate
        {
            get { return (ControlTemplate)GetValue(BottomTemplateProperty); }
            set { SetValue(BottomTemplateProperty, value); }
        }

        public DialogButton DialogButton { get; set; } = DialogButton.Sumit;

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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.SizeToContent == SizeToContent.WidthAndHeight && System.Windows.Shell.WindowChrome.GetWindowChrome(this) != null)
            {
                InvalidateMeasure();
            }
        }
    }

    public partial class DialogWindow : Window
    {
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

        public static bool? ShowPresenter(object presenter, Action<IDialog> action = null, Func<bool> canSumit = null)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = presenter;
            dialog.Width = 400;
            dialog.SizeToContent = SizeToContent.Height;
            dialog.CanSumit = canSumit;
            action?.Invoke(dialog);
            dialog.Title = dialog.Title ?? presenter.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            ResourceKey key = GetResourceKey(dialog.DialogButton);
            dialog.Style = Application.Current.FindResource(key) as Style;
            var owner = dialog.Owner ?? Application.Current.MainWindow;
            if (owner?.IsLoaded == true)
            {
                dialog.Owner = owner;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            bool? r = dialog.ShowDialog();
            return r;
        }

        public static T ShowAction<P, T>(P presenter, Func<IDialog, P, T> func, Action<IDialog> action = null)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = presenter;
            dialog.Width = 500;
            dialog.MinHeight = 150;
            dialog.SizeToContent = SizeToContent.Height;
            action?.Invoke(dialog);
            dialog.Title = dialog.Title ?? presenter.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            dialog.Style = Application.Current.FindResource(GetResourceKey(dialog.DialogButton)) as Style;
            var owner = dialog.Owner ?? Application.Current.MainWindow;
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
                if (func != null)
                {
                    Task.Run(() =>
                    {
                        result = func.Invoke(dialog, presenter);
                        dialog.Dispatcher.Invoke(() =>
                        {
                            if (dialog.DialogResult == null)
                                dialog.DialogResult = true;
                        });
                    });
                }
            };
            dialog.ShowDialog();
            return result;
        }
    }
}
