global using H.Controls.Diagram.Presenter.NodeDatas.Base;
global using H.Common.Interfaces;
global using H.Themes.FontSizes;
using H.Themes.FontSizes;
namespace H.Controls.Diagram.Presenter.NodeDatas;

public interface ITextNodeData : ITextable
{
    FontFamily FontFamily { get; set; }
    double FontSize { get; set; }
    FontStretch FontStretch { get; set; }
    FontStyle FontStyle { get; set; }
    FontWeight FontWeight { get; set; }
    Brush Foreground { get; set; }
    Thickness TextMargin { get; set; }
}

public class TextNodeData : PortableNodeData, ITextNodeData
{
    public TextNodeData()
    {
        this.Text = this.Name??this.GetType().Name;
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.FontSize = Application.Current == null ? 15.0 : (double)Application.Current.FindResource(FontSizeKeys.Default);
        //this.FontFamily = Application.Current.FindResource(LayoutKeys.FontFamily) as FontFamily;
        this.FontStyle = FontStyles.Normal;
        this.FontWeight = FontWeights.Normal;
        this.FontStretch = FontStretches.Normal;
    }

    private string _text;
    [Display(Name = "文本", GroupName = "常用")]
    public virtual string Text
    {
        get { return _text; }
        set
        {
            _text = value;
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

    private Thickness _textMargin = new Thickness(0);
    [Display(Name = "文本间距", GroupName = "常用")]
    public Thickness TextMargin
    {
        get { return _textMargin; }
        set
        {
            _textMargin = value;
            RaisePropertyChanged();
        }
    }

    public override void ApplayStyleTo(INodeData to)
    {
        base.ApplayStyleTo(to);

        if (to is TextNodeData textNodeData)
        {
            textNodeData.Foreground = this.Foreground;
            textNodeData.FontFamily = this.FontFamily;
            textNodeData.FontSize = this.FontSize;
            textNodeData.FontStretch = this.FontStretch;
            textNodeData.FontStyle = this.FontStyle;
            textNodeData.FontWeight = this.FontWeight;
        }
    }
}
