// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Management;
using System.Text;

namespace H.Extensions.Computer.ComputerManagementObjects;
public interface IPropertyDataInfo
{
    PropertyData PropertyData { get; set; }
    string Value { get; set; }
}

public class PropertyDataInfo : DisplayBindableBase, IPropertyDataInfo
{
    public string Value { get; set; }

    public PropertyData PropertyData { get; set; }
}

public class ManagementPathInfoPresenter : ManagementObjectPresenterBase
{
    public ManagementPathInfoPresenter(ManagementPathInfo info, bool loadData = false)
    {
        this.Name = info.Name;
        this.ShortName = info.Name;
        this.GroupName = info.GroupName;
        this.Description = info.Description;
        this.Order = info.Order;
        this.ManagementPathKey = info.Key;

        this.PropertyDataInfos = new ObservableCollection<IPropertyDataInfo>(this.GetPropertyDataInfos());
    }

    public string ManagementPathKey { get; set; }

    protected override IEnumerable<IPropertyDataInfo> GetPropertyDataInfos()
    {
        return ComputerManagementObjectManager.GetManagementClassInfos(managementClass: this.ManagementPathKey);
    }
}

public class ManagementPathInfo
{
    public ManagementPathInfo(string key, string name, string groupName, string description, int order)
    {
        this.Key = key;
        this.Name = name;
        this.GroupName = groupName;
        this.Description = description;
        this.Order = order;
    }

    public string Key { get; }

    public string Name { get; }

    public string GroupName { get; }

    public string Description { get; }

    public int Order { get; }
}

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


