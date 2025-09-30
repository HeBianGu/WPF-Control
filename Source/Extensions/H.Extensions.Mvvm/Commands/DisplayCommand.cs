// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Text.Json.Serialization;
using H.Mvvm.Commands;

namespace H.Extensions.Mvvm.Commands;

public class DisplayCommand : RelayCommand, IDisplayCommand, INotifyPropertyChanged
{
    public DisplayCommand(Action<object> action) : base(action)
    {

    }

    public DisplayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
    {

    }

    public string Name { get; set; }

    private string _description;
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public virtual string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            RaisePropertyChanged();
        }
    }

    private int _order;
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public virtual int Order
    {
        get { return _order; }
        set
        {
            _order = value;
            RaisePropertyChanged();
        }
    }

    private string _icon;
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public string Icon
    {
        get { return _icon; }
        set
        {
            _icon = value;
            RaisePropertyChanged();
        }
    }

    private string _groupName;
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public string GroupName
    {
        get { return _groupName; }
        set
        {
            _groupName = value;
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
