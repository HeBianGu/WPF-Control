// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Extensions.StoryBoard;
using H.Extensions.Attach;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace H.Modules.Guide;

public partial class GuideBox : FrameworkElement
{

    public static RoutedCommand NextCommand = new RoutedCommand();
    public static RoutedCommand SkipCommand = new RoutedCommand();
    static GuideBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(GuideBox), new FrameworkPropertyMetadata(typeof(GuideBox)));
    }

    private GuideTree _guideTree = null;
    private readonly Border _backgroundBorder = new Border();
    private readonly Path _currentBound = new Path();
    private readonly UIElement _element;
    private readonly ContentControl _contentControl = new ContentControl() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

    private readonly Predicate<UIElement> _predicate;
    public GuideBox(UIElement element, Predicate<UIElement> predicate = null)
    {
        this._element = element;
        this._predicate = predicate;
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
        this._backgroundBorder.Background = this.Background;
        this._currentBound.Style = this.PathStyle;
        this._contentControl.Template = this.ControlTemplate;
        this.AddVisualChild(_backgroundBorder);
        this.AddVisualChild(_currentBound);
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
                control._currentBound.Style = n;
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
                control._backgroundBorder.Background = n;
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
        _backgroundBorder.Arrange(new Rect(new Point(0, 0), finalSize));
        _currentBound.Arrange(new Rect(new Point(0, 0), finalSize));
        this._contentControl.Arrange(new Rect(new Point(0, 0), finalSize));
        return base.ArrangeOverride(finalSize);
    }
    protected override Size MeasureOverride(Size availableSize)
    {
        _backgroundBorder.Measure(availableSize);
        _currentBound.Measure(availableSize);
        this._contentControl.Measure(availableSize);
        return base.MeasureOverride(availableSize);
    }
    protected override Visual GetVisualChild(int index)
    {
        if (base.VisualChildrenCount == index)
            return this._backgroundBorder;
        if (base.VisualChildrenCount + 1 == index)
            return this._currentBound;
        if (base.VisualChildrenCount + 2 == index)
            return this._contentControl;

        return base.GetVisualChild(index);
    }
    protected override int VisualChildrenCount => base.VisualChildrenCount + 3;
    #endregion

    private void LoadData(UIElement element)
    {
        var tree = element.GetGuideTree(this._predicate);
        this._guideTree = tree;
        this.CurrentIndex = 1;
        this.Total = tree.Roots.Count;
    }

    private void Refresh()
    {
        RectangleGeometry outRect = new RectangleGeometry(new Rect(this.RenderSize));
        RectangleGeometry childRect = new RectangleGeometry(new Rect(0, 0, 0, 0), 1, 1);
        CombinedGeometry combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, outRect, childRect);
        this._backgroundBorder.Background = this.Background;
        this._backgroundBorder.Clip = combinedGeometry;
        this._currentBound.Data = new RectangleGeometry() { Rect = new Rect() };
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

        var currentElement = this._guideTree.Current.Element;
        Point point = currentElement.TranslatePoint(new Point(0, 0), element);
        Rect rect = new Rect(point, currentElement.RenderSize);
        this._backgroundBorder.Clip = new CombinedGeometry(GeometryCombineMode.Exclude, new RectangleGeometry(new Rect(this._backgroundBorder.RenderSize)), new RectangleGeometry(rect));
        double thickness = this._currentBound.StrokeThickness;
        this._currentBound.Data = new RectangleGeometry(new Rect(new Point(point.X - (thickness * 0.5),
            point.Y - (thickness * 0.5)),
            new Size(currentElement.RenderSize.Width + (thickness * 1),
            currentElement.RenderSize.Height + (thickness * 1))));

        object title = Cattach.GetGuideTitle(currentElement);
        //if (title == null)
        //{
        //    if (currentElement is TextBlock textBlock)
        //        title = textBlock.Text;
        //    if (currentElement is ContentControl contentControl)
        //        title = contentControl.Content;
        //    if (currentElement is HeaderedContentControl headeredContentControl)
        //        title = headeredContentControl.Header;
        //    if (currentElement is HeaderedItemsControl headeredItemsControl)
        //        title = headeredItemsControl.Header;
        //    if (title == null && currentElement is FrameworkElement framework)
        //        title = framework.ToolTip;
        //}

        Cattach.SetGuideTitle(this._contentControl, title);
        string icon = Cattach.GetGuideIcon(currentElement);
        Cattach.SetGuideIcon(this._contentControl, icon);
        string version = Cattach.GetGuideAssemblyVersion(currentElement);
        Cattach.SetGuideAssemblyVersion(this._contentControl, version);
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
            x = (this.RenderSize.Width / 2) - (this._contentControl.RenderSize.Width / 2);

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
            if (this.Total == 0)
                this.Close();
            this.AutoClick();
            this.RefreshClip(this._element);

            this._contentControl.Focus();
            var nbtn = this._contentControl.GetChild<Button>(x => x.Name == "bt_next");
            nbtn?.Focus();
        }));
    }
}
