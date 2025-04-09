using H.Common.Interfaces;
using H.Common.Transitionable;
using H.Services.Message.Dialog;
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
        public DialogWindow()
        {
            this.Loaded += async (l, k) =>
            {//  ToDo：用动画显示
                await TransitionShow();
            };

        }
        static DialogWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(typeof(DialogWindow)));
        }

        public async Task TransitionShow()
        {
            await this.Show(this);

            if (this.Transitionable == null && this.Content is ITransitionHostable hostable)
                await hostable.Show(this);
            else
                await this.Show(this);
        }

        public async Task TransitionClose()
        {
            if (this.Transitionable == null && this.Content is ITransitionHostable hostable)
                await hostable.Close(this);
            else
                await this.Close(this);
            this.Close();
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            if (this.CanSumit != null && await this.CanSumit?.Invoke() == false)
            {
                e.Cancel = true;
                return;
            }
            //  ToDo：用动画关闭
            await this.Close(this);
            base.OnClosing(e);
            //task.ContinueWith(x =>
            //{
            //    this.Dispatcher.Invoke(() => base.OnClosing(e));
            //});
        }

        public void Sumit()
        {
            this.DialogResult = true;
            this.Close();
        }

        public Func<Task<bool>> CanSumit { get; set; }

        public bool IsCancel => this.Dispatcher.Invoke(() => this.DialogResult == false);

        public ControlTemplate BottomTemplate
        {
            get { return (ControlTemplate)GetValue(BottomTemplateProperty); }
            set { SetValue(BottomTemplateProperty, value); }
        }

        public DialogButton DialogButton { get; set; } = DialogButton.Sumit;
        public ITransitionable Transitionable { get; set; }
        string IDialog.Icon
        {
            get { return this.FontIcon; }
            set
            {
                this.FontIcon = value;
            }
        }

        public string FontIcon
        {
            get { return (string)GetValue(FontIconProperty); }
            set { SetValue(FontIconProperty, value); }
        }

        DataTemplate IDialog.PresenterTemplate { get => this.ContentTemplate; set => this.ContentTemplate = value; }

        public static readonly DependencyProperty FontIconProperty =
            DependencyProperty.Register("FontIcon", typeof(string), typeof(DialogWindow), new FrameworkPropertyMetadata("\xEA8F"));

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

        public static bool? ShowPresenter(object presenter, Action<IDialog> action = null, Func<Task<bool>> canSumit = null)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Content = presenter;
            dialog.Width = 400;
            dialog.SizeToContent = SizeToContent.Height;
            dialog.CanSumit = canSumit;
            if (presenter is IIconable iconable && !string.IsNullOrEmpty(iconable.Icon))
                dialog.FontIcon = iconable.Icon;
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
