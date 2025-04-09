// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using H.Services.Common.Serialize.Meta;
using H.Extensions.TextJsonable;
using H.Mvvm.Commands;

namespace H.Controls.GridSplitterBox
{
    [TemplatePart(Name = GridSplitterName, Type = typeof(System.Windows.Controls.GridSplitter))]
    [TemplatePart(Name = "PART_Row_Menu", Type = typeof(System.Windows.Controls.RowDefinition))]
    [TemplatePart(Name = "PART_Column_Menu", Type = typeof(System.Windows.Controls.ColumnDefinition))]
    [TemplatePart(Name = "PART_Menu", Type = typeof(System.Windows.Controls.Grid))]
    public partial class GridSplitterBox : ContentControl, IMetaSetting
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(GridSplitterBox), "S.GridSplitterBox.Default");
        public static ComponentResourceKey RightKey => new ComponentResourceKey(typeof(GridSplitterBox), "S.GridSplitterBox.Right");
        public static ComponentResourceKey TopKey => new ComponentResourceKey(typeof(GridSplitterBox), "S.GridSplitterBox.Top");
        public static ComponentResourceKey BottomKey => new ComponentResourceKey(typeof(GridSplitterBox), "S.GridSplitterBox.Bottom");

        public static ComponentResourceKey ToggleKey => new ComponentResourceKey(typeof(GridSplitterBox), "S.GridSplitterBox.Toggle");


        private const string GridSplitterName = "PART_GridSplitter";

        static GridSplitterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridSplitterBox), new FrameworkPropertyMetadata(typeof(GridSplitterBox)));
        }

        private System.Windows.Controls.GridSplitter _gridSplitter = null;
        private Grid _menuGrid = null;
        private ColumnDefinition _menuColumnDefinition = null;
        private RowDefinition _menuRowDefinition = null;
        private double _menuWidthTemp;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _gridSplitter = this.Template.FindName(GridSplitterName, this) as System.Windows.Controls.GridSplitter;
            if (_gridSplitter == null)
                return;
            _menuGrid = this.Template.FindName("PART_Menu", this) as Grid;
            if (_menuGrid == null)
                return;
            _menuColumnDefinition = this.Template.FindName("PART_Column_Menu", this) as ColumnDefinition;
            _menuRowDefinition = this.Template.FindName("PART_Row_Menu", this) as RowDefinition;
            _menuWidthTemp = this.MenuWidth.Value;
            this.Load();
            if (this.MenuDock == Dock.Right)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp - this._setting.HorizontalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.HorizontalChange = _menuWidthTemp - this.MenuWidth.Value + k.HorizontalChange;
                    this.RefreshButtonState();
                    this.Save(out string message);
                };
            }
            else if (this.MenuDock == Dock.Bottom)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp - this._setting.VerticalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.VerticalChange = _menuWidthTemp - this.MenuWidth.Value + k.VerticalChange;
                    this.RefreshButtonState();
                    this.Save(out string message);
                };
            }

            else if (this.MenuDock == Dock.Left)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp + this._setting.HorizontalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.HorizontalChange = this.MenuWidth.Value - _menuWidthTemp + k.HorizontalChange;
                    this.RefreshButtonState();
                    this.Save(out string message);
                };
            }
            else if (this.MenuDock == Dock.Top)
            {
                this.MenuWidth = new GridLength(this._menuWidthTemp + this._setting.VerticalChange);
                _gridSplitter.DragCompleted += (l, k) =>
                {
                    this._setting.VerticalChange = this.MenuWidth.Value - _menuWidthTemp + k.VerticalChange;
                    this.RefreshButtonState();
                    this.Save(out string message);
                };
            }

            {
                CommandBinding cb = new CommandBinding(Commands.Switch);
                cb.Executed += (l, k) =>
                {
                    this.RefreshState();
                };
                this.CommandBindings.Add(cb);
            }

            this.RefreshState();
        }



        private void RefreshButtonState()
        {
            if (this.Mode == GridSplitteMode.Hidden)
                return;
            if (this.MenuWidth.Value == this.MenuMinWidth)
                this.IsExpanded = false;
            if (this.MenuWidth.Value == this.MenuMaxWidth)
                this.IsExpanded = true;
        }


        private void RefreshState()
        {
            if (this.IsExpanded)
            {
                if (this.Mode == GridSplitteMode.Hidden)
                {
                    if (this._menuGrid != null)
                        this._menuGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    if (this._menuColumnDefinition != null)
                        this._menuColumnDefinition.Width = new GridLength(this.MenuMaxWidth);
                    if (this._menuRowDefinition != null)
                        this._menuRowDefinition.Height = new GridLength(this.MenuMaxWidth);
                }
            }
            else
            {
                if (this.Mode == GridSplitteMode.Hidden)
                {
                    if (this._menuGrid != null)
                        this._menuGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (this._menuColumnDefinition != null)
                        this._menuColumnDefinition.Width = new GridLength(this.MenuMinWidth);
                    if (this._menuRowDefinition != null)
                        this._menuRowDefinition.Height = new GridLength(this.MenuMinWidth);
                }
            }
        }


        public GridSplitteMode Mode
        {
            get { return (GridSplitteMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }


        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(GridSplitteMode), typeof(GridSplitterBox), new FrameworkPropertyMetadata(GridSplitteMode.Hidden, (d, e) =>
            {
                GridSplitterBox control = d as GridSplitterBox;

                if (control == null) return;

                if (e.OldValue is GridSplitteMode o)
                {

                }

                if (e.NewValue is GridSplitteMode n)
                {

                }

            }));


        public object MenuContent
        {
            get { return GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }


        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty MenuTempateProperty =
            DependencyProperty.Register("MenuTempate", typeof(DataTemplate), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty GridSpliteWidthProperty =
            DependencyProperty.Register("GridSpliteWidth", typeof(double), typeof(GridSplitterBox), new FrameworkPropertyMetadata(5.0, (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty MenuMinWidthProperty =
            DependencyProperty.Register("MenuMinWidth", typeof(double), typeof(GridSplitterBox), new FrameworkPropertyMetadata(10.0, (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty MenuMaxWidthProperty =
            DependencyProperty.Register("MenuMaxWidth", typeof(double), typeof(GridSplitterBox), new FrameworkPropertyMetadata(300.0, (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty MenuWidthProperty =
            DependencyProperty.Register("MenuWidth", typeof(GridLength), typeof(GridSplitterBox), new FrameworkPropertyMetadata(new GridLength(200.0), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

                 if (control == null) return;

                 if (e.OldValue is GridLength o)
                 {

                 }

                 if (e.NewValue is GridLength n)
                 {

                 }

             }));

        public Brush GridSpliterBackground
        {
            get { return (Brush)GetValue(GridSpliterBackgroundProperty); }
            set { SetValue(GridSpliterBackgroundProperty, value); }
        }


        public static readonly DependencyProperty GridSpliterBackgroundProperty =
            DependencyProperty.Register("GridSpliterBackground", typeof(Brush), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty ToggleHorizontalAlignmentProperty =
            DependencyProperty.Register("ToggleHorizontalAlignment", typeof(HorizontalAlignment), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty ToggleVerticalAlignmentProperty =
            DependencyProperty.Register("ToggleVerticalAlignment", typeof(VerticalAlignment), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(VerticalAlignment), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty MenuDockProperty =
            DependencyProperty.Register("MenuDock", typeof(Dock), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(Dock), (d, e) =>
             {
                 GridSplitterBox control = d as GridSplitterBox;

                 if (control == null) return;

                 if (e.OldValue is Dock o)
                 {

                 }

                 if (e.NewValue is Dock n)
                 {

                 }

             }));

        public Style ToggleStyle
        {
            get { return (Style)GetValue(ToggleStyleProperty); }
            set { SetValue(ToggleStyleProperty, value); }
        }


        public static readonly DependencyProperty ToggleStyleProperty =
            DependencyProperty.Register("ToggleStyle", typeof(Style), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(Style), (d, e) =>
            {
                GridSplitterBox control = d as GridSplitterBox;

                if (control == null) return;

                if (e.OldValue is Style o)
                {

                }

                if (e.NewValue is Style n)
                {

                }

            }));


        public string OpenGeometry
        {
            get { return (string)GetValue(OpenGeometryProperty); }
            set { SetValue(OpenGeometryProperty, value); }
        }


        public static readonly DependencyProperty OpenGeometryProperty =
            DependencyProperty.Register("OpenGeometry", typeof(string), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                GridSplitterBox control = d as GridSplitterBox;

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


        public static readonly DependencyProperty CloseGeometryProperty =
            DependencyProperty.Register("CloseGeometry", typeof(string), typeof(GridSplitterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                GridSplitterBox control = d as GridSplitterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }


        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(GridSplitterBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                GridSplitterBox control = d as GridSplitterBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.Save(out string message);
            }));


        public bool UseToggle
        {
            get { return (bool)GetValue(UseToggleProperty); }
            set { SetValue(UseToggleProperty, value); }
        }


        public static readonly DependencyProperty UseToggleProperty =
            DependencyProperty.Register("UseToggle", typeof(bool), typeof(GridSplitterBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                GridSplitterBox control = d as GridSplitterBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public string ID { get; set; }

        public IMetaSettingService MetaSettingService => new TextJsonMetaSettingService();

        private GridSplitterSetting _setting = new GridSplitterSetting();

        public bool Save(out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(this.ID))
            {
                message = "ID为空";
                return false;
            }
            this._setting.IsExpanded = this.IsExpanded;
            this.MetaSettingService?.Serilize(this._setting, this.ID);
            return true;
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

    public class GridSplitterSetting
    {
        public double HorizontalChange { get; set; }
        public double VerticalChange { get; set; }
        public bool IsExpanded { get; set; }
    }

    public enum GridSplitteMode
    {
        Extend, Hidden
    }
}
