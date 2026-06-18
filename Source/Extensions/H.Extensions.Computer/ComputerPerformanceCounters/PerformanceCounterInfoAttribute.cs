// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Computer.ComputerPerformanceCounters;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class PerformanceCounterInfoAttribute : Attribute
{
    public string CategoryName { get; set; } = string.Empty;
    public string CounterName { get; set; } = string.Empty;
    public string InstanceName { get; set; } = string.Empty;
    public string DisplayFormat { get; set; } = "{0}";
    public bool UseSum { get; set; }
}
