// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using System.Text.Json.Serialization;
global using System.Windows;
using H.Components.Modbus.Presenters;

namespace H.Components.Modbus;

[Display(Name = "寄存器数据服务")]
public class ModbusDataService : DisplayBindableBase, IModbusDataService
{
    private ModbusDataState _state;
    [JsonIgnore]
    public ModbusDataState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged();
        }
    }

    private ModbusTcpDatas _collection = new ModbusTcpDatas();
    public ModbusTcpDatas Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }

    private List<ITcpClientMaster> _masters;
    [JsonIgnore]
    public List<ITcpClientMaster> Masters
    {
        get { return _masters; }
        set
        {
            _masters = value;
            RaisePropertyChanged();
        }
    }

    public List<ITcpClientMaster> GetMasters()
    {
        if (this.Masters == null)
            this.Masters = this.CreateTcpClientMasters();
        return this.Masters;
    }

    public void InvalidateMasters()
    {
        this._masters = null;
    }

    public List<ITcpClientMaster> CreateTcpClientMasters()
    {
        var addresses = this.Collection.GroupBy(x => Tuple.Create(x.Ip, x.Port));
        List<ITcpClientMaster> masters = new List<ITcpClientMaster>();
        foreach (var item in addresses)
        {
            TcpClientMaster master = new TcpClientMaster(item.Key.Item1, item.Key.Item2, item.ToList());
            masters.Add(master);

        }
        return masters;
    }

    private CancellationTokenSource _cancellationTokenSource;
    private TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();
    public async Task Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        this.State = ModbusDataState.Running;
        foreach (var item in this.Collection)
        {
            item.State = MasterState.Waitting;
        }
        await Task.Run(() =>
        {
            while (true)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    return;
                if (Application.Current == null)
                    return;
                var masters = this.GetMasters();
                foreach (var master in masters)
                {
                    master.Read();
                }
                Thread.Sleep(1000);
            }
        }, _cancellationTokenSource.Token);
        _taskCompletionSource.SetResult(true);

    }

    public async Task Stop()
    {
        if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
        {
            this.State = ModbusDataState.Canceling;
            _taskCompletionSource = new TaskCompletionSource<bool>();
            _cancellationTokenSource?.Cancel();
            await _taskCompletionSource.Task;
        }
        this.State = ModbusDataState.Stopped;
        foreach (var item in this.Collection)
        {
            item.State = MasterState.Stopped;
        }
    }

    public bool CanStart() => this.State == ModbusDataState.Stopped;
    public bool CanStop() => this.State == ModbusDataState.Running;

    public void Add(IModbusTcpDataItem item)
    {
        this.Collection.Add(item);
        this.InvalidateMasters();
    }

    public void Delete(IModbusTcpDataItem item)
    {
        this.Collection.Remove(item);
        this.InvalidateMasters();
    }

    public void Clear()
    {
        this.Collection.Clear();
        this.InvalidateMasters();
    }
}

