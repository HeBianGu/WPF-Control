using H.Themes.FontSizes;

namespace H.Controls.Diagram.Presenter.LinkDatas;

public interface ITextLinkData : ITextable
{
    FontFamily FontFamily { get; set; }
    double FontSize { get; set; }
    FontStretch FontStretch { get; set; }
    FontStyle FontStyle { get; set; }
    FontWeight FontWeight { get; set; }
    Brush Foreground { get; set; }
    string Icon { get; set; }
}
public class TextLinkData : LinkData, ITextLinkData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.FontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
        //this.FontFamily = Application.Current.FindResource(LayoutKeys.FontFamily) as FontFamily;
        this.FontStyle = FontStyles.Normal;
        this.FontWeight = FontWeights.Normal;
        this.FontStretch = FontStretches.Normal;
    }
    private string _text;
    [Display(Name = "显示文本", GroupName = "常用")]
    public string Text
    {
        get { return _text; }
        set
        {
            _text = value;
            RaisePropertyChanged();
        }
    }

    private Brush _foreground;
    [DefaultValue(null)]
    [Display(Name = "文本颜色", GroupName = "常用")]
    public Brush Foreground
    {
        get { return _foreground; }
        set
        {
            _foreground = value;
            RaisePropertyChanged();
        }
    }

    private Brush _background;
    [DefaultValue(null)]
    [Display(Name = "文本背景", GroupName = "常用")]
    public Brush Background
    {
        get { return _background; }
        set
        {
            _background = value;
            RaisePropertyChanged();
        }
    }

    private double _fontSize;
    [Display(Name = "字号", GroupName = "常用")]
    public double FontSize
    {
        get { return _fontSize; }
        set
        {
            _fontSize = value;
            RaisePropertyChanged();
        }
    }

    private FontFamily _fontFamily;
    [Display(Name = "字体", GroupName = "文本")]
    public FontFamily FontFamily
    {
        get { return _fontFamily; }
        set
        {
            _fontFamily = value;
            RaisePropertyChanged();
        }
    }

    private FontStyle _fontStyle;
    [Display(Name = "字体样式", GroupName = "文本")]
    public FontStyle FontStyle
    {
        get { return _fontStyle; }
        set
        {
            _fontStyle = value;
            RaisePropertyChanged();
        }
    }

    private FontWeight _fontWeight;
    [Display(Name = "字体加粗", GroupName = "文本")]
    public FontWeight FontWeight
    {
        get { return _fontWeight; }
        set
        {
            _fontWeight = value;
            RaisePropertyChanged();
        }
    }

    private FontStretch _fontStretch;
    [Display(Name = "字体展开", GroupName = "文本")]
    public FontStretch FontStretch
    {
        get { return _fontStretch; }
        set
        {
            _fontStretch = value;
            RaisePropertyChanged();
        }
    }

    public override void ApplayStyleTo(ILinkData to)
    {
        base.ApplayStyleTo(to);

        if (to is TextLinkData data)
        {
            data.Foreground = this.Foreground;
            data.Background = this.Background;
            data.FontFamily = this.FontFamily;
            data.FontSize = this.FontSize;
            data.FontStretch = this.FontStretch;
            data.FontStyle = this.FontStyle;
            data.FontWeight = this.FontWeight;
        }
    }
}
