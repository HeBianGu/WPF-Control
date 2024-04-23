
using H.Extensions.ValueConverter;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Extension
{
    public abstract class DiagramBase : DisplayBindableBase, IDiagram
    {
        public DiagramBase()
        {
            //var vip = this.GetType().GetCustomAttribute<VipAttribute>(true);
            //this.Vip = vip == null ? 3 : vip.Level;
            this.TypeName = this.Name;
            this.DiagramThemeGroups = this.CreateDiagramThemes()?.ToObservable();

            //this.Init();
            this.LinkDrawers = this.CreateLinkDrawerSource()?.ToObservable();
            this.LinkDrawer = this.LinkDrawers?.FirstOrDefault();

            ObservableCollection<NodeGroup> nodeGroups = this.CreateNodeGroups()?.ToObservable();
            foreach (NodeGroup nodeGroup in nodeGroups)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                          {
                              this.NodeGroups.Add(nodeGroup);
                          }));
            }

            this.Layouts = this.CreateLayousSource()?.ToObservable();
            this.Layout = this.Layouts?.FirstOrDefault();

            //this.DynamicStyle.Stroke = Application.Current.FindResource(BrushKeys.Red) as Brush;
            //this.DynamicCanDropStyle.Stroke = Application.Current.FindResource(BrushKeys.Green) as Brush;
        }

        #region - 接口 -

        public virtual IEnumerable<DiagramThemeGroup> CreateDiagramThemes()
        {
            List<Color> colors = new List<Color>() { Colors.Red, Colors.Green, Colors.DarkBlue, Colors.Purple, Colors.Gray, Colors.Black, Colors.Orange, Colors.Brown, Colors.DeepPink, Colors.DarkSlateGray };

            SolidColorBrush white = new SolidColorBrush(Colors.White);
            DiagramTheme themeDefault = new DiagramTheme();

            {
                DiagramThemeGroup group = new DiagramThemeGroup();
                group.Add(themeDefault);
                foreach (Color color in colors)
                {
                    SolidColorBrush brush = new SolidColorBrush(color);
                    DiagramTheme theme = new DiagramTheme();
                    theme.Note.Fill = brush;
                    theme.Note.Stroke = brush;
                    theme.Note.Foreground = white;
                    theme.Link.Stroke = brush;
                    theme.Port.Stroke = brush;
                    group.Add(theme);
                }
                yield return group;
            }

            {
                DiagramThemeGroup group = new DiagramThemeGroup();
                group.Add(themeDefault);
                foreach (Color color in colors)
                {
                    SolidColorBrush brush = new SolidColorBrush(color);
                    DiagramTheme theme = new DiagramTheme();
                    theme.Note.Stroke = brush;
                    theme.Note.Foreground = brush;
                    theme.Link.Stroke = brush;
                    theme.Port.Stroke = brush;
                    group.Add(theme);
                }
                yield return group;
            }

            {
                DiagramThemeGroup group = new DiagramThemeGroup();
                group.Add(themeDefault);
                foreach (Color color in colors)
                {
                    SolidColorBrush brush = new SolidColorBrush(color);
                    DiagramTheme theme = new DiagramTheme();
                    theme.Note.Fill = new SolidColorBrush(color) { Opacity = 0.1 };
                    theme.Note.Stroke = brush;
                    theme.Note.Foreground = brush;
                    theme.Link.Stroke = brush;
                    theme.Port.Stroke = brush;
                    group.Add(theme);
                }
                yield return group;
            }

            {
                DiagramThemeGroup group = new DiagramThemeGroup();
                group.Add(themeDefault);
                foreach (Color color in colors)
                {
                    SolidColorBrush brush = new SolidColorBrush(color);
                    DiagramTheme theme = new DiagramTheme();
                    theme.Note.Fill = new SolidColorBrush(color) { Opacity = 0.1 };
                    group.Add(theme);
                }
                yield return group;
            }

            {
                DiagramThemeGroup group = new DiagramThemeGroup();
                group.Add(themeDefault);
                foreach (Color color in colors)
                {
                    SolidColorBrush brush = new SolidColorBrush(color);
                    DiagramTheme theme = new DiagramTheme();
                    theme.Note.Fill = Brushes.Transparent;
                    theme.Note.Stroke = brush;
                    theme.Note.Foreground = brush;
                    theme.Link.Stroke = brush;
                    theme.Port.Stroke = brush;
                    group.Add(theme);
                }
                yield return group;
            }
        }

        protected virtual IEnumerable<ILinkDrawer> CreateLinkDrawerSource()
        {
            yield return new BrokenLinkDrawer();
            yield return new LineLinkDrawer();
            yield return new BezierLinkDrawer();
            yield return new ArcLinkDrawer();
        }

        protected virtual IEnumerable<ILayout> CreateLayousSource()
        {
            yield return new LocationLayout();
        }

        protected virtual IEnumerable<NodeGroup> CreateNodeGroups()
        {
            IEnumerable<Type> types = this.GetType().Assembly.GetTypes().Where(x => typeof(INodeData).IsAssignableFrom(x)).Where(x => !x.IsAbstract);
            types = types.Where(x => x.GetCustomAttributes<NodeTypeAttribute>(true).Any(t => t.DiagramType == this.GetType()));
            List<NodeData> datas = new List<NodeData>();
            foreach (Type item in types)
            {
                NodeData data = Activator.CreateInstance(item) as NodeData;
                DisplayAttribute type = item.GetCustomAttribute<DisplayAttribute>();
                data.Name = type?.Name;
                data.GroupName = type?.GroupName;
                data.Description = type?.Description;
                //data.Order = type?.Order ?? 0;
                int? od = type.GetOrder();
                if (od.HasValue)
                    Order = od.Value;
                datas.Add(data);
            }
            IEnumerable<IGrouping<string, NodeData>> groups = datas.OrderBy(x => x.Order).GroupBy(x => x.GroupName);
            return groups.Select(x => new NodeGroup(x.ToList()) { Name = x.Key, Columns = x.ToList()?.FirstOrDefault()?.Columns ?? 4 }); ;
        }

        #endregion

        #region - 属性 -

        private ObservableCollection<DiagramThemeGroup> _diagramThemeGroups = new ObservableCollection<DiagramThemeGroup>();
        [XmlIgnore]
        public ObservableCollection<DiagramThemeGroup> DiagramThemeGroups
        {
            get { return _diagramThemeGroups; }
            set
            {
                _diagramThemeGroups = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<FlowableDiagramTemplateNodeData> _referenceTemplateNodeDatas = new ObservableCollection<FlowableDiagramTemplateNodeData>();
        [XmlIgnore]
        public ObservableCollection<FlowableDiagramTemplateNodeData> ReferenceTemplateNodeDatas
        {
            get { return _referenceTemplateNodeDatas; }
            set
            {
                _referenceTemplateNodeDatas = value;
                RaisePropertyChanged();
            }
        }

        private TreeNodeBase<Part> _root = new TreeNodeBase<Part>(null);
        [XmlIgnore]
        public TreeNodeBase<Part> Root
        {
            get { return _root; }
            set
            {
                _root = value;
                RaisePropertyChanged();
            }
        }

        private string _typeName;
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
                RaisePropertyChanged();
            }
        }

        private int _vip;
        public int Vip
        {
            get { return _vip; }
            set
            {
                _vip = value;
                RaisePropertyChanged();
            }
        }

        private double _width;
        [DefaultValue(2000.0)]
        [Display(Name = "面板宽度", GroupName = "基础信息", Order = 0)]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }

        private double _height;
        [DefaultValue(1300.0)]
        [Display(Name = "面板高度", GroupName = "基础信息", Order = 0)]
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged();
            }
        }

        private int _backgroundSelectedIndex = 0;
        public int BackgroundSelectedIndex
        {
            get { return _backgroundSelectedIndex; }
            set
            {
                _backgroundSelectedIndex = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<NodeGroup> _nodeGroups = new ObservableCollection<NodeGroup>();
        /// <summary> 数据源  </summary>
        [XmlIgnore]
        public ObservableCollection<NodeGroup> NodeGroups
        {
            get { return _nodeGroups; }
            set
            {
                _nodeGroups = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
        /// <summary> 工具拖动数据源  </summary>
        [XmlIgnore]
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                RaisePropertyChanged("Nodes");
            }
        }


        private Part _selectedPart;
        [XmlIgnore]
        public Part SelectedPart
        {
            get { return _selectedPart; }
            set
            {
                _selectedPart = value;
                RaisePropertyChanged();
            }
        }


        private ILinkDrawer _linkDrawer = new BrokenLinkDrawer();
        /// <summary> 说明  </summary>
        [XmlIgnore]
        public ILinkDrawer LinkDrawer
        {
            get { return _linkDrawer; }
            set
            {
                _linkDrawer = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ILinkDrawer> _lLinkDrawers = new ObservableCollection<ILinkDrawer>();
        /// <summary> 说明  </summary>
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ILinkDrawer> LinkDrawers
        {
            get { return _lLinkDrawers; }
            set
            {
                _lLinkDrawers = value;
                RaisePropertyChanged("LinkDrawers");
            }
        }

        private ObservableCollection<ILayout> _layouts = new ObservableCollection<ILayout>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ILayout> Layouts
        {
            get { return _layouts; }
            set
            {
                _layouts = value;
                RaisePropertyChanged("Layouts");
            }
        }


        private ILayout _layout = new LocationLayout();
        /// <summary> 说明  </summary>
        [XmlIgnore]
        public ILayout Layout
        {
            get { return _layout; }
            set
            {
                _layout = value;
                RaisePropertyChanged();
            }
        }

        private Type _nodeType = typeof(Node);
        [XmlIgnore]
        public Type NodeType
        {
            get { return _nodeType; }
            set
            {
                _nodeType = value;
                RaisePropertyChanged();
            }
        }

        private Point _location = new Point(1000, 650);
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region - 序列化 -

        public string ToXmlString()
        {
            //XmlDocument xmlDoc = XmlableSerializor.Instance.SaveAs(this);
            //return xmlDoc.OuterXml;
            return null;
        }

        public static T CreateFromXml<T>(string xml) where T : class, IDiagram
        {
            T instance = CreateFromXml(xml, typeof(T)) as T;
            return instance;
        }

        public static IDiagram CreateFromXml(string xml, Type type)
        {
            IDiagram instance = Application.Current.Dispatcher.Invoke(() =>
            {
                return Activator.CreateInstance(type) as IDiagram;
            });

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            //XmlableSerializor.Instance.Load(xmlDoc, instance);
            return instance;
        }

        public object Clone()
        {
            string xml = this.ToXmlString();
            return CreateFromXml(xml, this.GetType());
            //return Application.Current.Dispatcher.Invoke(() =>
            //  {
            //      //this._layout.DoLayout(this.Nodes.ToArray());
            //      return CreateFromXml(xml, this.GetType());
            //  });
        }

        protected virtual IEnumerable<Node> LoadNodes(IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
        {
            DiagramDataSourceConverter converter = new DiagramDataSourceConverter(nodeDatas, linkDatas);
            return converter.NodeSource;
        }

        protected virtual Tuple<IEnumerable<INodeData>, IEnumerable<ILinkData>> SaveDatas(IEnumerable<Node> nodes)
        {
            DiagramDataSourceConverter converter = new DiagramDataSourceConverter(nodes.ToList());
            IEnumerable<ILinkData> linkDatas = converter.GetLinkType();
            IEnumerable<INodeData> nodeDatas = converter.GetNodeType();
            return Tuple.Create(nodeDatas, linkDatas);
        }

        public void FromXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match = null)
        {
            //XmlDiagramData data = new XmlDiagramData();
            //XmlableSerializor.Instance.FromXml(xmlEle, this, cnt);

            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    XmlableSerializor.Instance.FromXml(xmlEle, data, cnt);
            //    var nodeDatas = data.Nodes.ToList();
            //    var linkDatas = data.Links.ToList();
            //    this.Nodes = this.LoadNodes(nodeDatas, linkDatas).ToObservable();
            //});

            //foreach (var file in data.ReferenceTemplates)
            //{
            //    if (!File.Exists(file.Value))
            //    {
            //        this.ReferenceTemplateNodeDatas.Add(new FlowableDiagramTemplateNodeData() { FilePath = file.Value });
            //        continue;
            //    }
            //    DiagramTemplate template = XmlableSerializor.Instance.Load<DiagramTemplate>(file.Value);
            //    var nodeData = new FlowableDiagramTemplateNodeData(template);
            //    this.ReferenceTemplateNodeDatas.Add(nodeData);
            //}
        }

        public void ToXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match = null)
        {
            //XmlableSerializor.Instance.ToXml(xmlEle, this, this.GetType().Name, cnt, null, false);
            //XmlDiagramData data = new XmlDiagramData();
            //var ns = Application.Current.Dispatcher.Invoke(() =>
            //{
            //    return this.Nodes.ToList();
            //});

            //DiagramDataSourceConverter converter = new DiagramDataSourceConverter(ns);
            //var datas = this.SaveDatas(ns);
            //var links = datas.Item2;
            //var nodes = datas.Item1;

            //foreach (var node in nodes)
            //{
            //    data.Nodes.Add(node);
            //}
            //foreach (var link in links)
            //{
            //    data.Links.Add(link);
            //}

            //data.ReferenceTemplates = this.ReferenceTemplateNodeDatas.Select(x => new XmlStringData(x.FilePath)).ToList();
            //XmlableSerializor.Instance.ToXml(xmlEle, data, this.GetType().Name, cnt, null, false);
        }

        #endregion

        #region - 命令 -
        public RelayCommand DeleteReferenceTemplateCommand => new RelayCommand((s, e) =>
        {
            if (e is FlowableDiagramTemplateNodeData template)
            {
                this.DeleteReferenceTemplate(template);
            }
        });

        public async void DeleteReferenceTemplate(FlowableDiagramTemplateNodeData data)
        {
            IEnumerable<Node> finds = this.Nodes.Where(x =>
            {
                if (x.Content is FlowableDiagramTemplateNodeData f)
                {
                    return f.FilePath == data.FilePath;
                }
                return false;
            });
            if (finds != null && finds.Count() > 0)
            {
                bool? r = await IocMessage.Dialog.Show("当前已添加对应节点，删除将删除节点，是否确定删除？");
                if (r != true) return;

                foreach (Node item in finds.ToList())
                {
                    item.Delete();
                }
            }

            this.ReferenceTemplateNodeDatas.Remove(data);
        }

        [Display(Name = "默认样式", GroupName = "操作", Order = 6, Description = "点击此功能，恢复所有节点、连线和端口默认样式")]
        //[Command(Icon = "\xe8dc")]
        public new RelayCommand LoadDefaultCommand => new RelayCommand((s, e) =>
        {
            foreach (Node node in this.Nodes)
            {
                IEnumerable<IDefaultable> displayers = node.GetParts().Select(x => x.Content).OfType<IDefaultable>();

                foreach (IDefaultable item in displayers)
                {
                    item.LoadDefault();
                }

                if (node.Content is IDefaultable displayer)
                {
                    displayer.LoadDefault();
                }
            }
        }, (s, e) => this.Nodes.Count > 0);

        [Display(Name = "删除", GroupName = "操作", Order = 4, Description = "点击此功能，删除选中的节点、连线或端口")]
        public virtual RelayCommand DeleteCommand => new RelayCommand((s, e) =>
        {
            this.SelectedPart.Delete();
        }, (s, e) => this.SelectedPart != null);

        [Display(Name = "清空", GroupName = "操作", Order = 5, Description = "点击此功能，删除所有节点、连线和端口")]
        public virtual RelayCommand ClearCommand => new RelayCommand((s, e) =>
        {
            this.Clear();
        }, (s, e) => this.Nodes.Count > 0);

        [XmlIgnore]
        [Display(Name = "自动对齐", GroupName = "操作", Order = 5)]
        public virtual RelayCommand AlignmentCommand => new RelayCommand((s, e) =>
        {
            this.Aligment();
        }, (s, e) => this.Nodes.Count > 0);


        [XmlIgnore]
        [Display(Name = "上一个", GroupName = "操作", Order = 5)]
        public virtual RelayCommand ProviewCommand => new RelayCommand((s, e) =>
        {
            this.SelectedPart.GetPrevious().IsSelected = true;
            this.LocateCenter(this.SelectedPart);
        }, (s, e) => this.SelectedPart?.GetPrevious() != null);

        [XmlIgnore]
        [Display(Name = "下一个", GroupName = "操作", Order = 5)]
        public virtual RelayCommand NextCommand => new RelayCommand((s, e) =>
        {
            this.SelectedPart.GetNext().IsSelected = true;
            this.LocateCenter(this.SelectedPart);
        }, (s, e) => this.SelectedPart?.GetNext() != null);


        public virtual void Aligment()
        {
            foreach (Node item in this.Nodes)
            {
                item.Aligment();
            }
        }

        public RelayCommand SelectedTreeNodeChanged => new RelayCommand((s, e) =>
        {
            if (e is TreeNodeBase<Part> project)
            {
                project.Model.IsSelected = true;
            }
        });

        public RelayCommand ApplyDiagramThemeCommand => new RelayCommand((s, e) =>
        {
            if (e is DiagramTheme project)
            {
                foreach (Node node in this.Nodes)
                {
                    if (node.Content is INodeData nodeData)
                    {
                        project.Note.ApplayStyleTo(nodeData);
                    }

                    foreach (Link link in node.GetAllLinks().Distinct())
                    {
                        if (link.Content is ILinkData linkData)
                        {
                            project.Link.ApplayStyleTo(linkData);
                        }
                    }

                    foreach (Port port in node.GetPorts().Distinct())
                    {
                        if (port.Content is IPortData portData)
                        {
                            project.Port.ApplayStyleTo(portData);
                        }
                    }
                }
            }
        });

        public RelayCommand ApplayNodeStyleCommand => new RelayCommand((s, e) =>
        {
            if (e is TextNodeData project)
            {
                if (this.SelectedPart?.Content is INodeData nodeData)
                {
                    project.ApplayStyleTo(nodeData);
                }
            }
        });

        public RelayCommand ApplayLinkStyleCommand => new RelayCommand((s, e) =>
        {
            if (e is TextLinkData project)
            {
                if (this.SelectedPart?.Content is ILinkData data)
                {
                    project.ApplayStyleTo(data);
                }
            }
        });

        public RelayCommand ApplayPortStyleCommand => new RelayCommand((s, e) =>
        {
            if (e is TextPortData project)
            {
                if (this.SelectedPart?.Content is IPortData data)
                {
                    project.ApplayStyleTo(data);
                }
            }
        });

        public RelayCommand ItemsChangedCommand => new RelayCommand((s, e) =>
        {
            this.RefreshRoot();
        });

        [Display(Name = "缩放定位", GroupName = "操作", Order = 5)]
        public virtual RelayCommand ZoomAllCommand => new RelayCommand((s, e) =>
        {
            this.ZoomAll();
        }, (s, e) => this.Nodes.Count > 0);

        [Display(Name = "平移定位", GroupName = "操作", Order = 5)]
        public virtual RelayCommand LocateCenterCommand => new RelayCommand((s, e) =>
        {
            this.LocateCenter();
        }, (s, e) => this.Nodes.Count > 0);

        private Action<Point> _locateCenterCallBack;
        [XmlIgnore]
        public Action<Point> LocateCenterCallBack
        {
            get { return _locateCenterCallBack; }
            set
            {
                if (value == null)
                    return;
                _locateCenterCallBack = value;
                RaisePropertyChanged();
            }
        }

        private Action<Rect> _locateRectCallBack;
        [XmlIgnore]
        public Action<Rect> LocateRectCallBack
        {
            get { return _locateRectCallBack; }
            set
            {
                if (value == null)
                    return;
                _locateRectCallBack = value;
                RaisePropertyChanged();
            }
        }

        private static bool _isSelectedPartRefreshing;
        public RelayCommand SelectedPartChangedCommand => new RelayCommand((s, e) =>
        {
            if (this.SelectedPart == null)
                return;
            //if (!this.SelectedPart.IsSelected)
            //    return; 

            if (_isSelectedPartRefreshing)
                return;
            _isSelectedPartRefreshing = true;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                _isSelectedPartRefreshing = false;
                TreeNodeBase<Part> find = this.Root.FindAll(x => x.Model == this.SelectedPart)?.FirstOrDefault();
                if (find == null)
                    return;
                find.IsSelected = true;
                if (find.Parent != null)
                    find.Parent.IsExpanded = true;
            }));
        });


        #endregion

        #region - 方法 -

        public void ZoomAll()
        {
            if (this.Nodes.Count == 0)
                return;
            double xmax = this.Nodes.Max(x => x.Location.X + (x.ActualWidth / 2));
            double xmin = this.Nodes.Min(x => x.Location.X - (x.ActualWidth / 2));
            double ymax = this.Nodes.Max(x => x.Location.Y + (x.ActualHeight / 2));
            double ymin = this.Nodes.Min(x => x.Location.Y - (x.ActualHeight / 2));
            Rect rect = new Rect(new Point(xmin, ymin), new Point(xmax, ymax));
            this.LocateRectCallBack.Invoke(rect);
        }

        public void LocateCenter()
        {
            double xmax = this.Nodes.Max(x => x.Location.X + (x.ActualWidth / 2));
            double xmin = this.Nodes.Min(x => x.Location.X - (x.ActualWidth / 2));
            double ymax = this.Nodes.Max(x => x.Location.Y + (x.ActualHeight / 2));
            double ymin = this.Nodes.Min(x => x.Location.Y - (x.ActualHeight / 2));
            Point center = new Point((xmin + xmax) / 2, (ymin + ymax) / 2);
            this.LocateCenterCallBack.Invoke(center);
        }

        public void Clear()
        {
            foreach (Node item in this.Nodes.ToList())
            {
                item.Delete();
            }
            this.SelectedPart = null;
        }

        private static bool _isRefreshRooting;
        public virtual void RefreshRoot()
        {
            if (_isRefreshRooting)
                return;
            _isRefreshRooting = true;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                _isRefreshRooting = false;
                this.Root.Nodes.Clear();
                foreach (Node note in this.Nodes)
                {
                    NodeTreeNode nd = new NodeTreeNode(note);

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                    {
                        this.Root.AddNode(nd);
                    }));

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                    {
                        foreach (Port port in note.GetPorts())
                        {
                            PortTreeNode pd = new PortTreeNode(port);
                            nd.AddNode(pd);
                            pd.RefreshSelected();
                        }
                        nd.RefreshSelected();
                    }));

                    foreach (Link link in note.LinksOutOf)
                    {
                        LinkTreeNode ld = new LinkTreeNode(link);
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                        {
                            this.Root.AddNode(ld);
                            ld.RefreshSelected();
                        }));
                    }
                }
            }));
        }

        public void LocateCenter(Part part)
        {
            if (part is Link link)
            {
                Point point1 = LinkLayer.GetStart(link);
                Point point2 = LinkLayer.GetEnd(link);
                Point center = new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
                this.LocateCenterCallBack.Invoke(center);
            }
            else
            {
                Rect rect = part.Bound;
                Point center = new Point(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2));
                this.LocateCenterCallBack.Invoke(center);
            }
        }

        public void LocateRect(Part part)
        {
            if (part is Link link)
            {
                Point point1 = LinkLayer.GetStart(link);
                Point point2 = LinkLayer.GetEnd(link);
                Rect rect = new Rect(point1, point2);
                this.LocateRectCallBack.Invoke(rect);
            }
            else
            {
                Rect rect = part.Bound;
                this.LocateRectCallBack.Invoke(rect);
            }
        }
        #endregion 
    }
}
