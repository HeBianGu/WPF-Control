// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using H.Services.Logger;
global using NModbus;
global using System.Net.Sockets;
using H.Components.Modbus.Presenters;
using H.Mvvm.ViewModels.Base;

namespace H.Components.Modbus.Base;

public class TcpClientMaster : BindableBase, ITcpClientMaster, IDisposable
{
    private readonly string _ip;
    private readonly int _port;
    public TcpClientMaster(string ip, int port, List<IModbusTcpDataItem> modbusDatas)
    {
        this._ip = ip;
        this._port = port;
        this.ModbusDatas = modbusDatas;
    }
    private IModbusMaster _master;
    private TcpClient _client;
    public List<IModbusTcpDataItem> ModbusDatas { get; set; } = new List<IModbusTcpDataItem>();
    private MasterState _state;
    public MasterState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged();
            foreach (var item in this.ModbusDatas)
            {
                item.State = value;
            }
        }
    }

    private bool Connect()
    {

        this._client?.Dispose();
        try
        {
            this._client = new TcpClient(this._ip, this._port);
            ModbusFactory factory = new ModbusFactory();
            this._master = factory.CreateMaster(this._client);
            this._master.Transport.ReadTimeout = 100;
            this._master.Transport.Retries = 3;
            this._master.Transport.SlaveBusyUsesRetryCount = true;
            return true;
        }
        catch (Exception ex)
        {
            IocLog.Instance?.Error(ex);
        }
        return false;
    }

    public void Read()
    {

        if (this._client == null || !this._client.Connected)
        {
            this.State = MasterState.Connectting;
            if (!this.Connect())
                this.State = MasterState.Unconnet;
            return;
        }
        this.State = MasterState.Connected;
        foreach (var item in this.ModbusDatas.OfType<IUnshortModbusTcpDataItem>())
        {
            try
            {
                ushort[] registers = _master.ReadHoldingRegisters(item.SlaveAddress, item.StartAddress, item.NumberOfPoints);
                ushort value = registers[0];
                item.Value = value;
                item.UpdateTime = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"{item.Ip}:{value}");
            }
            catch (Exception ex)
            {
                IocLog.Instance?.Error(ex);
                item.Message = ex.Message;
                this.State = MasterState.ReadError;
            }
        }
    }

    public void Dispose()
    {
        this._client?.Dispose();
        this._client = null;
        this._master?.Dispose();
        this._master = null;
    }
}

