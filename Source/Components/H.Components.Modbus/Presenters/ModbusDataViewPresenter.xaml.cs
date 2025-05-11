// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using H.Components.Modbus.Base;
global using H.Iocable;
global using H.Mvvm.Commands;
global using H.Mvvm.ViewModels.Base;
global using H.Services.Message;
global using H.Services.Message.Dialog;
global using System.ComponentModel.DataAnnotations;

namespace H.Components.Modbus.Presenters;

[Display(Name = "Modbus寄存器管理")]
public class ModbusDataViewPresenter : DisplayBindableBase
{
    public ModbusDataViewPresenter()
    {
        this.DataService = Ioc.GetService<ISerializableModbusDataService>();
    }
    private IModbusDataService _dataService;
    public IModbusDataService DataService
    {
        get { return _dataService; }
        set
        {
            _dataService = value;
            RaisePropertyChanged();
        }
    }

    private IModbusDataItem _selectedItem;
    public IModbusDataItem SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand AddCommand => new RelayCommand(async x =>
    {
        UshortModbusDataItem item = new UshortModbusDataItem();
        var r = await IocMessage.Form.ShowEdit(item, x => x.Title = "添加Modbus地址");
        if (r != true)
            return;
        this.DataService.Add(item);
    }, x => this.DataService.CanStart());

    public RelayCommand DeleteCommand => new RelayCommand(async x =>
    {
        if (x is IModbusDataItem item)
        {
            await IocMessage.Dialog.ShowDeleteDialog(l =>
            {
                this.DataService.Delete(item);
            });
        }
    }, x => this.DataService.CanStart());

    public RelayCommand EditCommand => new RelayCommand(async x =>
    {
        if (x is IModbusDataItem item)
            await IocMessage.Form.ShowEdit(item, x => x.Title = "编辑");
    }, x => this.DataService.CanStart());

    public RelayCommand ClearCommand => new RelayCommand(async x =>
    {
        await IocMessage.Dialog.ShowDeleteAllDialog(l =>
        {
            this.DataService.Clear();
        });
    }, x => this.DataService.Collection.Count > 0);

    public RelayCommand StartCommand => new RelayCommand(async x =>
    {
        await this.DataService.Start();
    }, x => this.DataService.CanStart());

    public RelayCommand StopCommand => new RelayCommand(async x =>
    {
        await this.DataService.Stop();
    }, x => this.DataService.CanStop());

}
