// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.PropertyItems.Base;

public abstract class ItemsSourcePropertyItem<T> : ObjectPropertyItem<T>
{
    public ItemsSourcePropertyItem(PropertyInfo property, object obj)
        : base(property, obj)
    {

    }
    private ObservableCollection<T> _collection = new ObservableCollection<T>();
    public ObservableCollection<T> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged("Collection");
        }
    }
}
