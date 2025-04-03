// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Controls.Diagram.Layers;
global using H.Controls.Diagram.Layouts.Base;
global using H.Controls.Diagram.LinkDrawers;
global using H.Common;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Input;
global using System.Windows.Media;
global using H.Common.Interfaces;
global using H.Extensions.Common;
using H.Controls.Diagram.Parts.Base;

namespace H.Controls.Diagram;

public interface IDiagram
{
    IDiagramDataSource DataSource { get; set; }
    List<Link> Links { get; }
    List<Node> Nodes { get; }
    Node SelectedNode { get; set; }
    Part SelectedPart { get; set; }
    void AddLink(Link link);
    void AddNode(params Node[] nodes);
    IEnumerable<Part> GetAllParts(Func<Part, bool> predicate = null);
    void RemoveNode(params Node[] nodes);
    void ZoomTo(Point point);
    void ZoomTo(Rect rect, double scale);
    void ZoomToFit(double scale);
    void ZoomToFit(double scale, params Part[] parts);
}



[TemplatePart(Name = "NodeLayer", Type = typeof(NodeLayer))]
[TemplatePart(Name = "LinkLayer", Type = typeof(LinkLayer))]
[TemplatePart(Name = "DynamicLayer", Type = typeof(LinkLayer))]
public partial class Diagram : ContentControl, IDiagram
{
    public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Diagram), "S.Diagram.Default");

    static Diagram()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Diagram), new FrameworkPropertyMetadata(typeof(Diagram)));
    }

    private readonly List<Layer> _layers = new List<Layer>();
    [Browsable(false)]
    internal readonly Link _dynamicLink = new Link() { Visibility = Visibility.Collapsed };
    [Browsable(false)]
    public NodeLayer NodeLayer { get; private set; }
    [Browsable(false)]
    public LinkLayer LinkLayer { get; private set; }
    [Browsable(false)]
    public LinkLayer DynamicLayer { get; private set; }

    public Diagram()
    {
        this.Loaded += (l, k) =>
            this.RefreshData();

        this.SizeChanged += (l, k) =>
            this.RefreshData();
        {
            CommandBinding binding = new CommandBinding(DiagramCommands.DeleteSelected);
            binding.Executed += (l, k) =>
            {
                List<Part> parts = this.Nodes.SelectMany(x => x.GetParts()).Where(x => x.IsSelected).ToList();
                foreach (Part item in parts)
                {
                    if (item is Node || item is Link)
                        item.Delete();
                }
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.SelectMany(x => x.GetParts()).Any(x => x.IsSelected);
            };
            this.CommandBindings.Add(binding);
            KeyBinding keyBinding = new KeyBinding(DiagramCommands.DeleteSelected, new KeyGesture(Key.Delete));
            this.InputBindings.Add(keyBinding);
        }
        {
            CommandBinding binding = new CommandBinding(DiagramCommands.SelectAll);
            binding.Executed += (l, k) =>
            {
                if (this.Nodes.All(x => x.IsSelected))
                {
                    foreach (Node item in this.Nodes)
                    {
                        item.IsSelected = false;
                    }
                }
                else
                {
                    foreach (Node item in this.Nodes)
                    {
                        item.IsSelected = true;
                    }
                }
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            //binding.CanExecute += (l, k) =>
            //{
            //    k.CanExecute = this.Nodes.Any(x => x.IsSelected == false);
            //};
            this.CommandBindings.Add(binding);
            KeyBinding keyBinding = new KeyBinding(DiagramCommands.SelectAll, Key.A, ModifierKeys.Control);
            this.InputBindings.Add(keyBinding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.ZoomToFit);
            binding.Executed += (l, k) =>
            {
                this.ZoomToFit();
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            this.CommandBindings.Add(binding);

            MouseBinding keyBinding = new MouseBinding(DiagramCommands.ZoomToFit, new MouseGesture(MouseAction.LeftDoubleClick));
            this.InputBindings.Add(keyBinding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Aligment);
            binding.Executed += (l, k) =>
            {
                this.AligmentNodes();
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            this.CommandBindings.Add(binding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Next);
            binding.Executed += (l, k) =>
            {
                Node selected = this.Nodes.FirstOrDefault(x => x.IsSelected);
                if (selected == null)
                {
                    selected = this.Nodes.FirstOrDefault();
                }
                else
                {
                    selected.IsSelected = false;
                    int index = this.Nodes.IndexOf(selected);
                    selected = index == this.Nodes.Count - 1 ? this.Nodes[0] : this.Nodes[index + 1];
                }
                selected.IsSelected = true;
                this.SelectedPart = selected;
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);

            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.Count > 0;
            };
            this.CommandBindings.Add(binding);


            {
                KeyBinding keyBinding = new KeyBinding(DiagramCommands.Next, new KeyGesture(Key.Tab));
                this.InputBindings.Add(keyBinding);
            }
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Previous);
            binding.Executed += (l, k) =>
            {
                Node selected = this.Nodes.FirstOrDefault(x => x.IsSelected);
                if (selected == null)
                {
                    selected = this.Nodes.LastOrDefault();
                }
                else
                {
                    selected.IsSelected = false;
                    int index = this.Nodes.IndexOf(selected);
                    selected = index == 0 ? this.Nodes.LastOrDefault() : this.Nodes[index - 1];
                }
                selected.IsSelected = true;
                this.SelectedPart = selected;
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);

            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.Count > 0;
            };
            this.CommandBindings.Add(binding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.MoveLeft);
            binding.Executed += (l, k) =>
            {
                IEnumerable<Node> selecteds = this.Nodes.Where(x => x.IsSelected);
                foreach (Node item in selecteds)
                {
                    NodeLayer.SetPosition(item, new Point(item.Location.X - 5, item.Location.Y));
                }
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.Count > 0;
            };
            this.CommandBindings.Add(binding);

            KeyBinding keyBinding = new KeyBinding(DiagramCommands.MoveLeft, new KeyGesture(Key.Left));
            this.InputBindings.Add(keyBinding);
        }
        {
            CommandBinding binding = new CommandBinding(DiagramCommands.MoveRight);
            binding.Executed += (l, k) =>
            {
                IEnumerable<Node> selecteds = this.Nodes.Where(x => x.IsSelected);
                foreach (Node item in selecteds)
                {
                    NodeLayer.SetPosition(item, new Point(item.Location.X + 5, item.Location.Y));
                }
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.Count > 0;
            };
            this.CommandBindings.Add(binding);

            KeyBinding keyBinding = new KeyBinding(DiagramCommands.MoveRight, new KeyGesture(Key.Right));
            this.InputBindings.Add(keyBinding);
        }
        {
            CommandBinding binding = new CommandBinding(DiagramCommands.MoveUp);
            binding.Executed += (l, k) =>
            {
                IEnumerable<Node> selecteds = this.Nodes.Where(x => x.IsSelected);
                foreach (Node item in selecteds)
                {
                    NodeLayer.SetPosition(item, new Point(item.Location.X, item.Location.Y - 5));
                }
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.Count > 0;
            };
            this.CommandBindings.Add(binding);

            KeyBinding keyBinding = new KeyBinding(DiagramCommands.MoveUp, new KeyGesture(Key.Up));
            this.InputBindings.Add(keyBinding);
        }
        {
            CommandBinding binding = new CommandBinding(DiagramCommands.MoveDown);
            binding.Executed += (l, k) =>
            {
                IEnumerable<Node> selecteds = this.Nodes.Where(x => x.IsSelected);
                foreach (Node item in selecteds)
                {
                    NodeLayer.SetPosition(item, new Point(item.Location.X, item.Location.Y + 5));
                }
                this.InvokeMessage((binding.Command as RoutedUICommand).Text);
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.Nodes.Count > 0;
            };
            this.CommandBindings.Add(binding);

            KeyBinding keyBinding = new KeyBinding(DiagramCommands.MoveDown, new KeyGesture(Key.Down));
            this.InputBindings.Add(keyBinding);
        }

        //{

        //    ContextMenu contextMenu = new ContextMenu();
        //    foreach (CommandBinding item in this.CommandBindings)
        //    {
        //        if (item.Command is RoutedUICommand routed)
        //            contextMenu.Items.Add(new MenuItem() { Command = item.Command, Header = routed.Text });
        //    }
        //    this.ContextMenu = contextMenu;
        //}
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.LinkLayer = this.Template.FindName("LinkLayer", this) as LinkLayer;
        this.NodeLayer = this.Template.FindName("NodeLayer", this) as NodeLayer;
        this.DynamicLayer = this.Template.FindName("DynamicLayer", this) as LinkLayer;
        this.DynamicLayer.Children.Add(_dynamicLink);
        this._dynamicLink.Style = this.DynamicLinkStyle;
        _layers.Add(this.LinkLayer);
        _layers.Add(this.NodeLayer);
        _layers.Add(this.DynamicLayer);
        this.RefreshData();
    }

    #region - 属性 -

    [Browsable(false)]
    public Style NodeStyle
    {
        get { return (Style)GetValue(NodeStyleProperty); }
        set { SetValue(NodeStyleProperty, value); }
    }

    public static readonly DependencyProperty NodeStyleProperty =
        DependencyProperty.Register("NodeStyle", typeof(Style), typeof(Diagram), new PropertyMetadata(default(Style), (d, e) =>
        {
            Diagram control = d as Diagram;
            if (control == null)
                return;
            Style config = e.NewValue as Style;

        }));

    [Browsable(false)]
    public Style LinkStyle
    {
        get { return (Style)GetValue(LinkStyleProperty); }
        set { SetValue(LinkStyleProperty, value); }
    }

    [Browsable(false)]
    public Style DynamicLinkStyle
    {
        get { return (Style)GetValue(DynamicLinkStyleProperty); }
        set { SetValue(DynamicLinkStyleProperty, value); }
    }

    public static readonly DependencyProperty DynamicLinkStyleProperty =
        DependencyProperty.Register("DynamicLinkStyle", typeof(Style), typeof(Diagram), new PropertyMetadata(default(Style), (d, e) =>
        {
            Diagram control = d as Diagram;
            if (control == null)
                return;
            Style config = e.NewValue as Style;
        }));

    public static readonly DependencyProperty LinkStyleProperty =
        DependencyProperty.Register("LinkStyle", typeof(Style), typeof(Diagram), new PropertyMetadata(default(Style), (d, e) =>
        {
            Diagram control = d as Diagram;
            if (control == null)
                return;
            Style config = e.NewValue as Style;
        }));

    [Display(Name = "布局方式", GroupName = "显示设置")]
    public ILayout Layout
    {
        get { return (ILayout)GetValue(LayoutProperty); }
        set { SetValue(LayoutProperty, value); }
    }


    public static readonly DependencyProperty LayoutProperty =
        DependencyProperty.Register("Layout", typeof(ILayout), typeof(Diagram), new FrameworkPropertyMetadata(default(ILayout), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        {
            Diagram control = d as Diagram;
            if (control == null)
                return;
            ILayout config = e.NewValue as ILayout;
            if (config == null)
                return;
            config.Diagram = control;
            //  Do ：切换布局时动画显示
            bool temp = config.Diagram.UseAnimation;
            config.Diagram.UseAnimation = true;
            //control.RefreshData();
            control.RefreshLayout();
            config.Diagram.UseAnimation = temp;

        }));

    [Browsable(false)]
    public List<Link> Links { get; private set; } = new List<Link>();
    [Browsable(false)]
    public List<Node> Nodes { get; private set; } = new List<Node>();

    [Display(Name = "选中部位", GroupName = "显示设置")]
    public Part SelectedPart
    {
        get { return (Part)GetValue(SelectedPartProperty); }
        set { SetValue(SelectedPartProperty, value); }
    }


    public static readonly DependencyProperty SelectedPartProperty =
        DependencyProperty.Register("SelectedPart", typeof(Part), typeof(Diagram), new FrameworkPropertyMetadata(default(Part), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        {
            Diagram control = d as Diagram;
            if (control == null)
                return;
            Part config = e.NewValue as Part;
            control.OnSelectedPartChanged();
            if (config == null)
                return;
            if (config is Node node)
                control.SelectedNode = node;
            else
                control.InvokeMessage("选中 - " + config?.GetType().Name);

        }));

    [Display(Name = "选中节点", GroupName = "显示设置")]
    public Node SelectedNode
    {
        get { return (Node)GetValue(SelectedNodeProperty); }
        set { SetValue(SelectedNodeProperty, value); }
    }


    public static readonly DependencyProperty SelectedNodeProperty =
        DependencyProperty.Register("SelectedNode", typeof(Node), typeof(Diagram), new PropertyMetadata(default(Node), (d, e) =>
        {
            Diagram control = d as Diagram;

            if (control == null) return;

            Node config = e.NewValue as Node;
            if (config == null)
                return;
            control.InvokeMessage("选中节点 - " + config.GetContent<ITextable>()?.Text);
        }));


    [Browsable(false)]
    public DataTemplate LinkTemplate { get; set; }

    public bool UseAutoAddLinkOnEnd
    {
        get { return (bool)GetValue(UseAutoAddLinkOnEndProperty); }
        set { SetValue(UseAutoAddLinkOnEndProperty, value); }
    }

    public static readonly DependencyProperty UseAutoAddLinkOnEndProperty =
        DependencyProperty.Register("UseAutoAddLinkOnEnd", typeof(bool), typeof(Diagram), new FrameworkPropertyMetadata(true));
    #endregion

    #region - 事件 -

    public static readonly RoutedEvent AddLinkedRoutedEvent =
        EventManager.RegisterRoutedEvent("AddLinked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs<Link>>), typeof(Diagram));

    public event EventHandler<RoutedEventArgs<Link>> AddLinked
    {
        add { this.AddHandler(AddLinkedRoutedEvent, value); }
        remove { this.RemoveHandler(AddLinkedRoutedEvent, value); }
    }

    public void AddLink(Link link)
    {
        if (link == null)
            return;
        this.LinkLayer.Children.Add(link);
        this.Layout.DoLayoutLink(link);
        this.OnAddLinked(link);
        this.OnItemsChanged();
    }

    internal void OnAddLinked(Link link)
    {
        RoutedEventArgs<Link> args = new RoutedEventArgs<Link>(AddLinkedRoutedEvent, this, link);
        this.RaiseEvent(args);
    }

    public static readonly RoutedEvent ItemsChangedRoutedEvent =
        EventManager.RegisterRoutedEvent("ItemsChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Diagram));

    public event RoutedEventHandler ItemsChanged
    {
        add { this.AddHandler(ItemsChangedRoutedEvent, value); }
        remove { this.RemoveHandler(ItemsChangedRoutedEvent, value); }
    }

    internal void OnItemsChanged()
    {
        RoutedEventArgs args = new RoutedEventArgs(ItemsChangedRoutedEvent, this);
        this.RaiseEvent(args);

        this.UpdateNodesToDataSource();
    }

    public static readonly RoutedEvent SelectedPartChangedRoutedEvent =
        EventManager.RegisterRoutedEvent("SelectedPartChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Diagram));

    public event RoutedEventHandler SelectedPartChanged
    {
        add { this.AddHandler(SelectedPartChangedRoutedEvent, value); }
        remove { this.RemoveHandler(SelectedPartChangedRoutedEvent, value); }
    }

    protected void OnSelectedPartChanged()
    {
        RoutedEventArgs args = new RoutedEventArgs(SelectedPartChangedRoutedEvent, this);
        this.RaiseEvent(args);
    }

    public static readonly RoutedEvent AddNodedRoutedEvent =
        EventManager.RegisterRoutedEvent("AddNoded", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Diagram));
    public event RoutedEventHandler AddNoded
    {
        add { this.AddHandler(AddNodedRoutedEvent, value); }
        remove { this.RemoveHandler(AddNodedRoutedEvent, value); }
    }

    protected void OnAddNoded(IEnumerable<Node> nodes)
    {
        RoutedEventArgs<IEnumerable<Node>> args = new RoutedEventArgs<IEnumerable<Node>>(AddNodedRoutedEvent, this, nodes);
        this.RaiseEvent(args);
    }

    public void ZoomToFit(double scale = 1.2)
    {
        this.ZoomToFit(scale, this.Nodes.ToArray());
    }

    public void ZoomToFit(double scale = 1.2, params Part[] parts)
    {
        if (parts == null || parts.Length == 0)
            return;
        Rect rect;
        foreach (Rect item in parts.Select(x => x.Bound))
        {
            if (rect == default(Rect))
                rect = item;
            else
                rect.Union(item);
        }
        this.ZoomTo(rect, scale);
    }

    public void ZoomTo(Rect rect, double scale = 1.8)
    {
        var center = rect.GetCenter();
        Matrix matrix = new Matrix();
        matrix.ScaleAt(scale, scale, center.X, center.Y);
        rect.Transform(matrix);
        IZoombox zoombox = this.GetParent<DependencyObject>(x => x is IZoombox) as IZoombox;
        zoombox.ZoomTo(rect);
    }

    public void ZoomTo(Part part, double scale = 1.8)
    {
        //var r = part.Bound;
        //var center = r.GetCenter();
        //Matrix matrix = new Matrix();
        //matrix.ScaleAt(1.8, 1.8, center.X, center.Y);
        //r.Transform(matrix);
        this.ZoomTo(part.Bound, scale);
    }

    public void ZoomToSelectPart()
    {
        if (this.SelectedPart == null)
            return;
        IZoombox zoombox = this.GetParent<DependencyObject>(x => x is IZoombox) as IZoombox;
        zoombox.ZoomTo(this.SelectedPart.Bound);
    }

    public void ZoomTo(Point point)
    {
        IZoombox zoombox = this.GetParent<DependencyObject>(x => x is IZoombox) as IZoombox;
        zoombox.ZoomTo(point);
    }

    #endregion

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        IEnumerable<Part> parts = this.GetAllParts();
        IEnumerable<Part> selecteds = parts.Where(x => x.IsSelected);
        foreach (Part selected in selecteds)
            selected.IsSelected = false;

        this.SelectedNode = null;
        this.SelectedPart = null;
        this.Focus();
    }

    public IEnumerable<Part> GetAllParts(Func<Part, bool> predicate = null)
    {
        return this.GetChildren<Part>();
        //return this.Nodes.SelectMany(x => x.GetAllParts(predicate));
    }

    //  Do ：全部更新，性能慎用
    public void RefreshData()
    {

#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        if (this.LinkLayer == null || this.NodeLayer == null)
            return;
        this.Clear();

        if (this.DataSource == null || this.DataSource?.Nodes.Count == 0)
            return;

        IEnumerable<Node> nodes = this.DataSource?.Nodes.OfType<Node>();
        this.Nodes = nodes?.ToList();
        this.Links = this.Nodes.SelectMany(x => x.GetAllLinks()).Distinct().ToList();
        //this._layers.ForEach(l => l.UseAnimation = this.UseAnimation);
        this.RefreshLayout();
        this.RefreshLinkDrawer();
        foreach (Node node in this.Nodes)
        {
            if (node.Parent is Panel panel)
                panel.Children.Remove(node);
            this.NodeLayer.Children.Add(node);
        }

        foreach (Link link in this.Links)
        {
            if (link.Parent is Panel panel)
                panel.Children.Remove(link);
            this.LinkLayer.Children.Add(link);
        }
        this.Layout?.UpdateNode(this.Nodes.ToArray());

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("RefreshData：" + span.ToString());
#endif 
    }

    public void RefreshLayout()
    {
        this.RefreshLayout(this.Nodes);
    }

    private void RefreshLayout(IEnumerable<Node> nodes)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        foreach (Node item in nodes)
        {
            item.Style = this.NodeStyle;
        }
        foreach (Link item in nodes.SelectMany(x => x.LinksInto))
        {
            item.Style = this.LinkStyle;
        }
        this.Layout?.DoLayout(nodes?.ToArray());

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("RefreshLayout：" + span.ToString());
#endif 
    }

    public void RefreshLinkDrawer()
    {
        this.LinkLayer?.InvalidateArrange();
        foreach (Link item in this.Nodes.SelectMany(x => x.GetAllLinks()).Distinct())
        {
            item.Update();
        }
    }

    public void AddNode(params Node[] nodes)
    {
        List<Node> endNode = this.Nodes.Where(x => x.GetPorts(x => x.PortType == PortType.OutPut && x.GetLinksOutOf().Count() == 0).Count > 0).ToList();
        foreach (Node node in nodes)
        {
            //this.NodesSource.Add(node);
            this.Nodes.Add(node);
            this.NodeLayer.Children.Add(node);
            this.InvokeMessage("添加节点 - " + node.GetContent<ITextable>()?.Text);
        }

        this.Layout.AddNode(nodes);
        this.OnItemsChanged();
        this.OnAddNoded(nodes);

        if (this.UseAutoAddLinkOnEnd && endNode.Count == 1 && nodes.Length == 1)
        {
            Node firstFrom = endNode.First();
            Node firstTo = nodes.First();
            this.LinkNodes(firstFrom, firstTo);
            this.AligmentNodes();
        }
    }

    public void RemoveNode(params Node[] nodes)
    {
        foreach (Node node in nodes)
        {
            //this.NodesSource.Remove(node);
            this.Nodes.Remove(node);
            this.NodeLayer.Children.Remove(node);
            this.InvokeMessage("删除节点 - " + node.GetContent<ITextable>()?.Text);
        }
        this.Layout.RemoveNode(nodes);
        this.OnItemsChanged();
    }

    protected void Clear()
    {
        this.NodeLayer.Children.Clear();
        this.LinkLayer.Children.Clear();
        this.Nodes.Clear();
        this.Links.Clear();
    }
}

