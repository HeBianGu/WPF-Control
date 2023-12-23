using H.Extensions.Setting;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.App.FileManager
{
    [Display(Name = "系统配置")]
    public class AppSetting : Setting<AppSetting>
    {
        private DisplayMode _displayMode;
        [Display(Name = "布局方式")]
        public DisplayMode DisplayMode
        {
            get { return _displayMode; }
            set
            {
                _displayMode = value;
                RaisePropertyChanged();
            }
        }

        private ViewSizeMode _viewSizeMode;
        [Display(Name = "缩率图尺寸")]
        public ViewSizeMode ViewSizeMode
        {
            get { return _viewSizeMode; }
            set
            {
                _viewSizeMode = value;
                RaisePropertyChanged();
            }
        }

        private Stretch _stretch;
        [Display(Name = "缩率图拉伸方式")]
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
}
