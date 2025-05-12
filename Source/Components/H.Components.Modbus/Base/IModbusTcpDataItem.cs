// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Components.Modbus.Presenters;

namespace H.Components.Modbus.Base;

public interface IModbusTcpDataItem
{
    string Ip { get; set; }
    int Port { get; set; }
    byte SlaveAddress { get; set; }
    ushort StartAddress { get; set; }
    ushort NumberOfPoints { get; set; }
    string Message { get; set; }
    MasterState State { get; set; }
    DateTime? UpdateTime { get; set; }

}

