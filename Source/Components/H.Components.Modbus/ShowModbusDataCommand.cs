// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Common.Attributes;
using H.Common.Commands;
using H.Components.Modbus.Presenters;

namespace H.Components.Modbus;

[Icon("\xE963")]
[Display(Name = "寄存器管理", Description = "显示寄存器管理")]
public class ShowModbusDataCommand : DisplayMarkupCommandBase
{
    private ModbusDataViewPresenter _modbusDataViewPresenter = new ModbusDataViewPresenter();
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.ShowDialog(this._modbusDataViewPresenter, x =>
        {
            x.HorizontalAlignment = HorizontalAlignment.Stretch;
            x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            x.VerticalContentAlignment = VerticalAlignment.Stretch;
            x.MinWidth = 600;
            x.MinHeight = 400;
        });
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc<ISerializableModbusDataService>.Instance != null;
    }
}

