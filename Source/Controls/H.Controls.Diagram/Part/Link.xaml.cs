// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace H.Controls.Diagram
{
    public interface ILink
    {
        //void RefreshLayout();
    }

    /// <summary> 连线 </summary>
    public partial class Link : Part, ILink
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Link), "S.Link.Default");
        public static ComponentResourceKey DashKey => new ComponentResourceKey(typeof(Link), "S.Link.Dash");

        static Link()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Link), new FrameworkPropertyMetadata(typeof(Link)));
        }

        Path _path = new Path();
        public Link()
        {
            this.Content = new DefaultLinkData();
            //this.DataContext = this.Content;
            this.AddVisualChild(_path);
        }

        public static Link Create(Node from, Node to)
        {
            Link link = new Link();
            link.FromNode = from;
            link.ToNode = to;
            if (from.Content is ILinkInitializer initializer1)
                initializer1.InitLink(link);
            if (to.Content is ILinkInitializer initializer3)
                initializer3.InitLink(link);

            if (from.Content is ILinkDataCreator creator)
                link.Content = creator.CreateLinkData();

            if (link.Content is IFlowable)
            {
                from.LinksOutOf.Add(link);
                to.LinksInto.Add(link);
            }
            else
            {
                from.ConnectLinks.Add(link);
                to.ConnectLinks.Add(link);
            }



            return link;
        }

        public static Link Create(Node fromNode, Node toNode, Port fromPort, Port toPort)
        {
            Link link = new Link();
            link.FromNode = fromNode;
            link.ToNode = toNode;
            link.FromPort = fromPort;
            link.ToPort = toPort;

            if (fromNode.Content is ILinkInitializer initializer1)
                initializer1.InitLink(link);
            if (fromPort?.Content is ILinkInitializer initializer2)
                initializer2.InitLink(link);
            if (toNode.Content is ILinkInitializer initializer3)
                initializer3.InitLink(link);
            if (toPort?.Content is ILinkInitializer initializer4)
                initializer4.InitLink(link);

            if (fromNode.Content is ILinkDataCreator creator1)
                link.Content = creator1.CreateLinkData();
            if (fromPort?.Content is ILinkDataCreator creator)
                link.Content = creator.CreateLinkData();


            if (link.Content is IFlowable)
            {
                fromNode.LinksOutOf.Add(link);
                toNode.LinksInto.Add(link);
            }
            else
            {
                fromNode.ConnectLinks.Add(link);
                toNode.ConnectLinks.Add(link);
            }

            return link;
        }

        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(Link), new PropertyMetadata(default(Style), (d, e) =>
            {
                Link control = d as Link;
                if (control == null) return;
                Style config = e.NewValue as Style;
                control._path.Style = config;

            }));


        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(Link), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 Link control = d as Link;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(Link), new FrameworkPropertyMetadata(default(double), (d, e) =>
             {
                 Link control = d as Link;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));



        /// <summary> 从那个节点 </summary>
        public Node FromNode { get; set; }

        /// <summary> 到哪个节点 </summary>
        public Node ToNode { get; set; }

        public Port FromPort { get; set; }

        public Port ToPort { get; set; }

        Point _center;
        void Draw(Point start, Point end)
        {
#if DEBUG
            DateTime dateTime = DateTime.Now;
#endif

            Diagram diagram = this.GetDiagram();
            if (diagram?.LinkDrawer == null) return;
            this._path.Data = diagram.LinkDrawer.DrawPath(this, out Point center);
            //this._path.DataContext = this.DataContext;
            _center = center;
            //bool candrop = Link.GetIsCanDrop(this);
            //Link.SetIsCanDrop(this._path, Link.GetIsCanDrop(this));
            //Link.SetIsDragEnter(this._path, Link.GetIsDragEnter(this));
            //this._path.Style = this.PathStyle;
            //this.InvalidateArrange();
#if DEBUG
            var span = DateTime.Now - dateTime;
            System.Diagnostics.Debug.WriteLine("Link.Draw：" + span.ToString());
#endif 
        }

        public Node GetConnectNode(Node node)
        {
            return this.FromNode == node ? this.ToNode : this.FromNode;
        }

        public void Update()
        {
            Point start = LinkLayer.GetStart(this);
            Point end = LinkLayer.GetEnd(this);

            //System.Diagnostics.Debug.WriteLine("start:" + start);
            //System.Diagnostics.Debug.WriteLine("end:" + end);
            this.Draw(start, end);

            //this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //this.Arrange(new Rect(new Point(0, 0), this.DesiredSize));
        }

        public override void Delete()
        {
            base.Delete();
            //  Do ：删除节点关联关系
            this.Clear();
        }

        public override void Clear()
        {
            this.FromNode?.LinksOutOf.Remove(this);
            this.ToNode?.LinksInto.Remove(this);

            this.FromNode = null;
            this.ToNode = null;

            this.FromPort = null;
            this.ToPort = null;
        }

        public void SetDefaultMessage(string message)
        {
            if (this.Content is DefaultLinkData link)
            {
                link.Message = message;
            }
        }

        public override Part GetPrevious()
        {
            return this.FromNode;
        }

        public override Part GetNext()
        {
            return this.ToNode;
        }
    }

    public partial class Link
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            System.Diagnostics.Debug.WriteLine("Link.MeasureOverride");
            for (int i = 0; i < this.VisualChildrenCount; i++)
            {
                UIElement child = VisualTreeHelper.GetChild(this, i) as UIElement;
                if (child == null) continue;
                child.Measure(availableSize);
                //  Do ：按Path的宽度计算
                if (child is Path path)
                {
                    return child.DesiredSize;
                }
            }

            return base.MeasureOverride(availableSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            System.Diagnostics.Debug.WriteLine("Link.OnRender");
            base.OnRender(drawingContext);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //for (int i = 0; i < this.VisualChildrenCount; i++)
            //{
            //    UIElement child = VisualTreeHelper.GetChild(this, i) as UIElement;

            //    child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            //    if (child is Path path)
            //    {
            //        child.Arrange(new Rect(finalSize));
            //    }
            //    else
            //    {
            //        var start = LinkLayer.GetStart(this);

            //        var end = LinkLayer.GetEnd(this);

            //        Rect rect = new Rect(); 

            //        child.Arrange(this.Path.Data.Bounds);
            //    }
            //}

            System.Diagnostics.Debug.WriteLine("Link.ArrangeOverride");
            if (this._path == null)
                return finalSize;
            this._path.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this._path.Arrange(new Rect(finalSize));

            //this._path.Data.GetFlattenedPathGeometry().GetPointAtFractionLength(0.5, out Point p1, out Point _);
            //System.Diagnostics.Debug.WriteLine("GetPointAtFractionLength " + p1);
            UIElement child = VisualTreeHelper.GetChild(this, 1) as UIElement;
            child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            if (this._path.Data != null && this._path.Data.Bounds != Rect.Empty)
            {
                //Rect rect = Rect.Inflate(this._path.Data.Bounds, 500, 500);

                Rect rect = new Rect(_center.X - child.DesiredSize.Width / 2, _center.Y - child.DesiredSize.Height / 2, child.DesiredSize.Width, child.DesiredSize.Height);
                child.Arrange(rect);
            }
            return finalSize;


        }

        protected override int VisualChildrenCount => base.VisualChildrenCount + 1;

        //UIElementCollection _children=null;

        //protected override Visual GetVisualChild(int index)
        //{ 
        //    if (index >= base.VisualChildrenCount) return this.Path;

        //    return base.GetVisualChild(index);
        //}

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0) return this._path;

            return base.GetVisualChild(index - 1);
        }
    }

}
