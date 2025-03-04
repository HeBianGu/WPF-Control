// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace H.Windows.Ribbon
{
    public class ContextualTabGroupBindable : BindableBase
    {
        public ContextualTabGroupBindable()
            : this(null)
        {
        }

        public ContextualTabGroupBindable(string header)
        {
            this.Header = header;
        }


        public string Header
        {
            get
            {
                return _header;
            }

            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Header"));
                }
            }
        }
        private string _header;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsVisible"));
                }
            }
        }
        private bool _isVisible;

        public ObservableCollection<TabBindable> TabDataCollection
        {
            get
            {
                if (_tabDataCollection == null)
                {
                    _tabDataCollection = new ObservableCollection<TabBindable>();
                }
                return _tabDataCollection;
            }
        }
        private ObservableCollection<TabBindable> _tabDataCollection;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
