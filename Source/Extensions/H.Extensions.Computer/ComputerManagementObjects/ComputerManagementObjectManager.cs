// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;
using System.Management;
using System.Text;

namespace H.Extensions.Computer.ComputerManagementObjects;

/// <summary>
/// 电脑系统管理对象
/// </summary>
public static class ComputerManagementObjectManager
{
    public static IEnumerable<PropertyDataInfo> GetVersionInfos(string name = "电脑型号")
    {
        return GetManagementClassInfos(name, "Win32_ComputerSystemProduct");
    }

    public static IEnumerable<PropertyDataInfo> GetBaseBoardInfos(string name = "主板信息")
    {
        return GetManagementClassInfos(name, "Win32_BaseBoard");
    }


    public static IEnumerable<PropertyDataInfo> GetCPUInfos(string name = "主板信息")
    {
        return GetManagementClassInfos(name, "Win32_Processor");
    }

    public static IEnumerable<PropertyDataInfo> GetRAMInfos(string name = "内存信息")
    {
        StringBuilder ram = new StringBuilder();
        ManagementObjectCollection.ManagementObjectEnumerator searcher = new ManagementObjectSearcher()
        {
            Query = new SelectQuery("Win32_PhysicalMemory"),
        }.Get().GetEnumerator();

        while (searcher.MoveNext())
        {
            ManagementBaseObject baseObj = searcher.Current;
            foreach (PropertyData item in baseObj.Properties)
            {
                yield return new PropertyDataInfo()
                {
                    Name = item.Name,
                    ShortName = item.Name,
                    Value = FormatValue(item.Value),
                    PropertyData = item,
                    Description = FormatValue(item.Value),
                    GroupName = item.Origin
                };
            }
        }

        searcher = new ManagementObjectSearcher()
        {
            Query = new SelectQuery("Win32_PerfRawData_PerfOS_Memory"),
        }.Get().GetEnumerator();

        while (searcher.MoveNext())
        {
            ManagementBaseObject baseObj = searcher.Current;
            foreach (PropertyData item in baseObj.Properties)
            {
                yield return new PropertyDataInfo()
                {
                    Name = item.Name,
                    ShortName = item.Name,
                    Value = FormatValue(item.Value),
                    PropertyData = item,
                    Description = FormatValue(item.Value),
                    GroupName = item.Origin
                };
            }
        }
    }

    public static IEnumerable<PropertyDataInfo> GetManagementClassInfos(string name = "系统信息", string managementClass = "Win32_ComputerSystemProduct")
    {
        ManagementObjectCollection moc = new ManagementClass(managementClass).GetInstances();
        foreach (ManagementObject mo in moc)
        {
            foreach (PropertyData item in mo.Properties)
            {
                yield return new PropertyDataInfo()
                {
                    Name = item.Name,
                    ShortName = item.Name,
                    Value = FormatValue(item.Value),
                    PropertyData = item,
                    Description = FormatValue(item.Value),
                    GroupName = item.Origin
                };
            }
        }
    }

    public static IEnumerable<PropertyData> GetManagementClassPropertyDatas(string name = "系统信息", string managementClass = "Win32_ComputerSystemProduct")
    {
        StringBuilder baseBoard = new StringBuilder();
        ManagementObjectCollection moc = new ManagementClass(managementClass).GetInstances();
        foreach (ManagementObject mo in moc)
        {
            foreach (PropertyData item in mo.Properties)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<ManagementObject> GetManagementObjects(string name = "系统信息", string managementClass = "Win32_ComputerSystemProduct")
    {
        ManagementObjectCollection moc = new ManagementClass(managementClass).GetInstances();
        foreach (ManagementObject mo in moc)
        {
            yield return mo;
        }
    }


    public static IEnumerable<PropertyDataInfo> GetGpuInfos(string name = "显卡信息")
    {
        return GetManagementObjectSearcherInfos(name, "select * from Win32_VideoController");
    }

    public static IEnumerable<PropertyDataInfo> GetManagementObjectSearcherInfos(string name = "显卡信息", string managementClass = "select * from Win32_VideoController")
    {
        StringBuilder gpu = new StringBuilder();
        ManagementObjectCollection moc = new ManagementObjectSearcher(managementClass).Get();
        foreach (ManagementBaseObject mo in moc)
        {
            foreach (PropertyData item in mo.Properties)
            {
                yield return new PropertyDataInfo()
                {
                    Name = item.Name,
                    ShortName = item.Name,
                    Value = FormatValue(item.Value),
                    PropertyData = item,
                    Description = FormatValue(item.Value),
                    GroupName = item.Origin
                };

            }
        }

    }

    private static string FormatValue(object value)
    {
        if (value == null)
            return string.Empty;

        if (value is string text)
            return text;

        if (value is IEnumerable values)
            return string.Join(", ", values.Cast<object>());

        return value.ToString();
    }
}
