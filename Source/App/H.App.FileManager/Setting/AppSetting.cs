global using H.Extensions.Setting;
global using System.Windows.Media;

namespace H.App.FileManager
{
    [Display(Name = "系统配置", GroupName = SettingGroupNames.GroupApp)]
    public class AppSetting : Settable<AppSetting>
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

        private bool _useProject = true;
        [DefaultValue(true)]
        [Display(Name = "启用工程管理")]
        public bool UseProject
        {
            get { return _useProject; }
            set
            {
                _useProject = value;
                RaisePropertyChanged();
            }
        }
    }
}
