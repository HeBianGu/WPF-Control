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

/// <summary>
/// 支持输入文本和选择参数两种方式，输入文本的TypeConverter需要包含在集合中
/// </summary>
public class ComboBoxTextPropertyItem : TextPropertyItem
{
    public ComboBoxTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
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

    private IEnumerable<object> GetCollection()
    {
        if (this.Collection.Count > 0)
            return this.Collection;
        return this.Collection = this.CreateSource().ToObservable();
    }

    protected override bool CheckType(string value, out string error)
    {
        error = null;
        try
        {
            object to = this.ConverToObject(value);
            if (!this.GetCollection().Any(x => x.Equals(to)))
            {
                error = $"[{this.Name}]不在可选范围内";
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message + $"[{this.PropertyInfo.PropertyType.Name}]";
            return false;
        }
    }

    protected override void SetValue(string value)
    {
        base.SetValue(value);
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



/// <summary>
/// 输入的值需要是T类型的值，如果不是则会提示错误
/// </summary>
/// <typeparam name="T"></typeparam>
public class ComboBoxTextPropertyItem<T> : ComboBoxTextPropertyItem
{
    public ComboBoxTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override bool CheckType(string value, out string error)
    {
        var br = base.CheckType(value, out error);
        if (br)
            return true;
        if (value.TryChangeType(out T result))
        {
            error = null;
            return true;
        }
        else
        {
            error = $"[{this.Name}]不是有效的{typeof(T).Name}";
            return false;
        }
    }
}
