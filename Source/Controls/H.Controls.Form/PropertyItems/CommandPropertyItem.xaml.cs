// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItems;

public class CommandPropertyItem : ObjectPropertyItem<ICommand>
{
    public CommandPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();
        this.ReadOnly = readyOnly?.IsReadOnly == true;
        //CommandAttribute command = property.GetCustomAttribute<CommandAttribute>();
        //this.Icon = command?.Icon;
        //HotKeyAttribute hotkey = property.GetCustomAttribute<HotKeyAttribute>();
        //this.HotKey = hotkey?.ToString();
    }

    public string HotKey { get; set; }

    //private string _icon;
    ///// <summary> 说明  </summary>
    //public string Icon
    //{
    //    get { return _icon; }
    //    set
    //    {
    //        _icon = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private object _commandParameter;
    /// <summary> 说明  </summary>
    public object CommandParameter
    {
        get { return _commandParameter; }
        set
        {
            _commandParameter = value;
            RaisePropertyChanged();
        }
    }

}
