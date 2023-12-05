// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.Diagram
{
    [TemplatePart(Name = "NodeLayer", Type = typeof(NodeLayer))]
    [TemplatePart(Name = "LinkLayer", Type = typeof(LinkLayer))]
    [TemplatePart(Name = "DynamicLayer", Type = typeof(LinkLayer))]
    public partial class Diagram : ContentControl
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
                CommandBinding binding = new CommandBinding(DiagramCommands.Start);
                binding.Executed += async (l, k) =>
                {
                    Node start = this.GetStartNode(out string message);
                    if (start == null)
                    {
                        IocMessage.ShowDialogMessage(message);
                        return;
                    }

                    this.State = DiagramFlowableState.Running;
                    bool? b = await Start(start);
                    this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
                    IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
                    Commands.InvalidateRequerySuggested();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = State != DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(DiagramCommands.Stop);
                binding.Executed += (l, k) =>
                {
                    IEnumerable<Part> parts = this.Nodes.SelectMany(x => x.GetParts());
                    foreach (Part part in parts)
                    {
                        IFlowable data = part.GetContent<IFlowable>();
                        {
                            if (data != null)
                            {
                                if (data.State == FlowableState.Running || data.State == FlowableState.Wait)
                                    data.State = FlowableState.Canceling;
                            }
                        }
                        if (part.State == FlowableState.Running || part.State == FlowableState.Wait)
                        {
                            part.State = FlowableState.Canceling;
                        }
                    };
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = State == DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(DiagramCommands.Reset);
                binding.Executed += (l, k) =>
                {
                    IEnumerable<Part> parts = this.Nodes.SelectMany(x => x.GetParts());
                    foreach (Part part in parts)
                    {
                        part.State = FlowableState.Ready;
                        if (part.Content is IFlowable flowable)
                            flowable.State = FlowableState.Ready;
                    }
                    this.State = DiagramFlowableState.None;
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = State != DiagramFlowableState.None && this.State != DiagramFlowableState.Canceling;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(DiagramCommands.Clear);
                binding.Executed += (l, k) =>
                {
                    this.Clear();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = State != DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling;
                };
                this.CommandBindings.Add(binding);
            }

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
                };
                this.CommandBindings.Add(binding);

                MouseBinding keyBinding = new MouseBinding(DiagramCommands.ZoomToFit, new MouseGesture(MouseAction.LeftDoubleClick));
                this.InputBindings.Add(keyBinding);
            }

            {
                CommandBinding binding = new CommandBinding(DiagramCommands.Aligment);
                binding.Executed += (l, k) =>
                {
                    Action<IEnumerable<Node>> action = null;
                    action = nodes =>
                    {
                        foreach (Node node in nodes)
                        {
                            node.Aligment();
                            List<Node> fromNodes = node.GetFromNodes();
                            action.Invoke(fromNodes);
                        }
                    };

                    IEnumerable<Node> nodes = this.Nodes.Where(x => x.LinksOutOf.Count == 0);
                    action.Invoke(nodes);
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
                        if (index == this.Nodes.Count - 1)
                        {
                            selected = this.Nodes[0];
                        }
                        else
                        {
                            selected = this.Nodes[index + 1];
                        }
                    }
                    selected.IsSelected = true;

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
                        if (index == 0)
                        {
                            selected = this.Nodes.LastOrDefault();
                        }
                        else
                        {
                            selected = this.Nodes[index - 1];
                        }
                    }
                    selected.IsSelected = true;

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
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = this.Nodes.Count > 0;
                };
                this.CommandBindings.Add(binding);

                KeyBinding keyBinding = new KeyBinding(DiagramCommands.MoveDown, new KeyGesture(Key.Down));
                this.InputBindings.Add(keyBinding);
            }

            {

                ContextMenu contextMenu = new ContextMenu();
                foreach (CommandBinding item in this.CommandBindings)
                {
                    if (item.Command is RoutedUICommand routed)
                        contextMenu.Items.Add(new MenuItem() { Command = item.Command, Header = routed.Text });
                }
                this.ContextMenu = contextMenu;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LinkLayer = Template.FindName("LinkLayer", this) as LinkLayer;
            this.NodeLayer = Template.FindName("NodeLayer", this) as NodeLayer;
            this.DynamicLayer = Template.FindName("DynamicLayer", this) as LinkLayer;
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

        [Display(Name = "启用开始点接口检查", GroupName = "流程控制")]
        public bool UseStartNodeOnly
        {
            get { return (bool)GetValue(UseStartNodeOnlyProperty); }
            set { SetValue(UseStartNodeOnlyProperty, value); }
        }

        
        public static readonly DependencyProperty UseStartNodeOnlyProperty =
            DependencyProperty.Register("UseStartNodeOnly", typeof(bool), typeof(Diagram), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        [Display(Name = "执行方式", GroupName = "流程控制")]
        public DiagramFlowableMode FlowableMode
        {
            get { return (DiagramFlowableMode)GetValue(FlowableModeProperty); }
            set { SetValue(FlowableModeProperty, value); }
        }

        
        public static readonly DependencyProperty FlowableModeProperty =
            DependencyProperty.Register("FlowableMode", typeof(DiagramFlowableMode), typeof(Diagram), new FrameworkPropertyMetadata(DiagramFlowableMode.Link, (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                if (e.OldValue is DiagramFlowableMode o)
                {

                }

                if (e.NewValue is DiagramFlowableMode n)
                {

                }

            }));

        [Display(Name = "执行状态", GroupName = "流程控制")]
        public DiagramFlowableState State
        {
            get { return (DiagramFlowableState)GetValue(StateProperty); }
            private set { SetValue(StateProperty, value); }
        }

        
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(DiagramFlowableState), typeof(Diagram), new FrameworkPropertyMetadata(default(DiagramFlowableState), (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                if (e.OldValue is DiagramFlowableState o)
                {

                }

                if (e.NewValue is DiagramFlowableState n)
                {

                }
            }));

        [Browsable(false)]
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(Diagram), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

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

                if (control == null) return;

                ILayout config = e.NewValue as ILayout;

                if (config == null) return;

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
                if (config is Node node)
                    control.SelectedNode = node;

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

            }));


        [Browsable(false)]
        public DataTemplate LinkTemplate { get; set; }

        [Browsable(false)]
        public IList NodesSource
        {
            get { return (IList)GetValue(NodesSourceProperty); }
            set { SetValue(NodesSourceProperty, value); }
        }

        
        public static readonly DependencyProperty NodesSourceProperty =
            DependencyProperty.Register("NodesSource", typeof(IList), typeof(Diagram), new PropertyMetadata(new ObservableCollection<Node>(), (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                IList config = e.NewValue as IList;

                //if (e.OldValue is INotifyCollectionChanged old)
                //{
                //    old.CollectionChanged -= control.Notify_CollectionChanged;
                //}

                //if (config is INotifyCollectionChanged notify)
                //{
                //    notify.CollectionChanged -= control.Notify_CollectionChanged;
                //    notify.CollectionChanged += control.Notify_CollectionChanged;
                //}

                control.RefreshData();
            }));

        [Display(Name = "执行时节点自动选中", GroupName = "流程控制")]
        public bool UseFlowableSelectToRunning
        {
            get { return (bool)GetValue(UseFlowableSelectToRunningProperty); }
            set { SetValue(UseFlowableSelectToRunningProperty, value); }
        }

        
        public static readonly DependencyProperty UseFlowableSelectToRunningProperty =
            DependencyProperty.Register("UseFlowableSelectToRunning", typeof(bool), typeof(Diagram), new FrameworkPropertyMetadata(false, (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        [Display(Name = "执行时节点自动缩放", GroupName = "流程控制")]
        public DiagramFlowableZoomMode FlowableZoomMode
        {
            get { return (DiagramFlowableZoomMode)GetValue(FlowableZoomModeProperty); }
            set { SetValue(FlowableZoomModeProperty, value); }
        }

        
        public static readonly DependencyProperty FlowableZoomModeProperty =
            DependencyProperty.Register("FlowableZoomMode", typeof(DiagramFlowableZoomMode), typeof(Diagram), new FrameworkPropertyMetadata(default(DiagramFlowableZoomMode), (d, e) =>
            {
                Diagram control = d as Diagram;

                if (control == null) return;

                if (e.OldValue is DiagramFlowableZoomMode o)
                {

                }

                if (e.NewValue is DiagramFlowableZoomMode n)
                {

                }

            }));

        #endregion

        #region - 事件 -

        public static readonly RoutedEvent AddLinkedRoutedEvent =
            EventManager.RegisterRoutedEvent("AddLinked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs<Link>>), typeof(Diagram));

        public event EventHandler<RoutedEventArgs<Link>> AddLinked
        {
            add { this.AddHandler(AddLinkedRoutedEvent, value); }
            remove { this.RemoveHandler(AddLinkedRoutedEvent, value); }
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


        //声明和注册路由事件
        public static readonly RoutedEvent RunningPartChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("RunningPartChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs<Part>>), typeof(Diagram));
        //CLR事件包装
        public event RoutedEventHandler RunningPartChanged
        {
            add { this.AddHandler(RunningPartChangedRoutedEvent, value); }
            remove { this.RemoveHandler(RunningPartChangedRoutedEvent, value); }
        }


        internal void OnRunningPartChanged(Part part)
        {
            RoutedEventArgs<Part> args = new RoutedEventArgs<Part>(RunningPartChangedRoutedEvent, this, part);
            this.RaiseEvent(args);

            if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
                this.ZoomTo(part.Bound);
            else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
            {
                Point point = part.Bound.GetCenter();
                //zoombox.ZoomToCenter(part.Bound.BottomRight);
            }
            if (this.UseFlowableSelectToRunning)
                part.IsSelected = true;
        }

        public void ZoomToFit()
        {
            if (this.Nodes.Count == 0)
                return;
            Rect rect;
            foreach (Rect item in this.Nodes.Select(x => x.Bound))
            {
                if (rect == default(Rect))
                    rect = item;
                else
                    rect.Union(item);
            }
            this.ZoomTo(rect);
        }

        public void ZoomTo(Rect rect)
        {
            IZoombox zoombox = this.GetParent<DependencyObject>(x => x is IZoombox) as IZoombox;
            zoombox.ZoomTo(rect);
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

        public virtual async Task<bool?> Start(Node startNode)
        {
            Action<Part> builder = x =>
            {

            };
            if (this.FlowableMode == DiagramFlowableMode.Link)
                return await startNode.StartLink(builder);
            if (this.FlowableMode == DiagramFlowableMode.Port)
                return await startNode.StartPort(builder);
            return await startNode.StartNode(builder);
        }

        protected virtual Node GetStartNode(out string message)
        {
            message = null;
            IEnumerable<Part> parts = this.Nodes.SelectMany(x => x.GetParts());
            foreach (Part part in parts)
            {
                part.State = FlowableState.Wait;
                if (part.Content is IFlowable flowable)
                    flowable.State = FlowableState.Wait;
            }
            IEnumerable<Node> starts = this.Nodes.Where(x => x.GetFromNodes().Count == 0 && (x.GetContent<IFlowableNode>()?.UseStart == true || !this.UseStartNodeOnly));
            if (starts == null || starts.Count() == 0)
            {
                message = "未找到起始节点,请添加UseStart节点";
                return null;
            }

            if (starts.Count() > 1)
            {
                message = "查询到多个节点";
                return null;
            }

            return starts.FirstOrDefault();
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

            if (this.NodesSource == null || this.NodesSource.Count == 0)
                return;

            IEnumerable<Node> nodes = this.NodesSource?.OfType<Node>();
            this.Nodes = nodes?.ToList();
            //this.Nodes = nodes.ToList();
            this.Links = this.Nodes.SelectMany(x => x.GetAllLinks()).Distinct().ToList();
            this._layers.ForEach(l => l.UseAnimation = this.UseAnimation);
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
            foreach (Node node in nodes)
            {
                this.NodesSource.Add(node);
                this.Nodes.Add(node);
                this.NodeLayer.Children.Add(node);
            }

            this.Layout.AddNode(nodes);
            this.OnItemsChanged();
        }

        public void RemoveNode(params Node[] nodes)
        {
            foreach (Node node in nodes)
            {
                this.NodesSource.Remove(node);
                this.Nodes.Remove(node);
                this.NodeLayer.Children.Remove(node);
            }

            this.Layout.RemoveNode(nodes);
            this.OnItemsChanged();
        }

        private void Clear()
        {
            this.NodeLayer.Children.Clear();
            this.LinkLayer.Children.Clear();
            this.Nodes.Clear();
            this.Links.Clear();
        }
    }

    public partial class Diagram
    {
        [Display(Name = "切换布局动画间隔", GroupName = "显示设置")]
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(Diagram), new PropertyMetadata(TimeSpan.FromMilliseconds(500), (d, e) =>
             {
                 Diagram control = d as Diagram;

                 if (control == null) return;

                 //TimeSpan config = e.NewValue as TimeSpan;

             }));


        //public bool UseAnimation
        //{
        //    get { return (bool)GetValue(UseAnimationProperty); }
        //    set { SetValue(UseAnimationProperty, value); }
        //}

        //
        //public static readonly DependencyProperty UseAnimationProperty =
        //    DependencyProperty.Register("UseAnimation", typeof(bool), typeof(Diagram), new PropertyMetadata(false, (d, e) =>
        //     {
        //         Diagram control = d as Diagram;

        //         if (control == null) return;

        //         //bool config = e.NewValue as bool;

        //     }));

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
    }
}
