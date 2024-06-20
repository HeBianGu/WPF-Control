// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.ObjectModel;

namespace H.Windows.Ribbon
{
    public class TabBindable : ControlBindableBase
    {
        public TabBindable()
            : this(null)
        {
        }

        public TabBindable(string header)
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
                    RaisePropertyChanged();
                }
            }
        }
        private string _header;

        public string ContextualTabGroupHeader
        {
            get
            {
                return _contextualTabGroupHeader;
            }

            set
            {
                if (_contextualTabGroupHeader != value)
                {
                    _contextualTabGroupHeader = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _contextualTabGroupHeader;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged();
                }
            }
        }
        private bool _isSelected;

        public ObservableCollection<GroupBindable> GroupDataCollection
        {
            get
            {
                if (_groupDataCollection == null)
                {
                    _groupDataCollection = new ObservableCollection<GroupBindable>();
                }
                return _groupDataCollection;
            }
        }
        private ObservableCollection<GroupBindable> _groupDataCollection;
    }
}
