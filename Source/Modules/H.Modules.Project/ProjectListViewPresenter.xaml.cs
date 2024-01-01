// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Modules.Project
{
    public class ProjectListViewPresenter : ViewModelBase
    {
        private IProjectItem _selectedItem;
        public IProjectItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
    }
}
