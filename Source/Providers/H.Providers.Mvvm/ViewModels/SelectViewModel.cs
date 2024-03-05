// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;

namespace H.Providers.Mvvm
{

    public partial class SelectViewModel<T> : ModelViewModel<T>, ISelectViewModel
    {
        public SelectViewModel(T t) : base(t)
        {

        }

        private bool _isSelected;
        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

    }
}
