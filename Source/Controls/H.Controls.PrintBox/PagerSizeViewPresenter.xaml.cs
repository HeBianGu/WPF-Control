global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;
global using H.Extensions.Common;
global using H.Mvvm.ViewModels.Base;
global using H.Services.Setting;

namespace H.Controls.PrintBox
{
    [Display(Name = "纸张设置", GroupName = SettingGroupNames.GroupControl, Description = "纸张设置")]
    public class PagerSizeViewPresenter : BindableBase, IPagerSizeViewPresenter
    {
        public PagerSizeViewPresenter()
        {
            this.Collection = PageSize.Defines.ToObservable();
            this.SelectedPagerSizeData = this.Collection?.FirstOrDefault();
        }

        private PageSize _selectedPagerSizeData;
        public PageSize SelectedPagerSizeData
        {
            get { return _selectedPagerSizeData; }
            set
            {
                _selectedPagerSizeData = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PageSize> _collection = new ObservableCollection<PageSize>();
        public ObservableCollection<PageSize> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private int _layoutSelectedIndex;
        public int LayoutSelectedIndex
        {
            get { return _layoutSelectedIndex; }
            set
            {
                _layoutSelectedIndex = value;
                RaisePropertyChanged();
            }
        }

        public Size GetSize()
        {
            if (this.LayoutSelectedIndex == 0)
                return new Size(this.SelectedPagerSizeData.SizeInInch.Height * 96, this.SelectedPagerSizeData.SizeInInch.Width * 96);
            return new Size(this.SelectedPagerSizeData.SizeInInch.Width * 96, this.SelectedPagerSizeData.SizeInInch.Height * 96);
        }
    }
}
