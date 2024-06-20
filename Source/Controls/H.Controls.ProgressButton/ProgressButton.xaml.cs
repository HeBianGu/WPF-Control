// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.ProgressButton
{
    public class ProgressButton : Button
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ProgressButton), "S.ProgressButton.Default");
        public static ComponentResourceKey CommandKey => new ComponentResourceKey(typeof(ProgressButton), "S.ProgressButton.Command");
        static ProgressButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressButton), new FrameworkPropertyMetadata(typeof(ProgressButton)));
        }

        public ProgressButton()
        {

        }


        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }


        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(ProgressButton), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 ProgressButton control = d as ProgressButton;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }


        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(ProgressButton), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 ProgressButton control = d as ProgressButton;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public double Percent
        {
            get { return (double)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }


        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(double), typeof(ProgressButton), new FrameworkPropertyMetadata(default(double), (d, e) =>
             {
                 ProgressButton control = d as ProgressButton;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }


        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ProgressButton), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 ProgressButton control = d as ProgressButton;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        public double ProgressOpacity
        {
            get { return (double)GetValue(ProgressOpacityProperty); }
            set { SetValue(ProgressOpacityProperty, value); }
        }


        public static readonly DependencyProperty ProgressOpacityProperty =
            DependencyProperty.Register("ProgressOpacity", typeof(double), typeof(ProgressButton), new FrameworkPropertyMetadata(0.5, (d, e) =>
             {
                 ProgressButton control = d as ProgressButton;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));

    }
}