public partial class Diagram
{
    public virtual void InvokeMessage(string message)
    {
        this.Message = message;
    }

    [Browsable(false)]
    public string Message
    {
        get { return (string)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }


    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register("Message", typeof(string), typeof(Diagram), new FrameworkPropertyMetadata(default(string)));

    [Display(Name = "启用切换布局动画", GroupName = "显示设置")]
    public bool UseAnimation { get; set; } = true;

    [Browsable(false)]
    public ILinkDrawer LinkDrawer
    {
        get { return (ILinkDrawer)GetValue(LinkDrawerProperty); }
        set { SetValue(LinkDrawerProperty, value); }
    }


    public static readonly DependencyProperty LinkDrawerProperty =
        DependencyProperty.Register("LinkDrawer", typeof(ILinkDrawer), typeof(Diagram), new PropertyMetadata(default(ILinkDrawer), (d, e) =>
         {
             Diagram control = d as Diagram;
             if (control == null)
                 return;
             ILinkDrawer config = e.NewValue as ILinkDrawer;
             if (config == null)
                 return;
             config.Diagram = control;
             control.RefreshLinkDrawer();
         }));

    public IDiagramDataSource DataSource
    {
        get { return (IDiagramDataSource)GetValue(DataSourceProperty); }
        set { SetValue(DataSourceProperty, value); }
    }

    public static readonly DependencyProperty DataSourceProperty =
        DependencyProperty.Register("DataSource", typeof(IDiagramDataSource), typeof(Diagram), new FrameworkPropertyMetadata(default(IDiagramDataSource), (d, e) =>
        {
            Diagram control = d as Diagram;

            if (control == null) return;

            if (e.OldValue is IDiagramDataSource o)
            {

            }

            if (e.NewValue is IDiagramDataSource n)
            {

            }
            control.RefreshData();
        }));

    protected void UpdateNodesToDataSource()
    {
        if (this.DataSource == null)
            return;
        this.DataSource.Nodes = this.Nodes.ToList();
    }
}
