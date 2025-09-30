// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.ComponentModel;
global using System.Text.Json.Serialization;
using H.Mvvm.Commands;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;

namespace H.Mvvm.ViewModels.Base;

/// <summary>
/// 可绑定的基类。
/// </summary>
public abstract class Bindable : BindableBase
{
    [Browsable(false)]
    [JsonIgnore]
    [XmlIgnore]
    public RelayCommand RelayCommand { get; set; }

    [Browsable(false)]
    [JsonIgnore]
    [XmlIgnore]
    public RelayCommand LoadedCommand => new RelayCommand(Loaded);

    [Browsable(false)]
    [JsonIgnore]
    [XmlIgnore]
    public RelayCommand CallMethodCommand { get; set; }

    /// <summary>
    /// 用于继承类中的方法绑定。
    /// </summary>
    /// <param name="obj">方法参数。</param>
    protected virtual void RelayMethod(object obj)
    {

    }

    /// <summary>
    /// 构造函数。
    /// </summary>
    public Bindable()
    {
        this.RelayCommand = new RelayCommand(RelayMethod);
        this.CallMethodCommand = new RelayCommand(CallMethod);
        RelayMethod("init");
    }

    private object _targetElement;
    protected T GetTargetElement<T>() where T : UIElement => (T)this._targetElement;

    protected bool IsLoaded { get; set; }
    /// <summary>
    /// 加载事件处理方法。
    /// </summary>
    /// <param name="obj">事件参数。</param>
    protected virtual void Loaded(object obj)
    {
        if (obj is RoutedEventArgs args)
            this._targetElement = args.Source;
        this.IsLoaded = true;
    }

    /// <summary>
    /// 调用方法。
    /// </summary>
    /// <param name="obj">方法名。</param>
    protected virtual void CallMethod(object obj)
    {
        string methodName = obj?.ToString();
        MethodInfo method = GetType().GetMethod(methodName);
        if (method == null)
            throw new ArgumentException("no found method :" + method);
        object[] parameters = method.GetParameters().Select(l => l.RawDefaultValue is DBNull ? null : l.RawDefaultValue).ToArray();
        method.Invoke(this, parameters);
    }
}
