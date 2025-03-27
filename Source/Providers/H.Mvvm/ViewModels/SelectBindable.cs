// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Common.Interfaces;

namespace H.Mvvm.ViewModels;

public partial class SelectBindable<T> : ModelBindable<T>, ISelectable
{
    public SelectBindable(T t) : base(t)
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
