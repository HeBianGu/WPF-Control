// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.ResultPresenter.ResultPresenters;

public class DataGridResultPresenter<T> : DataGridResultPresenterBase where T : IResultPresenterItem
{
    public DataGridResultPresenter(IEnumerable<T> values)
    {
        this.Collection = values.ToObservable();
        for (int i = 0; i < this.Collection.Count(); i++)
        {
            this.Collection[i].Index = i + 1;
        }
        this.Type = typeof(T);
    }
    private ObservableCollection<T> _collection = new ObservableCollection<T>();
    public ObservableCollection<T> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }

    private T _selectedItem;
    public T SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }
}
