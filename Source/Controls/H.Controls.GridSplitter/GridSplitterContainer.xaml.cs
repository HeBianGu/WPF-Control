// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.GridSplitter
{
    [TemplatePart(Name = GridSplitterName, Type = typeof(System.Windows.Controls.GridSplitter))]
    public partial class GridSplitterContainer : ContentControl, IMetaSettingSerilize
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(GridSplitterContainer), "S.GridSplitterContainer.Default");
        public static ComponentResourceKey RightKey => new ComponentResourceKey(typeof(GridSplitterContainer), "S.GridSplitterContainer.Right");
        public static ComponentResourceKey TopKey => new ComponentResourceKey(typeof(GridSplitterContainer), "S.GridSplitterContainer.Top");
        public static ComponentResourceKey BottomKey => new ComponentResourceKey(typeof(GridSplitterContainer), "S.GridSplitterContainer.Bottom");

        private const string GridSplitterName = "PART_GridSplitter";
        static GridSplitterContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridSplitterContainer), new FrameworkPropertyMetadata(typeof(GridSplitterContainer)));
        }
        private System.Windows.Controls.GridSplitter _gridSplitter = null;
        private double _menuWidthTemp;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _gridSplitter = Template.FindName(GridSplitterName, this) as System.Windows.Controls.GridSplitter;
            if (_gridSplitter == null) 
                return;

            _menuWidthTemp = this.MenuWidth.Value;
            this.Load();
            if (this.MenuDock == Dock.Right)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp - this._setting.HorizontalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.HorizontalChange = _menuWidthTemp - this.MenuWidth.Value + k.HorizontalChange;
                    this.Save();
                };

            }
            else if (this.MenuDock == Dock.Bottom)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp - this._setting.VerticalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.VerticalChange = _menuWidthTemp - this.MenuWidth.Value + k.VerticalChange;
                    this.Save();
                };
            }

            else if (this.MenuDock == Dock.Left)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp + this._setting.HorizontalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.HorizontalChange = this.MenuWidth.Value - _menuWidthTemp + k.HorizontalChange;
                    this.Save();
                };
            }
            else if (this.MenuDock == Dock.Top)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp + this._setting.VerticalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.VerticalChange = this.MenuWidth.Value - _menuWidthTemp + k.VerticalChange;
                    this.Save();
                };
            }
        }

        public object MenuContent
        {
            get { return GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));

        public DataTemplate MenuTempate
        {
            get { return (DataTemplate)GetValue(MenuTempateProperty); }
            set { SetValue(MenuTempateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuTempateProperty =
            DependencyProperty.Register("MenuTempate", typeof(DataTemplate), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is DataTemplate o)
                 {

                 }

                 if (e.NewValue is DataTemplate n)
                 {

                 }

             }));

        public double GridSpliteWidth
        {
            get { return (double)GetValue(GridSpliteWidthProperty); }
            set { SetValue(GridSpliteWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridSpliteWidthProperty =
            DependencyProperty.Register("GridSpliteWidth", typeof(double), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(5.0, (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));

        public double MenuMinWidth
        {
            get { return (double)GetValue(MenuMinWidthProperty); }
            set { SetValue(MenuMinWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuMinWidthProperty =
            DependencyProperty.Register("MenuMinWidth", typeof(double), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(10.0, (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


        public double MenuMaxWidth
        {
            get { return (double)GetValue(MenuMaxWidthProperty); }
            set { SetValue(MenuMaxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuMaxWidthProperty =
            DependencyProperty.Register("MenuMaxWidth", typeof(double), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(300.0, (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));

        public GridLength MenuWidth
        {
            get { return (GridLength)GetValue(MenuWidthProperty); }
            set { SetValue(MenuWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuWidthProperty =
            DependencyProperty.Register("MenuWidth", typeof(GridLength), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(new GridLength(200.0), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is GridLength o)
                 {

                 }

                 if (e.NewValue is GridLength n)
                 {

                 }

             }));


        public bool UseToggle
        {
            get { return (bool)GetValue(UseToggleProperty); }
            set { SetValue(UseToggleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseToggleProperty =
            DependencyProperty.Register("UseToggle", typeof(bool), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                GridSplitterContainer control = d as GridSplitterContainer;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                GridSplitterContainer control = d as GridSplitterContainer;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.Save();
            }));

        public Brush GridSpliterBackground
        {
            get { return (Brush)GetValue(GridSpliterBackgroundProperty); }
            set { SetValue(GridSpliterBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridSpliterBackgroundProperty =
            DependencyProperty.Register("GridSpliterBackground", typeof(Brush), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));

        public HorizontalAlignment ToggleHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(ToggleHorizontalAlignmentProperty); }
            set { SetValue(ToggleHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleHorizontalAlignmentProperty =
            DependencyProperty.Register("ToggleHorizontalAlignment", typeof(HorizontalAlignment), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is HorizontalAlignment o)
                 {

                 }

                 if (e.NewValue is HorizontalAlignment n)
                 {

                 }

             }));


        public VerticalAlignment ToggleVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(ToggleVerticalAlignmentProperty); }
            set { SetValue(ToggleVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleVerticalAlignmentProperty =
            DependencyProperty.Register("ToggleVerticalAlignment", typeof(VerticalAlignment), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(VerticalAlignment), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is VerticalAlignment o)
                 {

                 }

                 if (e.NewValue is VerticalAlignment n)
                 {

                 }

             }));


        public Dock MenuDock
        {
            get { return (Dock)GetValue(MenuDockProperty); }
            set { SetValue(MenuDockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuDockProperty =
            DependencyProperty.Register("MenuDock", typeof(Dock), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(Dock), (d, e) =>
             {
                 GridSplitterContainer control = d as GridSplitterContainer;

                 if (control == null) return;

                 if (e.OldValue is Dock o)
                 {

                 }

                 if (e.NewValue is Dock n)
                 {

                 }

             }));


        public string OpenGeometry
        {
            get { return (string)GetValue(OpenGeometryProperty); }
            set { SetValue(OpenGeometryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenGeometryProperty =
            DependencyProperty.Register("OpenGeometry", typeof(string), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                GridSplitterContainer control = d as GridSplitterContainer;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        public string CloseGeometry
        {
            get { return (string)GetValue(CloseGeometryProperty); }
            set { SetValue(CloseGeometryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseGeometryProperty =
            DependencyProperty.Register("CloseGeometry", typeof(string), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                GridSplitterContainer control = d as GridSplitterContainer;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        public Style ToggleStyle
        {
            get { return (Style)GetValue(ToggleStyleProperty); }
            set { SetValue(ToggleStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleStyleProperty =
            DependencyProperty.Register("ToggleStyle", typeof(Style), typeof(GridSplitterContainer), new FrameworkPropertyMetadata(default(Style), (d, e) =>
            {
                GridSplitterContainer control = d as GridSplitterContainer;

                if (control == null) return;

                if (e.OldValue is Style o)
                {

                }

                if (e.NewValue is Style n)
                {

                }

            }));


        public string ID { get; set; }
        public IMetaSettingService MetaSettingService => Ioc.GetService<IMetaSettingService>(false);
        private GridSplitterSetting _setting = new GridSplitterSetting();
        public void Save()
        {
            if (string.IsNullOrEmpty(this.ID)) 
                return;
            this._setting.IsExpanded = this.IsExpanded;
            this.MetaSettingService?.Serilize(this._setting, this.ID);
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(this.ID)) 
                return;
            GridSplitterSetting find = this.MetaSettingService?.Deserilize<GridSplitterSetting>(this.ID);
            this._setting = find ?? new GridSplitterSetting() 
            { 
                IsExpanded = true 
            };
            this.IsExpanded = this._setting.IsExpanded;
        }

    }

    public class GridSplitterSetting : IMetaSetting
    {
        public double HorizontalChange { get; set; }
        public double VerticalChange { get; set; }
        public bool IsExpanded { get; set; }
    }

    public class GridSpliterContainerService
    {
        public static bool GetIsExpanded(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExpandedProperty);
        }

        public static void SetIsExpanded(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExpandedProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.RegisterAttached("IsExpanded", typeof(bool), typeof(GridSpliterContainerService), new PropertyMetadata(default(bool), OnIsExpandedChanged));

        static public void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }
    }
}
