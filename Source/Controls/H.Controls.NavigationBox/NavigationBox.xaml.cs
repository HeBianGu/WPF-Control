global using System.Collections;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Media;
global using H.Extensions.Common;

namespace H.Controls.NavigationBox
{

    /// <summary> 用于根据索引导航到指定Item效果 </summary>
    [TemplatePart(Name = "PART_NAVIGATION", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_SCROLLVIEWER", Type = typeof(ScrollViewer))]
    public partial class NavigationBox : ListBox
    {
        static NavigationBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationBox), new FrameworkPropertyMetadata(typeof(NavigationBox)));
        }

        private ListBox _navigation;
        private ScrollViewer _scrollviewer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._navigation = this.Template.FindName("PART_NAVIGATION", this) as ListBox;
            this._scrollviewer = this.Template.FindName("PART_SCROLLVIEWER", this) as ScrollViewer;
            this._navigation.SelectionChanged += (l, k) =>
            {
                int index = Math.Min(this._navigation.SelectedIndex, this.Items.Count - 1);
                if (index < 0) 
                    return;
                //this.ScrollIntoView(this.Items[index]);
                UIElement element = this.Items[index] as UIElement;
                UIElement find = this.ItemContainerGenerator.ContainerFromIndex(index) as UIElement;
                this.ScrollTo(find);
            };

            //  Do ：设置滚动到指定位置后导航跟着改变
            this._scrollviewer.ScrollChanged += (l, k) =>
            {
                //if (k.VerticalChange > 0)
                //{
                //    Point n = new Point() { X = this.HitTestPoint.X, Y = this._scrollviewer.ActualHeight - this.HitTestPoint.Y };

                //    Point point = this._scrollviewer.TranslatePoint(n, this);

                //    PointHitTestParameters parameters = new PointHitTestParameters(point);

                //    VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
                //}
                //else
                //{
                //    Point point = this._scrollviewer.TranslatePoint(this.HitTestPoint, this);

                //    PointHitTestParameters parameters = new PointHitTestParameters(point);

                //    VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
                //}

                Point point = this._scrollviewer.TranslatePoint(this.HitTestPoint, this);
                PointHitTestParameters parameters = new PointHitTestParameters(point);
                VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
            };
        }

        public void ScrollTo(UIElement element)
        {
            ScrollViewer scrollViewer = element.GetParent<ScrollViewer>();
            if (scrollViewer == null) 
                return;
            double currentScrollPosition = scrollViewer.VerticalOffset;
            Point point = new Point(0, currentScrollPosition);
            Point targetPosition = element.TransformToVisual(scrollViewer).Transform(point);
            scrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }

        public Point HitTestPoint
        {
            get { return (Point)GetValue(HitTestPointProperty); }
            set { SetValue(HitTestPointProperty, value); }
        }


        public static readonly DependencyProperty HitTestPointProperty =
            DependencyProperty.Register("HitTestPoint", typeof(Point), typeof(NavigationBox), new FrameworkPropertyMetadata(new Point(10, 10), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                NavigationBox control = d as NavigationBox;

                if (control == null) return;

                if (e.OldValue is Point o)
                {

                }

                if (e.NewValue is Point n)
                {

                }

            }));

        private HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            if (result.VisualHit is ListBoxItem)
            {
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;

        }

        private HitTestFilterBehavior HitTestFilter(DependencyObject obj)
        {
            Type type = obj.GetType();

            if (type.Name == this.GetType().Name) return HitTestFilterBehavior.ContinueSkipSelf;

            if (obj is ListBoxItem item)
            {
                int index = this.ItemContainerGenerator.IndexFromContainer(item);

                if (index >= 0)
                {
                    if (this._navigation.SelectedIndex != index)
                        this._navigation.SelectedIndex = index;
                }

            }

            return HitTestFilterBehavior.Continue;
        }

        public DataTemplate NavigationDataTemplate
        {
            get { return (DataTemplate)GetValue(NavigationDataTemplateProperty); }
            set { SetValue(NavigationDataTemplateProperty, value); }
        }


        public static readonly DependencyProperty NavigationDataTemplateProperty =
            DependencyProperty.Register("NavigationDataTemplate", typeof(DataTemplate), typeof(NavigationBox), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                NavigationBox control = d as NavigationBox;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public DataTemplate ContainDataTemplate
        {
            get { return (DataTemplate)GetValue(ContainDataTemplateProperty); }
            set { SetValue(ContainDataTemplateProperty, value); }
        }


        public static readonly DependencyProperty ContainDataTemplateProperty =
            DependencyProperty.Register("ContainDataTemplate", typeof(DataTemplate), typeof(NavigationBox), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                NavigationBox control = d as NavigationBox;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public IEnumerable NavigationSource
        {
            get { return (IEnumerable)GetValue(NavigationSourceProperty); }
            set { SetValue(NavigationSourceProperty, value); }
        }


        public static readonly DependencyProperty NavigationSourceProperty =
            DependencyProperty.Register("NavigationSource", typeof(IEnumerable), typeof(NavigationBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
            {
                NavigationBox control = d as NavigationBox;

                if (control == null) return;


                IEnumerable config = e.NewValue as IEnumerable;

            }));


        public Style NavigationStyle
        {
            get { return (Style)GetValue(NavigationStyleProperty); }
            set { SetValue(NavigationStyleProperty, value); }
        }


        public static readonly DependencyProperty NavigationStyleProperty =
            DependencyProperty.Register("NavigationStyle", typeof(Style), typeof(NavigationBox), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                NavigationBox control = d as NavigationBox;

                if (control == null) return;

                if (e.OldValue is Style o)
                {

                }

                if (e.NewValue is Style n)
                {

                }

            }));

    }
}
