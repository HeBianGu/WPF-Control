global using H.Common.Attributes;
global using H.Styles;
global using System.ComponentModel.DataAnnotations;
global using System.Windows.Media;

namespace H.Presenters.Design.Presenter;

[Icon("\xEB9F")]
[Display(Name = "图片")]
public class ImagePresenter : CommandsDesignPresenterBase
{
    public ImagePresenter()
    {
        this.ImageSource = LogoDataProvider.Logo;
        this.Width = 200;
        this.Height = 80;
    }
    private ImageSource _imageSource;
    [Display(Name = "图像资源", GroupName = "常用,数据")]
    public ImageSource ImageSource
    {
        get { return _imageSource; }
        set
        {
            _imageSource = value;
            RaisePropertyChanged();
        }
    }

    private Stretch _stretch = Stretch.Uniform;
    [Display(Name = "拉伸方式", GroupName = "常用,布局")]
    public Stretch Stretch
    {
        get { return _stretch; }
        set
        {
            _stretch = value;
            RaisePropertyChanged();
        }
    }
}
