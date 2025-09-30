// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Modbus.Presenters;
namespace H.Components.Modbus.Base;

public interface ITcpClientMaster
{
    List<IModbusTcpDataItem> ModbusDatas { get; set; }
    MasterState State { get; set; }
    void Dispose();
    void Read();
}

