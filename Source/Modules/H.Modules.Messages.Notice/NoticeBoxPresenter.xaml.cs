// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Adorner;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Notice
{
    public class NoticeBoxPresenter : NotifyPropertyChangedBase
    {
        private ObservableCollection<INoticeItem> _collection = new ObservableCollection<INoticeItem>();
        public ObservableCollection<INoticeItem> Collection
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
