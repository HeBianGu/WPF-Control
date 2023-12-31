using H.Providers.Ioc;
using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.FileManager
{
    public class FileProjectItemSetting : NotifyPropertyChangedBase, ISetting
    {
        public int Order => 0;
        public string Name => "当前工程";
        public string GroupName => "工程设置";

        private bool _useFavorite;
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

        private bool _useFavoritePath;
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

        private bool _useAreaTag;
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
    }
}
