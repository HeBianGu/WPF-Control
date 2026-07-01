// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using System.Windows.Controls.Primitives;

namespace H.Controls.Form.PropertyItem.TextPropertyItems;

public class ComboBoxTextPropertyItemItem : TextPropertyItem
{
    public ComboBoxTextPropertyItemItem(PropertyInfo property, object obj) : base(property, obj)
    {
        this.Collection = new ObservableCollection<object>(this.CreateSource());
    }

    protected virtual IEnumerable<object> CreateSource()
    {
        var source = this.PropertyInfo.GetCustomAttribute<GetSourceAttribute>();
        if (source == null)
            return null;
        IEnumerable items = source.GetSource(this.PropertyInfo, this.Obj);
        var result = items.OfType<object>().ToList();
        return result;
    }

    private ObservableCollection<object> _collection = new ObservableCollection<object>();
    public ObservableCollection<object> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand MouseDoubleClickCommand => new RelayCommand(x =>
    {
        if (x is System.Windows.Input.MouseButtonEventArgs arg && arg.Source is ListBox listBox)
        {
            if (listBox.SelectedItem != null)
            {
                if (this.LoadDefaultValue(listBox.SelectedItem, out string v))
                {
                    this.Value = v;
                    var p = listBox.GetParent<Popup>();
                    if (p != null)
                        p.IsOpen = false;
                }
            }
        }
    });
}
