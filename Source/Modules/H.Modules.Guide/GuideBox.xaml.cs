// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using H.Extensions.Attach;
using H.Extensions.StoryBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace H.Modules.Guide
{
    public partial class GuideBox : FrameworkElement
    {

        public static RoutedCommand NextCommand = new RoutedCommand();
        public static RoutedCommand SkipCommand = new RoutedCommand();
        static GuideBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GuideBox), new FrameworkPropertyMetadata(typeof(GuideBox)));
        }

        private GuideTree _guideTree = null;
        private readonly Border _conver = new Border();
        private readonly Path _path = new Path();
        private readonly UIElement _element;
        private readonly ContentControl _contentControl = new ContentControl() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

        public GuideBox(UIElement element)
        {
            this._element = element;

            if (element is FrameworkElement framework)
            {
                framework.SizeChanged += (l, k) =>
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                              {
                                  this.Refresh();
                                  this.RefreshClip(element);
                              }));

                };
            }
            {
                CommandBinding binding = new CommandBinding(NextCommand);
                binding.Executed += (l, k) =>
                {
                    if (_guideTree.Next() == null)
                    {
                        this.Close();
                        return;
                    }

                    this.CurrentIndex = _guideTree.CurrentIndex;
                    this.ScrollTo();
                    this.AutoClick();
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                               {
                                   this.RefreshClip(this._element);
                               }));

                };

                this.CommandBindings.Add(binding);
            }


            {
                CommandBinding binding = new CommandBinding(SkipCommand);
                binding.Executed += (l, k) =>
                {
                    this.Close();
                };

                this.CommandBindings.Add(binding);
            }

            this.Loaded += (l, k) =>
            {
                this.Begin();
            };
            this._conver.Background = this.Background;
            this._path.Style = this.PathStyle;
            this._contentControl.Template = this.ControlTemplate;
            this.AddVisualChild(_conver);
            this.AddVisualChild(_path);
            this.AddVisualChild(_contentControl);
        }

        #region -  属性 -

        public ControlTemplate ControlTemplate
        {
            get { return (ControlTemplate)GetValue(ControlTemplateProperty); }
            set { SetValue(ControlTemplateProperty, value); }
        }

        public static readonly DependencyProperty ControlTemplateProperty =
            DependencyProperty.Register("ControlTemplate", typeof(ControlTemplate), typeof(GuideBox), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
            {
                GuideBox control = d as GuideBox;

                if (control == null) return;

                if (e.OldValue is ControlTemplate o)
                {

                }

                if (e.NewValue is ControlTemplate n)
                {
                    control._contentControl.Template = n;
                }

            }));



        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(GuideBox), new FrameworkPropertyMetadata(default(Style), (d, e) =>
            {
                GuideBox control = d as GuideBox;

                if (control == null) return;

                if (e.OldValue is Style o)
                {

                }

                if (e.NewValue is Style n)
                {
                    control._path.Style = n;
                }

            }));

        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            private set { SetValue(CurrentIndexProperty, value); }
        }

        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(GuideBox), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                GuideBox control = d as GuideBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }
            }));

        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(GuideBox), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                GuideBox control = d as GuideBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }

            }));

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(GuideBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
            {
                GuideBox control = d as GuideBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {
                    control._conver.Background = n;
                }

            }));


        public static readonly RoutedEvent ClosedRoutedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(GuideBox));

        public event RoutedEventHandler Closed
        {
            add { this.AddHandler(ClosedRoutedEvent, value); }
            remove { this.RemoveHandler(ClosedRoutedEvent, value); }
        }
        protected void OnClosed()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClosedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        #endregion


        #region - FrameworkElement -
        protected override Size ArrangeOverride(Size finalSize)
        {
            _conver.Arrange(new Rect(new Point(0, 0), finalSize));
            _path.Arrange(new Rect(new Point(0, 0), finalSize));
            this._contentControl.Arrange(new Rect(new Point(0, 0), finalSize));
            return base.ArrangeOverride(finalSize);
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            _conver.Measure(availableSize);
            _path.Measure(availableSize);
            this._contentControl.Measure(availableSize);
            return base.MeasureOverride(availableSize);
        }
        protected override Visual GetVisualChild(int index)
        {
            if (base.VisualChildrenCount == index)
                return this._conver;
            if (base.VisualChildrenCount + 1 == index)
                return this._path;
            if (base.VisualChildrenCount + 2 == index)
                return this._contentControl;

            return base.GetVisualChild(index);
        }
        protected override int VisualChildrenCount => base.VisualChildrenCount + 3;
        #endregion

        private void LoadData(UIElement element)
        {
            List<UIElement> items = element.GetChildren<UIElement>(l => Cattach.GetUseGuide(l) && Cattach.GetGuideParentTitle(l) == null)?.ToList();
            IEnumerable<UIElement> roots = items.Where(l => Cattach.GetGuideParentTitle(l) == null && l.Visibility == Visibility.Visible);
            List<GuideTreeNode> guideTreeNodes = new List<GuideTreeNode>();
            Action<GuideTreeNode> build = null;
            build = parent =>
             {
                 //var gt = Cattach.GetGuideTitle(parent.Element);
                 IEnumerable<UIElement> children = items.Where(l => Cattach.GetGuideParentTitle(l) == Cattach.GetGuideTitle(parent.Element)?.ToString() && l.Visibility == Visibility.Visible);

                 foreach (UIElement child in children)
                 {
                     if (child.Visibility != Visibility.Visible)
                         continue;
                     if (child.IsVisible == false)
                         continue;
                     if (child.RenderSize.Width < 10 || child.RenderSize.Height < 10)
                         continue;
                     if (child is FrameworkElement framework)
                     {
                         if (framework.IsLoaded == false)
                             return;

                     }
                     GuideTreeNode childNode = new GuideTreeNode(child);
                     childNode.Parent = parent;
                     parent.Chidren.Add(childNode);
                     build.Invoke(childNode);
                 }
             };

            foreach (UIElement root in roots)
            {
                if (root.Visibility != Visibility.Visible)
                    continue;
                if (root.IsVisible == false)
                    continue;
                if (root.RenderSize.Width < 10 || root.RenderSize.Height < 10)
                    continue;
                GuideTreeNode rootNode = new GuideTreeNode(root);
                guideTreeNodes.Add(rootNode);
                build.Invoke(rootNode);
            }

            this._guideTree = new GuideTree(guideTreeNodes);
            this.CurrentIndex = 1;
            this.Total = items.Count;
        }

        private void Refresh()
        {
            RectangleGeometry outRect = new RectangleGeometry(new Rect(this.RenderSize));
            RectangleGeometry childRect = new RectangleGeometry(new Rect(0, 0, 0, 0), 1, 1);
            CombinedGeometry combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, outRect, childRect);
            this._conver.Background = this.Background;
            this._conver.Clip = combinedGeometry;
            this._path.Data = new RectangleGeometry() { Rect = new Rect() };
        }

        private void AutoClick()
        {
            if (!this.IsLoaded)
                return;
            if (_guideTree.Current == null)
                return;
            bool use = Cattach.GetGuideUseClick(_guideTree.Current.Element);
            if (!use)
                return;
            if (_guideTree.Current.Element is ListBoxItem listBoxItem)
                listBoxItem.IsSelected = true;
            if (_guideTree.Current.Element is TabItem tabItem)
                tabItem.IsSelected = true;

            //  Do ：加载本级节点 
            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                       {
                           List<UIElement> children = this.GetChildren<UIElement>(l => Cattach.GetUseGuide(l) && Cattach.GetGuideParentTitle(l) == Cattach.GetGuideTitle(_guideTree.Current.Element)?.ToString() && l.Visibility == Visibility.Visible)?.ToList();

                           _guideTree.Current.Chidren.Clear();
                           foreach (UIElement child in children)
                           {
                               GuideTreeNode childNode = new GuideTreeNode(child);
                               childNode.Parent = _guideTree.Current;
                               _guideTree.Current.Chidren.Add(childNode);
                           }
                       }));
        }

        private void ScrollTo()
        {
            ScrollViewer scrollViewer = _guideTree.Current.Element.GetParent<ScrollViewer>();
            if (scrollViewer == null)
                return;
            double currentScrollPosition = scrollViewer.VerticalOffset;
            Point point = new Point(0, currentScrollPosition);
            Point targetPosition = _guideTree.Current.Element.TransformToVisual(scrollViewer).Transform(point);
            scrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }

        private void RefreshClip(UIElement element)
        {
            if (!this.IsLoaded)
                return;
            if (this._guideTree.Current == null)
                return;
            Point point = this._guideTree.Current.Element.TranslatePoint(new Point(0, 0), element);
            Rect rect = new Rect(point, this._guideTree.Current.Element.RenderSize);
            {
                RectAnimation da = new RectAnimation();
                da.To = rect;
                da.EasingFunction = EasingFunctionFactroy.PowerEase;
                Storyboard sb = StoryboardFactory.Create();
                sb.Duration = TimeSpan.FromMilliseconds(1000);
                sb.Children.Add(da);
                Storyboard.SetTarget(da, this._conver);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1).(2)", new DependencyProperty[] { UIElement.ClipProperty, CombinedGeometry.Geometry2Property, RectangleGeometry.RectProperty }));
                sb.Begin();
            }

            {
                double thickness = this._path.StrokeThickness;
                RectAnimation da = new RectAnimation();
                da.To = new Rect(new Point(point.X - thickness * 0.5, point.Y - thickness * 0.5), new Size(this._guideTree.Current.Element.RenderSize.Width + thickness * 1, this._guideTree.Current.Element.RenderSize.Height + thickness * 1));
                da.EasingFunction = EasingFunctionFactroy.PowerEase;
                Storyboard sb = StoryboardFactory.Create();
                sb.Duration = TimeSpan.FromMilliseconds(1000);
                sb.Children.Add(da);
                Storyboard.SetTarget(da, this._path);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.DataProperty, RectangleGeometry.RectProperty }));
                sb.Begin();
            }
            object title = Cattach.GetGuideTitle(this._guideTree.Current.Element);
            Cattach.SetGuideTitle(this._contentControl, title);
            this._contentControl.Content = Cattach.GetGuideData(this._guideTree.Current.Element);
            this._contentControl.ContentTemplate = Cattach.GetGuideDataTemplate(this._guideTree.Current.Element);
            this._contentControl.Measure(this.RenderSize);
            this._contentControl.Arrange(new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            double x = rect.Right;

            if (rect.Width > this.RenderSize.Width - rect.Right)
                x = rect.Left - this._contentControl.RenderSize.Width;

            if (rect.Left > this.RenderSize.Width - rect.Right && rect.Left > rect.Width)
                x = rect.Left - this._contentControl.RenderSize.Width;

            if (x + this._contentControl.RenderSize.Width > this.RenderSize.Width)
                x = this.RenderSize.Width - this._contentControl.RenderSize.Width;

            if (x < 0)
                x = this.RenderSize.Width / 2 - this._contentControl.RenderSize.Width / 2;

            double y = rect.Bottom;
            if (rect.Height > this.RenderSize.Height - rect.Bottom)
                y = rect.Top;

            if (rect.Top > this.RenderSize.Height - rect.Bottom && rect.Top > rect.Width)
                y = rect.Bottom - this._contentControl.RenderSize.Height;

            if (y + this._contentControl.RenderSize.Height > this.RenderSize.Height)
                y = this.RenderSize.Height - this._contentControl.RenderSize.Height;

            if (this._contentControl.CheckDefaultTransformGroup())
                this._contentControl.BeginAnimationXY(x, y, GuideOptions.Instance.AnimationDuration);
        }

        private void Close()
        {
            this.OnClosed();
        }

        private void Begin()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                this.Refresh();
                this.LoadData(this._element);
                this.AutoClick();
                this.RefreshClip(this._element);

            }));
        }
    }
}
