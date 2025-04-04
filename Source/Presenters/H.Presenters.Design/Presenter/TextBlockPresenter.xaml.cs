global using H.Themes.FontSizes;
global using System.ComponentModel;
global using System.Windows;
using H.Themes.FontSizes;
using H.Themes.Layouts;

namespace H.Presenters.Design.Presenter;

[Display(Name = "文本")]
public class TextBlockPresenter : CommandsDesignPresenterBase, ITextData
{
    public TextBlockPresenter()
    {
        this.Text = this.Name;
    }
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.FontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
        //this.FontFamily = Application.Current.FindResource(SystemKeys.FontFamily) as FontFamily;
        this.FontStyle = FontStyles.Normal;
        this.FontWeight = FontWeights.Normal;
        this.FontStretch = FontStretches.Normal;
        //this.Foreground = Application.Current.FindResource(BrushKeys.Foreground) as Brush;
        //this.Height = (double)Application.Current.FindResource(SystemKeys.ItemHeight);
        this.Foreground = Brushes.Black;
        this.Height = (double)Application.Current.FindResource(LayoutKeys.RowHeight);
        this.VerticalContentAlignment = VerticalAlignment.Center;
    }
    private string _text;
    [Display(Name = "文本", GroupName = "常用,数据")]
    public string Text
    {
        get { return _text; }
        set
        {
            _text = value;
            RaisePropertyChanged();
        }
    }

    private double _fontSize;
    [Display(Name = "字号", GroupName = "常用,样式")]
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
    [Display(Name = "字体", GroupName = "样式")]
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
    [Display(Name = "字体样式", GroupName = "样式")]
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
    [Display(Name = "字体加粗", GroupName = "样式")]
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
    [Display(Name = "字体展开", GroupName = "样式")]
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
    [Display(Name = "文本颜色", GroupName = "常用,样式")]
    public Brush Foreground
    {
        get { return _foreground; }
        set
        {
            _foreground = value;
            RaisePropertyChanged();
        }
    }
}