[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class ManagementPathKeyAttribute : Attribute
{
    readonly string _key;

    // This is a positional argument
    public ManagementPathKeyAttribute(string key)
    {
        this._key = key;
    }

    public string Key
    {
        get { return _key; }
    }
}

public class ManagementPathKeys
{
    /// <summary>
    /// 电脑系统信息
    /// </summary>
    public const string ComputerSystem = "Win32_ComputerSystem";

    /// <summary>
    /// 主板信息
    /// </summary>
    public const string BaseBoard = "Win32_BaseBoard";

    /// <summary>
    /// 电脑型号
    /// </summary>
    public const string ComputerSystemProduct = "Win32_ComputerSystemProduct";

    /// <summary>
    /// BIOS信息
    /// </summary>
    public const string BIOS = "Win32_BIOS";

    /// <summary>
    /// 操作系统信息
    /// </summary>
    public const string OperatingSystem = "Win32_OperatingSystem";

    /// <summary>
    /// CPU信息
    /// </summary>
    public const string Processor = "Win32_Processor";

    /// <summary>
    /// 物理内存信息
    /// </summary>
    public const string PhysicalMemory = "Win32_PhysicalMemory";

    /// <summary>
    /// 显卡信息
    /// </summary>
    public const string VideoController = "Win32_VideoController";

    /// <summary>
    /// 声卡信息
    /// </summary>
    public const string SoundDevice = "Win32_SoundDevice";

    /// <summary>
    /// 磁盘驱动器信息
    /// </summary>
    public const string DiskDrive = "Win32_DiskDrive";

    /// <summary>
    /// 逻辑磁盘信息
    /// </summary>
    public const string LogicalDisk = "Win32_LogicalDisk";

    /// <summary>
    /// 网络适配器信息
    /// </summary>
    public const string NetworkAdapter = "Win32_NetworkAdapter";

    /// <summary>
    /// 网络适配器配置信息
    /// </summary>
    public const string NetworkAdapterConfiguration = "Win32_NetworkAdapterConfiguration";

    /// <summary>
    /// 电池信息
    /// </summary>
    public const string Battery = "Win32_Battery";

    /// <summary>
    /// 打印机信息
    /// </summary>
    public const string Printer = "Win32_Printer";

    /// <summary>
    /// 进程信息
    /// </summary>
    public const string Process = "Win32_Process";

    /// <summary>
    /// 服务信息
    /// </summary>
    public const string Service = "Win32_Service";

    /// <summary>
    /// 共享信息
    /// </summary>
    public const string Share = "Win32_Share";

    /// <summary>
    /// 用户账户信息
    /// </summary>
    public const string UserAccount = "Win32_UserAccount";

    /// <summary>
    /// 用户组信息
    /// </summary>
    public const string Group = "Win32_Group";

    /// <summary>
    /// 启动项信息
    /// </summary>
    public const string StartupCommand = "Win32_StartupCommand";

    /// <summary>
    /// 系统补丁信息
    /// </summary>
    public const string QuickFixEngineering = "Win32_QuickFixEngineering";

    /// <summary>
    /// 时区信息
    /// </summary>
    public const string TimeZone = "Win32_TimeZone";

    public static IEnumerable<ManagementPathInfo> GetManagementPathInfos()
    {
        int order = 0;
        foreach (string key in new ManagementPathKeys().GetWin32Keys().Distinct())
        {
            string name = GetDisplayName(key);
            yield return new ManagementPathInfo(key, name, GetGroupName(key), $"{name}的详细信息", order++);
        }
    }

    public static IEnumerable<ManagementObjectPresenterBase> CreatePresenters(bool loadData = false)
    {
        return GetManagementPathInfos().Select(x => new ManagementPathInfoPresenter(x, loadData));
    }

    private static string GetDisplayName(string key)
    {
        return key switch
        {
            ComputerSystem => "电脑系统信息",
            ComputerSystemProduct => "电脑型号信息",
            BaseBoard => "主板信息",
            BIOS => "BIOS信息",
            OperatingSystem => "操作系统信息",
            Processor => "CPU信息",
            PhysicalMemory => "物理内存信息",
            VideoController => "显卡信息",
            SoundDevice => "声卡信息",
            DiskDrive => "磁盘驱动器信息",
            LogicalDisk => "逻辑磁盘信息",
            NetworkAdapter => "网络适配器信息",
            NetworkAdapterConfiguration => "网络适配器配置信息",
            Battery => "电池信息",
            Printer => "打印机信息",
            Process => "进程信息",
            Service => "服务信息",
            Share => "共享信息",
            UserAccount => "用户账户信息",
            Group => "用户组信息",
            StartupCommand => "启动项信息",
            QuickFixEngineering => "系统补丁信息",
            TimeZone => "时区信息",
            _ => key.Replace("Win32_", string.Empty)
        };
    }

    private static string GetGroupName(string key)
    {
        if (key.Contains("Processor") || key.Contains("Memory") || key.Contains("BaseBoard") || key.Contains("BIOS") || key.Contains("Disk") || key.Contains("Video") || key.Contains("Sound") || key.Contains("USB"))
            return "硬件信息";

        if (key.Contains("Network") || key.Contains("TCP") || key.Contains("IP") || key.Contains("Route"))
            return "网络信息";

        if (key.Contains("Battery") || key.Contains("Power") || key.Contains("Voltage"))
            return "电源信息";

        if (key.Contains("Printer") || key.Contains("Print"))
            return "打印信息";

        if (key.Contains("User") || key.Contains("Group") || key.Contains("Account") || key.Contains("Logon"))
            return "用户信息";

        if (key.Contains("Process") || key.Contains("Service") || key.Contains("Driver") || key.Contains("Startup"))
            return "系统服务";

        if (key.Contains("Perf"))
            return "性能计数";

        if (key.Contains("Product") || key.Contains("Software") || key.Contains("Application"))
            return "软件信息";

        return "系统信息";
    }


    public IEnumerable<string> GetWin32Keys()
    {
        //"    冷却类别
        yield return "Win32_Fan";//风扇
        yield return "Win32_HeatPipe";//热管
        yield return "Win32_Refrigeration";//致冷
        yield return "Win32_TemperatureProbe";//温度传感
                                              //"输入设备类别
        yield return "Win32_Keyboard";//键盘
        yield return "Win32_PointingDevice";//指示设备（如鼠标）　
                                            //"大容量存储类别
        yield return "Win32_AutochkSetting";//磁盘自动检查操作设置
        yield return "Win32_CDROMDrive";//光盘驱动器
        yield return "Win32_DiskDrive";//硬盘驱动器
        yield return "Win32_FloppyDrive";//软盘驱动器
        yield return "Win32_PhysicalMedia";//物理媒体
        yield return "Win32_TapeDrive";//磁带驱动器
                                       //"主板、控制器、端口类别
        yield return "Win32_1394Controller";//1394控制器
        yield return "Win32_1394ControllerDevice";//1394控制器设备
        yield return "Win32_AllocatedResource";//已分配的资源
        yield return "Win32_AssociatedProcessorMemory";//处理器和高速缓冲存储器
        yield return "Win32_BaseBoard";//主板
        yield return "Win32_BIOS";//BIOS（基本输入输出系统）
        yield return "Win32_Bus";//总线
        yield return "Win32_CacheMemory";//缓存内存
        yield return "Win32_ControllerHasHub";//USB控制器
        yield return "Win32_DeviceBus";//设备总线
        yield return "Win32_DeviceMemoryAddress";//设备存储器地址
        yield return "Win32_DeviceSettings";//设备设置
        yield return "Win32_DMAChannel";//DMA通道
        yield return "Win32_FloppyController";//软盘控制器
        yield return "Win32_IDEController";//IDE控制器
        yield return "Win32_IDEControllerDevice";//IDE控制器设备
        yield return "Win32_InfraredDevice";//红外线设备
        yield return "Win32_IRQResource";//中断（IRQ）资源
        yield return "Win32_MemoryArray";//内存数组
        yield return "Win32_MemoryArrayLocation";//内存数组位置
        yield return "Win32_MemoryDevice";//内存设备
        yield return "Win32_MemoryDeviceArray";//内存设备数组
        yield return "Win32_MemoryDeviceLocation";//内存设备位置
        yield return "Win32_MotherboardDevice";//主板设备
        yield return "Win32_OnBoardDevice";//插件设备
        yield return "Win32_ParallelPort";//并行端口
        yield return "Win32_PCMCIAController";//PCMCIA控制器
        yield return "Win32_PhysicalMemory";//物理内存
        yield return "Win32_PhysicalMemoryArray";//物理内存数组
        yield return "Win32_PhysicalMemoryLocation";//物理内存位置
        yield return "Win32_PNPAllocatedResource";//PNP保留资源
        yield return "Win32_PNPDevice";//PNP设备
        yield return "Win32_PNPEntity";//PNP实体
        yield return "Win32_PortConnector";//端口连接器
        yield return "Win32_PortResource";//端口资源
        yield return "Win32_Processor";//（CPU）处理器
        yield return "Win32_SCSIController";//SCSI控制器
        yield return "Win32_SCSIControllerDevice";//SCSI控制器设备
        yield return "Win32_SerialPort";//串行端口
        yield return "Win32_SerialPortConfiguration";//串行端口配置
        yield return "Win32_SerialPortSetting";//串行端口设置
        yield return "Win32_SMBIOSMemory";//内存有关的设备的管理
        yield return "Win32_SoundDevice";//声卡
        yield return "Win32_SystemBIOS";//系统BIOS
        yield return "Win32_SystemDriverPNPEntity";//系统驱动器PNP实体
        yield return "Win32_SystemEnclosure";//系统封闭
        yield return "Win32_SystemMemoryResource";//系统内存资源
        yield return "Win32_SystemSlot";//系统插槽
        yield return "Win32_USBController";//USB控制器
        yield return "Win32_USBControllerDevice";//USB控制器设备
        yield return "Win32_USBHub";//USB集线器


        //"建网设备类别
        yield return "Win32_NetworkAdapter";//网络适配器
        yield return "Win32_NetworkAdapterConfiguration";//网络适配器配置
        yield return "Win32_NetworkAdapterSetting";//网络适配器设置


        //"电源类别
        yield return "Win32_AssociatedBattery";//联合电池组
        yield return "Win32_Battery";//电池
        yield return "Win32_CurrentProbe";//当前传感
        yield return "Win32_PortableBattery";//便携式电池
        yield return "Win32_PowerManagementEvent";//电池事件管理
        yield return "Win32_UninterruptiblePowerSupply";//UPS电源
        yield return "Win32_VoltageProbe";//电压探测


        //"打印类别
        yield return "Win32_DriverForDevice";//驱动器设备
        yield return "Win32_Printer";//打印机
        yield return "Win32_PrinterConfiguration";//打印机配置
        yield return "Win32_PrinterController";//打印机控制器
        yield return "Win32_PrinterDriver";//打印机驱动器
        yield return "Win32_PrinterDriverDll";//打印机驱动器DLL
        yield return "Win32_PrinterSetting";//打印机设置
        yield return "Win32_PrintJob";//打印工作
        yield return "Win32_TCPIPPrinterPort";//TCPIP打印机端口


        //"电话类别
        yield return "Win32_POTSModem";//POTS调制解调器（Modem）
        yield return "Win32_POTSModemToSerialPort";//POTS调制解调器串行端口


        //"视频监视器类别
        yield return "Win32_DesktopMonitor";//即插即用监视器
        yield return "Win32_DisplayConfiguration";//显示配置
        yield return "Win32_DisplayControllerConfiguration";//显示控制器配置
        yield return "Win32_VideoConfiguration";//视频配置
        yield return "Win32_VideoController";//视频控制器
        yield return "Win32_VideoSettings";//视频设置


        //"2、操作系统类
        //"COM类别
        yield return "Win32_ClassicCOMApplicationClasses";//
        yield return "Win32_ClassicCOMClass";//
        yield return "Win32_ClassicCOMClassSettings";//
        yield return "Win32_ClientApplicationSetting";// 
        yield return "Win32_COMApplication";//COM应用
        yield return "Win32_COMApplicationClasses";//
        yield return "Win32_COMApplicationSettings";//
        yield return "Win32_COMClass";//
        yield return "Win32_ComClassAutoEmulator";//
        yield return "Win32_ComClassEmulator";//
        yield return "Win32_ComponentCategory";//
        yield return "Win32_COMSetting";//
        yield return "Win32_DCOMApplication";//DCOM应用
        yield return "Win32_DCOMApplicationAccessAllowedSetting";//
        yield return "Win32_DCOMApplicationSetting";//
        yield return "Win32_ImplementedCategory";//


        //"桌面类别
        yield return "Win32_Desktop";//桌面
        yield return "Win32_Environment";//环境
        yield return "Win32_TimeZone";//时区
        yield return "Win32_UserDesktop";//使用者桌面


        //"驱动程序类别
        yield return "Win32_DriverVXD";//
        yield return "Win32_SystemDriver";//系统驱动程序


        //"文件系统类别
        yield return "Win32_CIMLogicalDeviceCIMDataFile";//
        yield return "Win32_Directory";//
        yield return "Win32_DirectorySpecification";//
        yield return "Win32_DiskDriveToDiskPartition";//
        yield return "Win32_DiskPartition";//磁盘逻辑分区
        yield return "Win32_DiskQuota";//NTFS磁盘分区定额
        yield return "Win32_LogicalDisk";//逻辑磁盘分区
        yield return "Win32_LogicalDiskRootDirectory";//
        yield return "Win32_LogicalDiskToPartition";//
        yield return "Win32_MappedLogicalDisk";//映射逻辑磁盘
        yield return "Win32_OperatingSystemAutochkSetting";//
        yield return "Win32_QuotaSetting";//
        yield return "Win32_ShortcutFile";//
        yield return "Win32_SubDirectory";//
        yield return "Win32_SystemPartitions";//
        yield return "Win32_Volume";// 
        yield return "Win32_VolumeQuota";// 
        yield return "Win32_VolumeQuotaSetting";//
        yield return "Win32_VolumeUserQuota";//


        //"作业对象类别
        yield return "Win32_CollectionStatistics";//
        yield return "Win32_LUID";//
        yield return "Win32_LUIDandAttributes";//
        yield return "Win32_NamedJobObject";//
        yield return "Win32_NamedJobObjectActgInfo";//
        yield return "Win32_NamedJobObjectLimit";//
        yield return "Win32_NamedJobObjectLimitSetting";//
        yield return "Win32_NamedJobObjectProcess";//
        yield return "Win32_NamedJobObjectSecLimit";//
        yield return "Win32_NamedJobObjectSecLimitSetting";//
        yield return "Win32_NamedJobObjectStatistics";//
        yield return "Win32_SIDandAttributes";//
        yield return "Win32_TokenGroups";//
        yield return "Win32_TokenPrivileges";//


        //"存储页面文件类别
        yield return "Win32_LogicalMemoryConfiguration";//逻辑内存配置
        yield return "Win32_PageFile";//页面文件
        yield return "Win32_PageFileElementSetting";//
        yield return "Win32_PageFileSetting";//页面文件设置
        yield return "Win32_PageFileUsage";//页面文件使用
        yield return "Win32_SystemLogicalMemoryConfiguration";//


        //"多媒体视听类别
        yield return "Win32_CodecFile";//编解码器文件


        //"建网类别
        yield return "Win32_ActiveRoute";//活动路由
        yield return "Win32_IP4PersistedRouteTable";//
        yield return "Win32_IP4RouteTable";//路由表
        yield return "Win32_IP4RouteTableEvent";//
        yield return "Win32_NetworkClient";//
        yield return "Win32_NetworkConnection";//
        yield return "Win32_NetworkProtocol";//网络协议
        yield return "Win32_NTDomain";//
        yield return "Win32_PingStatus";//
        yield return "Win32_ProtocolBinding";//协议绑定


        //"操作系统事件类别
        yield return "Win32_ComputerShutdownEvent";//
        yield return "Win32_ComputerSystemEvent";//
        yield return "Win32_DeviceChangeEvent";//
        yield return "Win32_ModuleLoadTrace";//
        yield return "Win32_ModuleTrace";//
        yield return "Win32_ProcessStartTrace";//
        yield return "Win32_ProcessStopTrace";//
        yield return "Win32_ProcessTrace";//
        yield return "Win32_SystemConfigurationChangeEvent";//
        yield return "Win32_SystemTrace";//
        yield return "Win32_ThreadStartTrace";//
        yield return "Win32_ThreadStopTrace";//
        yield return "Win32_ThreadTrace";//
        yield return "Win32_VolumeChangeEvent";//


        yield return "Win32_BootConfiguration";//引导配置
        yield return "Win32_ComputerSystem";//计算机系统
        yield return "Win32_ComputerSystemProcessor";//计算机系统处理器
        yield return "Win32_ComputerSystemProduct";//计算机系统产品
        yield return "Win32_DependentService";//信任的服务
        yield return "Win32_LoadOrderGroup";//装载顺序组
        yield return "Win32_LoadOrderGroupServiceDependencies";//
        yield return "Win32_LoadOrderGroupServiceMembers";//
        yield return "Win32_OperatingSystem";//操作系统
        yield return "Win32_OperatingSystemQFE";//
        yield return "Win32_OSRecoveryConfiguration";//操作系统恢复配置
        yield return "Win32_QuickFixEngineering";//
        yield return "Win32_StartupCommand";//启动命令
        yield return "Win32_SystemBootConfiguration";//
        yield return "Win32_SystemDesktop";//
        yield return "Win32_SystemDevices";//
        yield return "Win32_SystemLoadOrderGroups";//
        yield return "Win32_SystemNetworkConnections";//
        yield return "Win32_SystemOperatingSystem";//
        yield return "Win32_SystemProcesses";//
        yield return "Win32_SystemProgramGroups";//Windows开始程序组
        yield return "Win32_SystemResources";//
        yield return "Win32_SystemServices";//系统服务
        yield return "Win32_SystemSetting";//
        yield return "Win32_SystemSystemDriver";//
        yield return "Win32_SystemTimeZone";//系统时区
        yield return "Win32_SystemUsers";//系统用户


        //"进程类别
        yield return "Win32_Process";//进程
        yield return "Win32_ProcessStartup";//
        yield return "Win32_Thread";//线程


        //"注册类别
        yield return "Win32_Registry";//注册表
                                      //"调试程序作业类别
        yield return "Win32_CurrentTime";//当前时间
        yield return "Win32_ScheduledJob";//


        // "安全类别
        yield return "Win32_AccountSID";//
        yield return "Win32_ACE";//
        yield return "Win32_LogicalFileAccess";//
        yield return "Win32_LogicalFileAuditing";//
        yield return "Win32_LogicalFileGroup";//
        yield return "Win32_LogicalFileOwner";//
        yield return "Win32_LogicalFileSecuritySetting";//
        yield return "Win32_LogicalShareAccess";//
        yield return "Win32_LogicalShareAuditing";//
        yield return "Win32_LogicalShareSecuritySetting";//
        yield return "Win32_PrivilegesStatus";//
        yield return "Win32_SecurityDescriptor";//
        yield return "Win32_SecuritySetting";//
        yield return "Win32_SecuritySettingAccess";//
        yield return "Win32_SecuritySettingAuditing";//
        yield return "Win32_SecuritySettingGroup";//
        yield return "Win32_SecuritySettingOfLogicalFile";//
        yield return "Win32_SecuritySettingOfLogicalShare";//
        yield return "Win32_SecuritySettingOfObject";//
        yield return "Win32_SecuritySettingOwner";//
        yield return "Win32_SID";//
        yield return "Win32_Trustee";//
                                     //"服务类别
        yield return "Win32_BaseService";//基本服务
        yield return "Win32_Service";//服务


        //"共享类别
        yield return "Win32_DFSNode";//
        yield return "Win32_DFSNodeTarget";//
        yield return "Win32_DFSTarget";//
        yield return "Win32_ServerConnection";//
        yield return "Win32_ServerSession";//
        yield return "Win32_ConnectionShare";//
        yield return "Win32_PrinterShare";//
        yield return "Win32_SessionConnection";//
        yield return "Win32_SessionProcess";//
        yield return "Win32_ShareToDirectory";//
        yield return "Win32_Share";//共享文件夹


        //"开始菜单类别
        yield return "Win32_LogicalProgramGroup";//Windows开始逻辑程序组
        yield return "Win32_LogicalProgramGroupDirectory";//Windows开始逻辑程序组目录Win32_LogicalProgramGroupItem";//Windows开始逻辑程序组项
        yield return "Win32_LogicalProgramGroupItemDataFile";//Windows开始逻辑程序组项数据文件
        yield return "Win32_ProgramGroup";//Windows程序组
        yield return "Win32_ProgramGroupContents";//Windows程序组内容
        yield return "Win32_ProgramGroupOrItem";//Windows程序组或项
                                                //"存储类别
        yield return "Win32_ShadowBy";//
        yield return "Win32_ShadowContext";//
        yield return "Win32_ShadowCopy";//
        yield return "Win32_ShadowDiffVolumeSupport";//
        yield return "Win32_ShadowFor";//
        yield return "Win32_ShadowOn";//
        yield return "Win32_ShadowProvider";//
        yield return "Win32_ShadowStorage";//
        yield return "Win32_ShadowVolumeSupport";//
        yield return "Win32_Volume";//
        yield return "Win32_VolumeUserQuota";//


        //"用户类别
        yield return "Win32_Account";//帐户
        yield return "Win32_Group";//组
        yield return "Win32_GroupInDomain";//域中的组
        yield return "Win32_GroupUser";//组用户
        yield return "Win32_LogonSession";//登录会话
        yield return "Win32_LogonSessionMappedDisk";//
        yield return "Win32_NetworkLoginProfile";//
        yield return "Win32_SystemAccount";//系统账户
        yield return "Win32_UserAccount";//使用账户
        yield return "Win32_UserInDomain";//域中的用户
                                          //"Windows NT的事件日志类别
        yield return "Win32_NTEventlogFile";//事件日志文件
        yield return "Win32_NTLogEvent";//日志事件
        yield return "Win32_NTLogEventComputer";//日志事件计算机
        yield return "Win32_NTLogEventLog";//日志事件日志
        yield return "Win32_NTLogEventUser";//


        //"Windows产品激活类别
        yield return "Win32_ComputerSystemWindowsProductActivationSetting";//
        yield return "Win32_Proxy";//代理
        yield return "Win32_WindowsProductActivation";//Windows产品激活


        //"3、安装应用程序类
        yield return "Win32_ActionCheck";//
        yield return "Win32_ApplicationCommandLine";//
        yield return "Win32_ApplicationService";//
        yield return "Win32_Binary";//
        yield return "Win32_BindImageAction";//
        yield return "Win32_CheckCheck";//
        yield return "Win32_ClassInfoAction";//
        yield return "Win32_CommandLineAccess";//
        yield return "Win32_Condition";//
        yield return "Win32_CreateFolderAction";//
        yield return "Win32_DuplicateFileAction";//
        yield return "Win32_EnvironmentSpecification";//
        yield return "Win32_ExtensionInfoAction";//
        yield return "Win32_FileSpecification";//
        yield return "Win32_FontInfoAction";//
        yield return "Win32_IniFileSpecification";//
        yield return "Win32_InstalledSoftwareElement";//
        yield return "Win32_LaunchCondition";//
        yield return "Win32_ManagedSystemElementResource";//
        yield return "Win32_MIMEInfoAction";//
        yield return "Win32_MoveFileAction";//
        yield return "Win32_MSIResource";//
        yield return "Win32_ODBCAttribute";//
        yield return "Win32_ODBCDataSourceAttribute";//
        yield return "Win32_ODBCDataSourceSpecification";//
        yield return "Win32_ODBCDriverAttribute";//
        yield return "Win32_ODBCDriverSoftwareElement";//
        yield return "Win32_ODBCDriverSpecification";//
        yield return "Win32_ODBCSourceAttribute";//
        yield return "Win32_ODBCTranslatorSpecification";//
        yield return "Win32_Patch";//
        yield return "Win32_PatchFile";//
        yield return "Win32_PatchPackage";//
        yield return "Win32_Product";//
        yield return "Win32_ProductCheck";//
        yield return "Win32_ProductResource";//
        yield return "Win32_ProductSoftwareFeatures";//
        yield return "Win32_ProgIDSpecification";//
        yield return "Win32_Property";//
        yield return "Win32_PublishComponentAction";//
        yield return "Win32_RegistryAction";//
        yield return "Win32_RemoveFileAction";//
        yield return "Win32_RemoveIniAction";//
        yield return "Win32_ReserveCost";//
        yield return "Win32_SelfRegModuleAction";//
        yield return "Win32_ServiceControl";//
        yield return "Win32_ServiceSpecification";//
        yield return "Win32_ServiceSpecificationService";//
        yield return "Win32_SettingCheck";//
        yield return "Win32_ShortcutAction";//
        yield return "Win32_ShortcutSAP";//
        yield return "Win32_SoftwareElement";//
        yield return "Win32_SoftwareElementAction";//
        yield return "Win32_SoftwareElementCheck";//
        yield return "Win32_SoftwareElementCondition";//
        yield return "Win32_SoftwareElementResource";//
        yield return "Win32_SoftwareFeature";//
        yield return "Win32_SoftwareFeatureAction";//
        yield return "Win32_SoftwareFeatureCheck";//
        yield return "Win32_SoftwareFeatureParent";//
        yield return "Win32_SoftwareFeatureSoftwareElements";//
        yield return "Win32_TypeLibraryAction";//
                                               //"4、WMI服务管理类
                                               //"WMI配置类别
        yield return "Win32_MethodParameterClass";//方法参数类
                                                  //"WMI管理类别
        yield return "Win32_WMISetting";//WMI设置
        yield return "Win32_WMIElementSetting";//WMI单元设置


        //"5、性能计数器类
        //"格式化性能计数器类别
        yield return "Win32_PerfFormattedData";//
        yield return "Win32_PerfFormattedData_ASP_ActiveServerPages";//
        yield return "Win32_PerfFormattedData_ContentFilter_IndexingServiceFilter";//
        yield return "Win32_PerfFormattedData_ContentIndex_IndexingService";//
        yield return "Win32_PerfFormattedData_InetInfo_InternetInformationServicesGlobal";//
        yield return "Win32_PerfFormattedData_ISAPISearch_HttpIndexingService";//
        yield return "Win32_PerfFormattedData_MSDTC_DistributedTransactionCoordinator";//
        yield return "Win32_PerfFormattedData_NTFSDRV_SMTPNTFSStoreDriver";//
        yield return "Win32_PerfFormattedData_PerfDisk_LogicalDisk";//
        yield return "Win32_PerfFormattedData_PerfDisk_PhysicalDisk";//
        yield return "Win32_PerfFormattedData_PerfNet_Browser";//
        yield return "Win32_PerfFormattedData_PerfNet_Redirector";//
        yield return "Win32_PerfFormattedData_PerfNet_Server";//
        yield return "Win32_PerfFormattedData_PerfNet_ServerWorkQueues";//
        yield return "Win32_PerfFormattedData_PerfOS_Cache";//
        yield return "Win32_PerfFormattedData_PerfOS_Memory";//
        yield return "Win32_PerfFormattedData_PerfOS_Objects";//
        yield return "Win32_PerfFormattedData_PerfOS_PagingFile";//
        yield return "Win32_PerfFormattedData_PerfOS_Processor";//
        yield return "Win32_PerfFormattedData_PerfOS_System";//
        yield return "Win32_PerfFormattedData_PerfProc_FullImage_Costly";//
        yield return "Win32_PerfFormattedData_PerfProc_Image_Costly";//Win32_PerfFormattedData_PerfProc_JobObject";//Win32_PerfFormattedData_PerfProc_JobObjectDetails";//Win32_PerfFormattedData_PerfProc_Process";//Win32_PerfFormattedData_PerfProc_ProcessAddressSpace_Costly";//Win32_PerfFormattedData_PerfProc_Thread";//Win32_PerfFormattedData_PerfProc_ThreadDetails_Costly";//Win32_PerfFormattedData_PSched_PSchedFlow";//
        yield return "Win32_PerfFormattedData_PSched_PSchedPipe";//Win32_PerfFormattedData_RemoteAccess_RASPort";//Win32_PerfFormattedData_RemoteAccess_RASTotal";//Win32_PerfFormattedData_RSVP_ACSRSVPInterfaces";//Win32_PerfFormattedData_RSVP_ACSRSVPService";//Win32_PerfFormattedData_SMTPSVC_SMTPServer";//Win32_PerfFormattedData_Spooler_PrintQueue";//
        yield return "Win32_PerfFormattedData_TapiSrv_Telephony";//
        yield return "Win32_PerfFormattedData_Tcpip_ICMP";//
        yield return "Win32_PerfFormattedData_Tcpip_IP";//
        yield return "Win32_PerfFormattedData_Tcpip_NBTConnection";//Win32_PerfFormattedData_Tcpip_NetworkInterface";//
        yield return "Win32_PerfFormattedData_Tcpip_TCP";//
        yield return "Win32_PerfFormattedData_Tcpip_UDP";//Win32_PerfFormattedData_TermService_TerminalServices";//Win32_PerfFormattedData_TermService_TerminalServicesSession";//Win32_PerfFormattedData_W3SVC_WebService";//


        //"原始性能计数器类别
        yield return "Win32_PerfRawData";//Win32_PerfRawData_ASP_ActiveServerPages";//Win32_PerfRawData_ContentFilter_IndexingServiceFilter";//Win32_PerfRawData_ContentIndex_IndexingService";//Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal";//Win32_PerfRawData_ISAPISearch_HttpIndexingService";//Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator";//Win32_PerfRawData_NTFSDRV_SMTPNTFSStoreDriver";//
        yield return "Win32_PerfRawData_PerfDisk_LogicalDisk";//
        yield return "Win32_PerfRawData_PerfDisk_PhysicalDisk";//
        yield return "Win32_PerfRawData_PerfNet_Browser";//
        yield return "Win32_PerfRawData_PerfNet_Redirector";//
        yield return "Win32_PerfRawData_PerfNet_Server";//
        yield return "Win32_PerfRawData_PerfNet_ServerWorkQueues";//
        yield return "Win32_PerfRawData_PerfOS_Cache";//
        yield return "Win32_PerfRawData_PerfOS_Memory";//
        yield return "Win32_PerfRawData_PerfOS_Objects";//
        yield return "Win32_PerfRawData_PerfOS_PagingFile";//
        yield return "Win32_PerfRawData_PerfOS_Processor";//
        yield return "Win32_PerfRawData_PerfOS_System";//
        yield return "Win32_PerfRawData_PerfProc_FullImage_Costly";//
        yield return "Win32_PerfRawData_PerfProc_Image_Costly";//
        yield return "Win32_PerfRawData_PerfProc_JobObject";//
        yield return "Win32_PerfRawData_PerfProc_JobObjectDetails";//
        yield return "Win32_PerfRawData_PerfProc_Process";//Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly";//Win32_PerfRawData_PerfProc_Thread";//
        yield return "Win32_PerfRawData_PerfProc_ThreadDetails_Costly";//
        yield return "Win32_PerfRawData_PSched_PSchedFlow";//
        yield return "Win32_PerfRawData_PSched_PSchedPipe";//
        yield return "Win32_PerfRawData_RemoteAccess_RASPort";//
        yield return "Win32_PerfRawData_RemoteAccess_RASTotal";//
        yield return "Win32_PerfRawData_RSVP_ACSRSVPInterfaces";//
        yield return "Win32_PerfRawData_RSVP_ACSRSVPService";//
        yield return "Win32_PerfRawData_SMTPSVC_SMTPServer";//
        yield return "Win32_PerfRawData_Spooler_PrintQueue";//
        yield return "Win32_PerfRawData_TapiSrv_Telephony";//
        yield return "Win32_PerfRawData_Tcpip_ICMP";//
        yield return "Win32_PerfRawData_Tcpip_IP";//
        yield return "Win32_PerfRawData_Tcpip_NBTConnection";//
        yield return "Win32_PerfRawData_Tcpip_NetworkInterface";//
        yield return "Win32_PerfRawData_Tcpip_TCP";//
        yield return "Win32_PerfRawData_Tcpip_UDP";//
        yield return "Win32_PerfRawData_TermService_TerminalServices";//Win32_PerfRawData_TermService_TerminalServicesSession";//Win32_PerfRawData_W3SVC_WebService
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
