// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Validation.Attributes;
using H.Mvvm.ViewModels.Base;
using System.ComponentModel;

namespace H.Components.Modbus.Presenters;

public class ModbusTcpDataItemBase : BindableBase, IModbusTcpDataItem
{
    private string _name = "新建地址";
    [DefaultValue("新建地址")]
    [Display(Name = "名称", GroupName = "数据", Description = "读取位置")]
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    private string _ip = "127.0.0.1";
    [IPAddress]
    [DefaultValue("127.0.0.1")]
    [Display(Name = "Slave IP", GroupName = "数据", Description = "Modbus地址")]
    public string Ip
    {
        get { return _ip; }
        set
        {
            _ip = value;
            RaisePropertyChanged();
        }
    }

    private int _port = 502;
    [DefaultValue(502)]
    [Display(Name = "Slave端口号", GroupName = "数据", Description = "Modbus端口号")]
    public int Port
    {
        get { return _port; }
        set
        {
            _port = value;
            RaisePropertyChanged();
        }
    }

    private byte _slaveAddress = 1;
    [DefaultValue(1)]
    [Display(Name = "Slave地址", GroupName = "数据", Description = "要从中读取值的设备的地址")]
    public byte SlaveAddress
    {
        get { return _slaveAddress; }
        set
        {
            _slaveAddress = value;
            RaisePropertyChanged();
        }
    }

    private ushort _startAddress = 0;
    [DefaultValue(0)]
    [Display(Name = "读取位置", GroupName = "数据", Description = "读取位置")]
    public ushort StartAddress
    {
        get { return _startAddress; }
        set
        {
            _startAddress = value;
            RaisePropertyChanged();
        }
    }

    private ushort _numberOfPoints = 1;
    [DefaultValue(1)]
    [Display(Name = "读取长度", GroupName = "数据", Description = "读取位置")]
    public ushort NumberOfPoints
    {
        get { return _numberOfPoints; }
        set
        {
            _numberOfPoints = value;
            RaisePropertyChanged();
        }
    }

    private MasterState _state;
    public MasterState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged();
        }
    }

    private string _message;
    [Browsable(false)]
    [Display(Name = "信息", GroupName = "数据", Description = "读取位置")]
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

    private DateTime? _updateTime;
    [Browsable(false)]
    [Display(Name = "更新时间", GroupName = "数据", Description = "读取位置")]
    public DateTime? UpdateTime
    {
        get { return _updateTime; }
        set
        {
            _updateTime = value;
            RaisePropertyChanged();
        }
    }




}

public class ModbusTcpDataItem<T> : ModbusTcpDataItemBase
{
    private T _value;
    [Display(Name = "读取结果", GroupName = "数据", Description = "读取位置")]
    public T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }

    //public override string ToString()
    //{
    //    return $"{this.Name}[{this.State}]:{this.Value}";
    //}
}

