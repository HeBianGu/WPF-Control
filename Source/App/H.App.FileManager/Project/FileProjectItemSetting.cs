global using H.Mvvm.ViewModels.Base;
global using H.Services.Setting;

namespace H.App.FileManager
{
    [Display(Name = "当前工程",GroupName = "工程设置")]
    public class FileProjectItemSetting : DisplayBindableBase, ISettable
    {
        private bool _useFavorite = true;
        [Display(Name = "启用喜欢过滤器")]
        public bool UseFavorite
        {
            get { return _useFavorite; }
            set
            {
                _useFavorite = value;
                RaisePropertyChanged();
            }
        }

        private bool _useFavoritePath = true;
        [Display(Name = "启用收藏夹")]
        public bool UseFavoritePath
        {
            get { return _useFavoritePath; }
            set
            {
                _useFavoritePath = value;
                RaisePropertyChanged();
            }
        }

        private bool _useSeeLater;
        [Display(Name = "启用稍后观看")]
        public bool UseSeeLater
        {
            get { return _useSeeLater; }
            set
            {
                _useSeeLater = value;
                RaisePropertyChanged();
            }
        }

        private bool _useAreaTag = false;
        [Display(Name = "启用国家标记")]
        public bool UseAreaTag
        {
            get { return _useAreaTag; }
            set
            {
                _useAreaTag = value;
                RaisePropertyChanged();
            }
        }

        private bool _useArticulationTag = false;
        [Display(Name = "启用清晰度标记")]
        public bool UseArticulationTag
        {
            get { return _useArticulationTag; }
            set
            {
                _useArticulationTag = value;
                RaisePropertyChanged();
            }
        }

        private bool _useExtension = true;
        [Display(Name = "启用扩展名")]
        public bool UseExtension
        {
            get { return _useExtension; }
            set
            {
                _useExtension = value;
                RaisePropertyChanged();
            }
        }

        private bool _useFileType = true;
        [Display(Name = "启用文件类型")]
        public bool UseFileType
        {
            get { return _useFileType; }
            set
            {
                _useFileType = value;
                RaisePropertyChanged();
            }
        }


        private bool _useHistory = true;
        [DefaultValue(true)]
        [Display(Name = "启用历史记录")]
        public bool UseHistory
        {
            get { return _useHistory; }
            set
            {
                _useHistory = value;
                RaisePropertyChanged();
            }
        }


        private bool _useWatched = false;
        [Display(Name = "启用观看")]
        public bool UseWatched
        {
            get { return _useWatched; }
            set
            {
                _useWatched = value;
                RaisePropertyChanged();
            }
        }


        private bool _useScore = true;
        [Display(Name = "启用评分")]
        public bool UseScore
        {
            get { return _useScore; }
            set
            {
                _useScore = value;
                RaisePropertyChanged();
            }
        }


        private bool _useObjectTag = false;
        [Display(Name = "启用对象标记")]
        public bool UseObjectTag
        {
            get { return _useObjectTag; }
            set
            {
                _useObjectTag = value;
                RaisePropertyChanged();
            }
        }


        private bool _useOrderBox = true;
        [Display(Name = "启用排序器")]
        public bool UseOrderBox
        {
            get { return _useOrderBox; }
            set
            {
                _useOrderBox = value;
                RaisePropertyChanged();
            }
        }

        private bool _usePixelFormat = false;
        [Display(Name = "启用像素格式")]
        public bool UsePixelFormat
        {
            get { return _usePixelFormat; }
            set
            {
                _usePixelFormat = value;
                RaisePropertyChanged();
            }
        }
        public bool IsVisibleInSetting { get; set; } = true;
    }
}
