using H.Themes.FontSizes;

namespace H.Presenters.Design.Presenter;

[Display(Name = "标题")]
public class TitlePresenter : TextBlockPresenter
{
    public TitlePresenter()
    {
        this.Title = "我是标题：";
        this.Text = "我是数据";

    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.TitleFontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
        this.TitleFontWeight = FontWeights.Normal;
        //this.TitleForeground = Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush;
        this.TitleForeground = Brushes.Black;

    }

    private string _title;
    [Display(Name = "文本颜色", GroupName = "常用,数据")]
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            RaisePropertyChanged();
        }
    }

    private double _titleFontSize;
    [Display(Name = "抬头字号", GroupName = "常用,样式")]
    public double TitleFontSize
    {
        get { return _titleFontSize; }
        set
        {
            _titleFontSize = value;
            RaisePropertyChanged();
        }
    }

    private Brush _titleForeground;
    [DefaultValue(null)]
    [Display(Name = "抬头颜色", GroupName = "常用,样式")]
    public Brush TitleForeground
    {
        get { return _titleForeground; }
        set
        {
            _titleForeground = value;
            RaisePropertyChanged();
        }
    }

    private FontWeight _titleFontWeight;
    [Display(Name = "抬头加粗", GroupName = "常用,样式")]
    public FontWeight TitleFontWeight
    {
        get { return _titleFontWeight; }
        set
        {
            _titleFontWeight = value;
            RaisePropertyChanged();
        }
    }
}
