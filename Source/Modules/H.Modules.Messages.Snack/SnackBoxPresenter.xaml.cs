// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Controls.Adorner;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Snack
{
    public class SnackBoxPresenter : NotifyPropertyChangedBase
    {
        private ObservableCollection<ISnackItem> _collection = new ObservableCollection<ISnackItem>();
        public ObservableCollection<ISnackItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }
}
