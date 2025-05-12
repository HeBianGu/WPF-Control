// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Modbus.Presenters;

namespace H.Components.Modbus;

public interface IModbusDataService
{
    ModbusTcpDatas Collection { get; set; }
    List<ITcpClientMaster> Masters { get; set; }
    ModbusDataState State { get; set; }
    void Add(IModbusTcpDataItem item);
    bool CanStart();
    bool CanStop();
    void Clear();
    List<ITcpClientMaster> CreateTcpClientMasters();
    void Delete(IModbusTcpDataItem item);
    List<ITcpClientMaster> GetMasters();
    void InvalidateMasters();
    Task Start();
    Task Stop();
}