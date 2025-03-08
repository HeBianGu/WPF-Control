namespace H.Controls.Diagram.Presenter.NodeDatas.Card;

public class TitleCardNodeData : CardNodeData
{
    public TitleCardNodeData()
    {
        this.Icon = "\xe7db";
    }
    private string _title;
    [Display(Name = "标题", GroupName = "样式")]
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            RaisePropertyChanged();
        }
    }

    private Brush _titleForeground;
    [DefaultValue(null)]
    [Display(Name = "标题颜色", GroupName = "样式")]
    public Brush TitleForeground
    {
        get { return _titleForeground; }
        set
        {
            _titleForeground = value;
            RaisePropertyChanged();
        }
    }

}

public class EndCardNodeData : TitleCardNodeData
{
    public EndCardNodeData()
    {
        this.Title = "结束节点";
        this.Icon = "\xe842";
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        //this.TitleForeground = Application.Current.FindResource(BrushKeys.Orange) as Brush;
    }
}

public class StartCardNodeData : TitleCardNodeData
{
    public StartCardNodeData()
    {
        this.UseStart = true;
        this.Title = "开始节点";
        this.Icon = "\xe843";
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        //this.TitleForeground = Application.Current.FindResource(BrushKeys.Orange) as Brush;
    }
}

public class MessageCardNodeData : TitleCardNodeData
{
    public MessageCardNodeData()
    {
        this.Icon = "\xe737";
    }
}
