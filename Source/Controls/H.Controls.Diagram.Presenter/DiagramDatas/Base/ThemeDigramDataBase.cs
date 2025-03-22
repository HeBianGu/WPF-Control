namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class ThemeDigramDataBase : TreeDigramDataBase, IThemeDigramDataBase
{
    protected ThemeDigramDataBase()
    {
        this.DiagramThemeGroups = this.CreateDiagramThemes()?.ToObservable();
    }

    private ObservableCollection<DiagramThemeGroup> _diagramThemeGroups = new ObservableCollection<DiagramThemeGroup>();
    [System.Text.Json.Serialization.JsonIgnore]

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

    [Icon(FontIcons.Refresh)]
    [Display(Name = "默认样式", GroupName = "操作", Order = 6, Description = "点击此功能，恢复所有节点、连线和端口默认样式")]
    public new DisplayCommand LoadDefaultCommand => new DisplayCommand(e =>
    {
        foreach (var node in this.Datas.NodeDatas)
        {
            IEnumerable<IDefaultable> displayers = node.GetPartDatas(this).OfType<IDefaultable>();
            foreach (IDefaultable item in displayers)
            {
                item.LoadDefault();
            }
            if (node is IDefaultable displayer)
                displayer.LoadDefault();
        }
    }, x => this.Datas.NodeDatas.Count > 0);

    public RelayCommand ApplyDiagramThemeCommand => new RelayCommand(e =>
    {
        if (e is DiagramTheme project)
        {
            //foreach (Node node in this.Nodes)
            //{
            //    if (node.Content is INodeData nodeData)
            //        project.Note.ApplayStyleTo(nodeData);

            //    foreach (Link link in node.GetAllLinks().Distinct())
            //    {
            //        if (link.Content is ILinkData linkData)
            //            project.Link.ApplayStyleTo(linkData);
            //    }

            //    foreach (Port port in node.GetPorts().Distinct())
            //    {
            //        if (port.Content is IPortData portData)
            //            project.Port.ApplayStyleTo(portData);
            //    }
            //}
        }
    });

    public RelayCommand ApplayNodeStyleCommand => new RelayCommand(e =>
    {
        if (e is TextNodeData project)
        {
            //if (this.SelectedPart?.Content is INodeData nodeData)
            //    project.ApplayStyleTo(nodeData);
        }
    });

    public RelayCommand ApplayLinkStyleCommand => new RelayCommand(e =>
    {
        if (e is TextLinkData project)
        {
            //if (this.SelectedPart?.Content is ILinkData data)
            //    project.ApplayStyleTo(data);
        }
    });

    public RelayCommand ApplayPortStyleCommand => new RelayCommand(e =>
    {
        if (e is TextPortData project)
        {
            //if (this.SelectedPart?.Content is IPortData data)
            //    project.ApplayStyleTo(data);
        }
    });

}
