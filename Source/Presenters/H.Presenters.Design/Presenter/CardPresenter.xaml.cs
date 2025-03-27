namespace H.Presenters.Design.Presenter;

[Display(Name = "卡片")]
public class CardPresenter : TitlePresenter
{
    public CardPresenter()
    {
        this.Title = "总计：";
        this.Text = "80.34%";
        this.Height = 80.0;
    }
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.FromColor = Colors.Orange;
        this.ToColor = Colors.OrangeRed;
        this.DropShadowEffectOpacity = 0.5;
        this.Height = 80;
        this.Width = 200;
        this.Padding = new Thickness(10);
        this.Margin = new Thickness(10);
        this.CornerRadius = new CornerRadius(5);
        this.Foreground = Brushes.White;
        this.TitleForeground = Brushes.White;
        this.FontSize = 25;
        this.TitleFontSize = 15;
        this.HorizontalContentAlignment = HorizontalAlignment.Center;
        this.VerticalContentAlignment = VerticalAlignment.Center;
        this.Orientation = Orientation.Horizontal;
    }
    private Color _fromColor;
    [Display(Name = "起始颜色", GroupName = "常用,样式")]
    public Color FromColor
    {
        get { return _fromColor; }
        set
        {
            _fromColor = value;
            RaisePropertyChanged();
        }
    }

    private Color _toColor;
    [Display(Name = "终止颜色", GroupName = "常用,样式")]
    public Color ToColor
    {
        get { return _toColor; }
        set
        {
            _toColor = value;
            RaisePropertyChanged();
        }
    }

    private double _dropShadowEffectOpacity;
    [Display(Name = "阴影透明度", GroupName = "常用,样式")]
    public double DropShadowEffectOpacity
    {
        get { return _dropShadowEffectOpacity; }
        set
        {
            _dropShadowEffectOpacity = value;
            RaisePropertyChanged();
        }
    }


    private CornerRadius _CornerRadius;
    [Display(Name = "圆角", GroupName = "常用,样式")]
    public CornerRadius CornerRadius
    {
        get { return _CornerRadius; }
        set
        {
            _CornerRadius = value;
            RaisePropertyChanged();
        }
    }

    private Orientation _orientation;
    [Display(Name = "对齐方式", GroupName = "常用,样式")]
    public Orientation Orientation
    {
        get { return _orientation; }
        set
        {
            _orientation = value;
            RaisePropertyChanged();
        }
    }
}
