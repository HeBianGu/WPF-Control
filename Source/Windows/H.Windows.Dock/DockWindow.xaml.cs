using System.Windows;

namespace H.Windows.Dock
{
    public class DockWindow : Window
    {
        static DockWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockWindow), new FrameworkPropertyMetadata(typeof(DockWindow)));
        }

        public DockWindow()
        {
            this.Loaded += DockWindow_Loaded;
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
        }

        private void DockWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshLayout();
        }

        public void RefreshLayout()
        {
            if (this.HorizontalDock == HorizontalAlignment.Right)
                this.Left = SystemParameters.WorkArea.Right - this.Width;
            if (this.HorizontalDock == HorizontalAlignment.Left)
                this.Left = 0;
            if (this.HorizontalDock == HorizontalAlignment.Stretch)
                this.Left = 0;
            if (this.HorizontalDock == HorizontalAlignment.Center)
                this.Left = (SystemParameters.WorkArea.Right - this.Width) / 2;

            if (this.VerticalDock == VerticalAlignment.Bottom)
                this.Top = SystemParameters.WorkArea.Bottom - this.Height;
            if (this.VerticalDock == VerticalAlignment.Top)
                this.Top = 0;
            if (this.VerticalDock == VerticalAlignment.Stretch)
                this.Top = 0;
            if (this.VerticalDock == VerticalAlignment.Center)
                this.Top = (SystemParameters.WorkArea.Bottom - this.Height) / 2;
        }


        public HorizontalAlignment HorizontalDock
        {
            get { return (HorizontalAlignment)GetValue(HorizontalDockProperty); }
            set { SetValue(HorizontalDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalDockProperty =
            DependencyProperty.Register("HorizontalDock", typeof(HorizontalAlignment), typeof(DockWindow), new FrameworkPropertyMetadata(HorizontalAlignment.Right, (d, e) =>
            {
                DockWindow control = d as DockWindow;

                if (control == null) return;

                if (e.OldValue is HorizontalAlignment o)
                {

                }

                if (e.NewValue is HorizontalAlignment n)
                {

                }
                control.RefreshLayout();
            }));


        public VerticalAlignment VerticalDock
        {
            get { return (VerticalAlignment)GetValue(VerticalDockProperty); }
            set { SetValue(VerticalDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalDockProperty =
            DependencyProperty.Register("VerticalDock", typeof(VerticalAlignment), typeof(DockWindow), new FrameworkPropertyMetadata(VerticalAlignment.Bottom, (d, e) =>
            {
                DockWindow control = d as DockWindow;

                if (control == null) return;

                if (e.OldValue is VerticalAlignment o)
                {

                }

                if (e.NewValue is VerticalAlignment n)
                {

                }
                control.RefreshLayout();
            }));



        public bool UseToggleButton
        {
            get { return (bool)GetValue(UseToggleButtonProperty); }
            set { SetValue(UseToggleButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseToggleButtonProperty =
            DependencyProperty.Register("UseToggleButton", typeof(bool), typeof(DockWindow), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                DockWindow control = d as DockWindow;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

    }
}
