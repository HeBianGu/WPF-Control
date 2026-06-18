// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace H.Extensions.Computer.ComputerManagementObjects;

public interface IManagementObjectPresenter
{
    ObservableCollection<IPropertyDataInfo> PropertyDataInfos { get; set; }
}

public abstract class ManagementObjectPresenterBase : DisplayBindableBase, IManagementObjectPresenter
{
    protected ManagementObjectPresenterBase()
    {
        this.PropertyDataInfos = new ObservableCollection<IPropertyDataInfo>(this.GetPropertyDataInfos());
    }
    private ObservableCollection<IPropertyDataInfo> _PropertyDataInfos = new ObservableCollection<IPropertyDataInfo>();
    public ObservableCollection<IPropertyDataInfo> PropertyDataInfos
    {
        get { return _PropertyDataInfos; }
        set
        {
            _PropertyDataInfos = value;
            RaisePropertyChanged();
        }
    }

    protected abstract IEnumerable<IPropertyDataInfo> GetPropertyDataInfos();
}

public abstract class ManagementPathManagementObjectPresenterBase : ManagementObjectPresenterBase
{
    protected ManagementPathManagementObjectPresenterBase()
    {
        var attribute = this.GetType().GetCustomAttribute<ManagementPathKeyAttribute>();
        if (attribute != null)
        {
            this.ManagementPathKey = attribute.Key;
        }
    }

    public string ManagementPathKey { get; set; }

    protected override IEnumerable<IPropertyDataInfo> GetPropertyDataInfos()
    {
        return ComputerManagementObjectManager.GetManagementClassInfos(managementClass: this.ManagementPathKey);
    }
}


[Display(Name = "主板信息", GroupName = "硬件信息", Description = "主板的详细信息", Order = 1)]
[ManagementPathKey(ManagementPathKeys.BaseBoard)]
public class BaseBoardInfoPresenter : ManagementPathManagementObjectPresenterBase
{

}

[Display(Name = "电脑系统信息", GroupName = "系统信息", Description = "电脑系统的详细信息", Order = 0)]
[ManagementPathKey(ManagementPathKeys.ComputerSystem)]
public class ComputerSystemInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "电脑型号信息", GroupName = "系统信息", Description = "电脑型号的详细信息", Order = 1)]
[ManagementPathKey(ManagementPathKeys.ComputerSystemProduct)]
public class ComputerSystemProductInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "BIOS信息", GroupName = "硬件信息", Description = "BIOS的详细信息", Order = 2)]
[ManagementPathKey(ManagementPathKeys.BIOS)]
public class BIOSInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "操作系统信息", GroupName = "系统信息", Description = "操作系统的详细信息", Order = 3)]
[ManagementPathKey(ManagementPathKeys.OperatingSystem)]
public class OperatingSystemInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "CPU信息", GroupName = "硬件信息", Description = "CPU的详细信息", Order = 4)]
[ManagementPathKey(ManagementPathKeys.Processor)]
public class CPUInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "显卡信息", GroupName = "硬件信息", Description = "显卡的详细信息", Order = 5)]
[ManagementPathKey(ManagementPathKeys.VideoController)]
public class GPUInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "声卡信息", GroupName = "硬件信息", Description = "声卡的详细信息", Order = 6)]
[ManagementPathKey(ManagementPathKeys.SoundDevice)]
public class SoundDeviceInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "磁盘驱动器信息", GroupName = "硬件信息", Description = "磁盘驱动器的详细信息", Order = 7)]
[ManagementPathKey(ManagementPathKeys.DiskDrive)]
public class DiskDriveInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "逻辑磁盘信息", GroupName = "硬件信息", Description = "逻辑磁盘的详细信息", Order = 8)]
[ManagementPathKey(ManagementPathKeys.LogicalDisk)]
public class LogicalDiskInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "网络适配器信息", GroupName = "网络信息", Description = "网络适配器的详细信息", Order = 9)]
[ManagementPathKey(ManagementPathKeys.NetworkAdapter)]
public class NetworkAdapterInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "网络适配器配置信息", GroupName = "网络信息", Description = "网络适配器配置的详细信息", Order = 10)]
[ManagementPathKey(ManagementPathKeys.NetworkAdapterConfiguration)]
public class NetworkAdapterConfigurationInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "电池信息", GroupName = "电源信息", Description = "电池的详细信息", Order = 11)]
[ManagementPathKey(ManagementPathKeys.Battery)]
public class BatteryInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "打印机信息", GroupName = "打印信息", Description = "打印机的详细信息", Order = 12)]
[ManagementPathKey(ManagementPathKeys.Printer)]
public class PrinterInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "进程信息", GroupName = "系统服务", Description = "进程的详细信息", Order = 13)]
[ManagementPathKey(ManagementPathKeys.Process)]
public class ProcessInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "服务信息", GroupName = "系统服务", Description = "服务的详细信息", Order = 14)]
[ManagementPathKey(ManagementPathKeys.Service)]
public class ServiceInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "共享信息", GroupName = "系统信息", Description = "共享资源的详细信息", Order = 15)]
[ManagementPathKey(ManagementPathKeys.Share)]
public class ShareInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "用户账户信息", GroupName = "用户信息", Description = "用户账户的详细信息", Order = 16)]
[ManagementPathKey(ManagementPathKeys.UserAccount)]
public class UserAccountInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "用户组信息", GroupName = "用户信息", Description = "用户组的详细信息", Order = 17)]
[ManagementPathKey(ManagementPathKeys.Group)]
public class GroupInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "启动项信息", GroupName = "系统服务", Description = "启动项的详细信息", Order = 18)]
[ManagementPathKey(ManagementPathKeys.StartupCommand)]
public class StartupCommandInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "系统补丁信息", GroupName = "系统信息", Description = "系统补丁的详细信息", Order = 19)]
[ManagementPathKey(ManagementPathKeys.QuickFixEngineering)]
public class QuickFixEngineeringInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "时区信息", GroupName = "系统信息", Description = "时区的详细信息", Order = 20)]
[ManagementPathKey(ManagementPathKeys.TimeZone)]
public class TimeZoneInfoPresenter : ManagementPathManagementObjectPresenterBase
{
}

[Display(Name = "内存信息", GroupName = "硬件信息", Description = "内存的详细信息", Order = 2)]
public class RAMInfoPresenter : ManagementObjectPresenterBase
{
    protected override IEnumerable<IPropertyDataInfo> GetPropertyDataInfos()
    {
        return ComputerManagementObjectManager.GetRAMInfos();
    }
}
