// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using Timer = System.Timers.Timer;

namespace H.Extensions.Computer.ComputerPerformanceCounters;

/// <summary>
/// 电脑性能计数器
/// </summary>
public class ComputerPerformanceCounter
{
    public static IEnumerable<IPerformanceCounterPresenter> CreateDefaultPresenters()
    {
        yield return new CpuCounter();
        yield return new CpuUserTimeCounter();
        yield return new CpuPrivilegedTimeCounter();
        yield return new CpuIdleTimeCounter();
        yield return new ProcessorQueueLengthCounter();
        yield return new MemoryUsageCounter();
        yield return new RamCounter();
        yield return new CommittedBytesInUseCounter();
        yield return new PagesPerSecCounter();
        yield return new ReadPhysicalDiskCounter();
        yield return new WritePhysicalDiskCounter();
        yield return new DiskReadBytesCounter();
        yield return new DiskWriteBytesCounter();
        yield return new ReceivedNetworkCounter();
        yield return new SendNetworkCounter();
        yield return new TotalNetworkCounter();
        yield return new ProcessCountCounter();
        yield return new ThreadCountCounter();
        yield return new HandleCountCounter();
    }
}

public class PerformanceCounterCategoryNames
{
    /// <summary>
    /// 缓存
    /// </summary>
    public const string Cache = "Cache";

    /// <summary>
    /// 内存
    /// </summary>
    public const string Memory = "Memory";

    /// <summary>
    /// 对象
    /// </summary>
    public const string Objects = "Objects";

    /// <summary>
    /// 物理磁盘
    /// </summary>
    public const string PhysicalDisk = "PhysicalDisk";

    /// <summary>
    /// 逻辑磁盘
    /// </summary>
    public const string LogicalDisk = "LogicalDisk";

    /// <summary>
    /// 进程
    /// </summary>
    public const string Process = "Process";

    /// <summary>
    /// 处理器
    /// </summary>
    public const string Processor = "Processor";

    /// <summary>
    /// 处理器信息
    /// </summary>
    public const string ProcessorInformation = "Processor Information";

    /// <summary>
    /// 系统
    /// </summary>
    public const string System = "System";

    /// <summary>
    /// 线程
    /// </summary>
    public const string Thread = "Thread";

    /// <summary>
    /// 网络接口
    /// </summary>
    public const string NetworkInterface = "Network Interface";

    /// <summary>
    /// 网络适配器
    /// </summary>
    public const string NetworkAdapter = "Network Adapter";

    /// <summary>
    /// IPv4
    /// </summary>
    public const string IPv4 = "IPv4";

    /// <summary>
    /// IPv6
    /// </summary>
    public const string IPv6 = "IPv6";

    /// <summary>
    /// TCPv4
    /// </summary>
    public const string TCPv4 = "TCPv4";

    /// <summary>
    /// TCPv6
    /// </summary>
    public const string TCPv6 = "TCPv6";

    /// <summary>
    /// UDPv4
    /// </summary>
    public const string UDPv4 = "UDPv4";

    /// <summary>
    /// UDPv6
    /// </summary>
    public const string UDPv6 = "UDPv6";

    /// <summary>
    /// ICMP
    /// </summary>
    public const string ICMP = "ICMP";

    /// <summary>
    /// ICMPv6
    /// </summary>
    public const string ICMPv6 = "ICMPv6";

    /// <summary>
    /// 分页文件
    /// </summary>
    public const string PagingFile = "Paging File";

    /// <summary>
    /// 打印队列
    /// </summary>
    public const string PrintQueue = "Print Queue";

    /// <summary>
    /// 浏览器
    /// </summary>
    public const string Browser = "Browser";

    /// <summary>
    /// 重定向器
    /// </summary>
    public const string Redirector = "Redirector";

    /// <summary>
    /// 服务器
    /// </summary>
    public const string Server = "Server";

    /// <summary>
    /// GPU引擎
    /// </summary>
    public const string GPUEngine = "GPU Engine";

    /// <summary>
    /// GPU适配器内存
    /// </summary>
    public const string GPUAdapterMemory = "GPU Adapter Memory";
}

public class PerformanceCounterNames
{
    /// <summary>
    /// 处理器时间百分比
    /// </summary>
    public const string ProcessorTimePercent = "% Processor Time";

    /// <summary>
    /// 用户时间百分比
    /// </summary>
    public const string UserTimePercent = "% User Time";

    /// <summary>
    /// 特权时间百分比
    /// </summary>
    public const string PrivilegedTimePercent = "% Privileged Time";

    /// <summary>
    /// 空闲时间百分比
    /// </summary>
    public const string IdleTimePercent = "% Idle Time";

    /// <summary>
    /// 中断时间百分比
    /// </summary>
    public const string InterruptTimePercent = "% Interrupt Time";

    /// <summary>
    /// DPC时间百分比
    /// </summary>
    public const string DPCTimePercent = "% DPC Time";

    /// <summary>
    /// DPC速率
    /// </summary>
    public const string DPCRate = "DPC Rate";

    /// <summary>
    /// 每秒中断数
    /// </summary>
    public const string InterruptsPerSec = "Interrupts/sec";

    /// <summary>
    /// 每秒DPC队列数
    /// </summary>
    public const string DPCsQueuedPerSec = "DPCs Queued/sec";

    /// <summary>
    /// 处理器队列长度
    /// </summary>
    public const string ProcessorQueueLength = "Processor Queue Length";

    /// <summary>
    /// 可用内存
    /// </summary>
    public const string AvailableMBytes = "Available MBytes";

    /// <summary>
    /// 已提交字节数
    /// </summary>
    public const string CommittedBytes = "Committed Bytes";

    /// <summary>
    /// 提交限制
    /// </summary>
    public const string CommitLimit = "Commit Limit";

    /// <summary>
    /// 内存提交百分比
    /// </summary>
    public const string CommittedBytesInUsePercent = "% Committed Bytes In Use";

    /// <summary>
    /// 分页缓冲池字节数
    /// </summary>
    public const string PoolPagedBytes = "Pool Paged Bytes";

    /// <summary>
    /// 非分页缓冲池字节数
    /// </summary>
    public const string PoolNonpagedBytes = "Pool Nonpaged Bytes";

    /// <summary>
    /// 每秒页面数
    /// </summary>
    public const string PagesPerSec = "Pages/sec";

    /// <summary>
    /// 每秒页面读取数
    /// </summary>
    public const string PageReadsPerSec = "Page Reads/sec";

    /// <summary>
    /// 每秒页面写入数
    /// </summary>
    public const string PageWritesPerSec = "Page Writes/sec";

    /// <summary>
    /// 磁盘时间百分比
    /// </summary>
    public const string DiskTimePercent = "% Disk Time";

    /// <summary>
    /// 磁盘读取时间百分比
    /// </summary>
    public const string DiskReadTimePercent = "% Disk Read Time";

    /// <summary>
    /// 磁盘写入时间百分比
    /// </summary>
    public const string DiskWriteTimePercent = "% Disk Write Time";

    /// <summary>
    /// 磁盘空闲时间百分比
    /// </summary>
    public const string DiskIdleTimePercent = "% Idle Time";

    /// <summary>
    /// 当前磁盘队列长度
    /// </summary>
    public const string CurrentDiskQueueLength = "Current Disk Queue Length";

    /// <summary>
    /// 平均磁盘队列长度
    /// </summary>
    public const string AvgDiskQueueLength = "Avg. Disk Queue Length";

    /// <summary>
    /// 平均磁盘读取队列长度
    /// </summary>
    public const string AvgDiskReadQueueLength = "Avg. Disk Read Queue Length";

    /// <summary>
    /// 平均磁盘写入队列长度
    /// </summary>
    public const string AvgDiskWriteQueueLength = "Avg. Disk Write Queue Length";

    /// <summary>
    /// 每秒磁盘传输次数
    /// </summary>
    public const string DiskTransfersPerSec = "Disk Transfers/sec";

    /// <summary>
    /// 每秒磁盘读取次数
    /// </summary>
    public const string DiskReadsPerSec = "Disk Reads/sec";

    /// <summary>
    /// 每秒磁盘写入次数
    /// </summary>
    public const string DiskWritesPerSec = "Disk Writes/sec";

    /// <summary>
    /// 每秒磁盘字节数
    /// </summary>
    public const string DiskBytesPerSec = "Disk Bytes/sec";

    /// <summary>
    /// 每秒磁盘读取字节数
    /// </summary>
    public const string DiskReadBytesPerSec = "Disk Read Bytes/sec";

    /// <summary>
    /// 每秒磁盘写入字节数
    /// </summary>
    public const string DiskWriteBytesPerSec = "Disk Write Bytes/sec";

    /// <summary>
    /// 平均每次磁盘传输秒数
    /// </summary>
    public const string AvgDiskSecPerTransfer = "Avg. Disk sec/Transfer";

    /// <summary>
    /// 平均每次磁盘读取秒数
    /// </summary>
    public const string AvgDiskSecPerRead = "Avg. Disk sec/Read";

    /// <summary>
    /// 平均每次磁盘写入秒数
    /// </summary>
    public const string AvgDiskSecPerWrite = "Avg. Disk sec/Write";

    /// <summary>
    /// 磁盘剩余空间百分比
    /// </summary>
    public const string FreeSpacePercent = "% Free Space";

    /// <summary>
    /// 磁盘剩余空间
    /// </summary>
    public const string FreeMegabytes = "Free Megabytes";

    /// <summary>
    /// 数据接收速率
    /// </summary>
    public const string BytesReceivedPerSec = "Bytes Received/sec";

    /// <summary>
    /// 数据发送速率
    /// </summary>
    public const string BytesSentPerSec = "Bytes Sent/sec";

    /// <summary>
    /// 数据总速率
    /// </summary>
    public const string BytesTotalPerSec = "Bytes Total/sec";

    /// <summary>
    /// 当前带宽
    /// </summary>
    public const string CurrentBandwidth = "Current Bandwidth";

    /// <summary>
    /// 每秒数据包
    /// </summary>
    public const string PacketsPerSec = "Packets/sec";

    /// <summary>
    /// 每秒接收数据包
    /// </summary>
    public const string PacketsReceivedPerSec = "Packets Received/sec";

    /// <summary>
    /// 每秒发送数据包
    /// </summary>
    public const string PacketsSentPerSec = "Packets Sent/sec";

    /// <summary>
    /// 当前连接数
    /// </summary>
    public const string ConnectionsEstablished = "Connections Established";

    /// <summary>
    /// 每秒数据报接收数
    /// </summary>
    public const string DatagramsReceivedPerSec = "Datagrams Received/sec";

    /// <summary>
    /// 每秒数据报发送数
    /// </summary>
    public const string DatagramsSentPerSec = "Datagrams Sent/sec";

    /// <summary>
    /// 线程数
    /// </summary>
    public const string ThreadCount = "Thread Count";

    /// <summary>
    /// 句柄数
    /// </summary>
    public const string HandleCount = "Handle Count";

    /// <summary>
    /// 私有字节数
    /// </summary>
    public const string PrivateBytes = "Private Bytes";

    /// <summary>
    /// 工作集
    /// </summary>
    public const string WorkingSet = "Working Set";

    /// <summary>
    /// 进程ID
    /// </summary>
    public const string IDProcess = "ID Process";

    /// <summary>
    /// 进程数
    /// </summary>
    public const string Processes = "Processes";

    /// <summary>
    /// 线程数
    /// </summary>
    public const string Threads = "Threads";

    /// <summary>
    /// 句柄数
    /// </summary>
    public const string Handles = "Handles";

    /// <summary>
    /// 系统调用速率
    /// </summary>
    public const string SystemCallsPerSec = "System Calls/sec";

    /// <summary>
    /// 文件读取速率
    /// </summary>
    public const string FileReadBytesPerSec = "File Read Bytes/sec";

    /// <summary>
    /// 文件写入速率
    /// </summary>
    public const string FileWriteBytesPerSec = "File Write Bytes/sec";

    /// <summary>
    /// GPU使用率
    /// </summary>
    public const string UtilizationPercentage = "Utilization Percentage";

    /// <summary>
    /// 专用显存使用量
    /// </summary>
    public const string DedicatedUsage = "Dedicated Usage";

    /// <summary>
    /// 共享显存使用量
    /// </summary>
    public const string SharedUsage = "Shared Usage";
}

public class InstanceCounterNames
{
    /// <summary>
    /// 总计
    /// </summary>
    public const string Total = "_Total";

    /// <summary>
    /// 空闲进程
    /// </summary>
    public const string Idle = "Idle";

    /// <summary>
    /// 系统进程
    /// </summary>
    public const string System = "System";

    /// <summary>
    /// 当前进程
    /// </summary>
    public static string CurrentProcess => Process.GetCurrentProcess().ProcessName;

    /// <summary>
    /// 全部实例
    /// </summary>
    public const string AllInstances = "*";
}

public static class PerformanceCounterManager
{
    /// <summary>
    /// “Cache”（缓存）、“Memory”（内存）、“Objects”（对象）、“PhysicalDisk”（物理磁盘）、“Process”（进程）、“Processor”（处理器）、“Server”（服务器）、“System”（系统）和“Thread”（线程）
    //SMB Server Shares
    //MSSQL$SQLEXPRESS:Wait Statistics
    //Hyper-V Hypervisor Partition
    //Hyper-V Dynamic Memory VM
    //Fax Service
    //WinNAT TCP
    //Hyper-V Virtual Storage Device
    //SQLAgent$SQLEXPRESS:SystemJobs
    //IPsec Connections
    //Generic IKEv1, AuthIP, and IKEv2
    //Pacer Pipe
    //Hyper-V Virtual Network Adapter
    //Storage Spaces Write Cache
    //SMSvcHost 3.0.0.0
    //UDPv6
    //Energy Meter
    //Event Tracing for Windows
    //MSSQL$SQLEXPRESS:User Settable
    //Hyper-V Configuration
    //TCPv4
    //RAS
    //Per Processor Network Interface Card Activity
    //XHCI CommonBuffer
    //MSSQL$SQLEXPRESS: Advanced Analytics
    //PacketDirect EC Utilization
    //MSSQL$SQLEXPRESS:SQL Errors
    //ServiceModelEndpoint 3.0.0.0
    //MSSQL$SQLEXPRESS:Broker Statistics
    //ReadyBoost Cache
    //IPv6
    //Pacer Flow
    //Network QoS Policy
    //Teredo Server
    //Hyper-V Virtual Machine Bus
    //Thermal Zone Information
    //Hyper-V Hypervisor Virtual Processor
    //MSSQL$SQLEXPRESS:Cursor Manager Total
    //.NET CLR Jit
    //Microsoft Winsock BSP
    //MSSQL$SQLEXPRESS:LogPool FreePool
    //MSSQL$SQLEXPRESS:FileTable
    //MSSQL$SQLEXPRESS:Cursor Manager by Type
    //SMSvcHost 4.0.0.0
    //MSSQL$SQLEXPRESS:Database Replica
    //RAS Total
    //NUMA Node Memory
    //SQLAgent$SQLEXPRESS:Jobs
    //MSSQL$SQLEXPRESS: 外部脚本
    //IPsec AuthIP IPv6
    //ASP.NET v4.0.30319
    //.NET CLR LocksAndThreads
    //MSSQL$SQLEXPRESS:Replication Merge
    //MSSQL$SQLEXPRESS:Replication Agents
    //Hyper-V Virtual Switch Port
    //Bluetooth Device
    //Processor
    //Hyper-V VM IO APIC
    //MSSQL$SQLEXPRESS:Access Methods
    //Synchronization
    //PacketDirect Receive Filters
    //Storage QoS Filter - Flow
    //SMB Server Sessions
    //Authorization Manager Applications
    //MSSQL$SQLEXPRESS:Exec Statistics
    //.NET CLR Exceptions
    //MSSQL$SQLEXPRESS:Memory Broker Clerks
    //.NET CLR Loading
    //Hyper-V VM Save, Snapshot, and Restore
    //ReFS
    //Hyper-V VM Vid Driver
    //Database ==> TableClasses
    //RAS Port
    //MSSQL$SQLEXPRESS:Memory Manager
    //IPsec IKEv1 IPv4
    //System
    //Hyper-V Virtual IDE Controller (Emulated)
    //Hyper-V Hypervisor Root Virtual Processor
    //IPsec IKEv2 IPv4
    //MSSQL$SQLEXPRESS:Replication Dist.
    //TCPIP Performance Diagnostics (Per-CPU)
    //ASP.NET Applications
    //SynchronizationNuma
    //MSSQL$SQLEXPRESS:Database Mirroring
    //Hyper-V VM Virtual Device Pipe IO
    //Teredo Relay
    //MSSQL$SQLEXPRESS:Workload Group Stats
    //.NET CLR Networking 4.0.0.0
    //Processor Information
    //SQLAgent$SQLEXPRESS:JobSteps
    //Hyper-V VM Vid Numa Node
    //Terminal Services
    //IPsec Driver
    //WSMan Quota Statistics
    //Network Interface
    //MSSQL$SQLEXPRESS:Columnstore
    //.NET CLR Memory
    //MSSQL$SQLEXPRESS:Plan Cache
    //Thread
    //MSSQL$SQLEXPRESS:CLR
    //Paging File
    //WinNAT UDP
    //Distributed Routing Table
    //MSSQL$SQLEXPRESS:Latches
    //PowerShell Workflow
    //BranchCache
    //RemoteFX Root GPU Management
    //Offline Files
    //WF(System.Workflow) 4.0.0.0
    //Hyper-V Hypervisor Root Partition
    //Teredo Client
    //Hyper-V Virtual Network Adapter Drop Reasons
    //LogicalDisk
    //ServiceModelOperation 4.0.0.0
    //IPv4
    //MSSQL$SQLEXPRESS:Locks
    //MSSQL$SQLEXPRESS:Broker/DBM Transport
    //MSSQL$SQLEXPRESS:Replication Snapshot
    //Storage Spaces Virtual Disk
    //MSSQL$SQLEXPRESS:General Statistics
    //IPsec IKEv2 IPv6
    //WinNAT
    //RemoteFX Graphics
    //Event Tracing for Windows Session
    //MSSQL$SQLEXPRESS:Databases
    //Storage Spaces Drt
    //Peer Name Resolution Protocol
    //PacketDirect Receive Counters
    //MSSQL$SQLEXPRESS:Availability Group
    //Hyper-V Virtual Switch Processor
    //PacketDirect Transmit Counters
    //ServiceModelService 4.0.0.0
    //Hyper-V Dynamic Memory Integration Service
    //WinNAT Instance
    //.NET CLR Interop
    //ServiceModelService 3.0.0.0
    //Hyper-V Virtual Machine Health Summary
    //Hyper-V Hypervisor
    //MSSQL$SQLEXPRESS:Batch Resp Statistics
    //TCPIP Performance Diagnostics
    //MSSQL$SQLEXPRESS:HTTP Storage
    //Browser
    //Database
    //IPHTTPS Session
    //UDPv4
    //Memory
    //Telephony
    //WFP Reauthorization
    //Hyper-V Hypervisor Logical Processor
    //Netlogon
    //NBT Connection
    //.NET Data Provider for Oracle
    //SMB Server
    //Per Processor Network Activity Cycles
    //DNS64 Global
    //IPsec IKEv1 IPv6
    //MSSQL$SQLEXPRESS:Broker Activation
    //Bluetooth Radio
    //Hyper-V VM Live Migration
    //WinNAT ICMP
    //MSSQL$SQLEXPRESS:Memory Node
    //Event Log
    //Search Indexer
    //.NET CLR Remoting
    //Cache
    //Hyper-V Dynamic Memory Balancer
    //ICMPv6
    //MSSQL$SQLEXPRESS:Query Store
    //MSSQL$SQLEXPRESS:Buffer Node
    //Process
    //Windows Workflow Foundation
    //SQLAgent$SQLEXPRESS:Alerts
    //Storage QoS Filter - Volume
    //ServiceModelOperation 3.0.0.0
    //Hyper-V Worker Virtual Processor
    //Job Object Details
    //MSDTC Bridge 4.0.0.0
    //ASP.NET Apps v4.0.30319
    //.NET Memory Cache 4.0
    //MSSQL$SQLEXPRESS:Resource Pool Stats
    //SQLAgent$SQLEXPRESS:Statistics
    //Database ==> Instances
    //Power Meter
    //ICMP
    //No name
    //Network Adapter
    //BitLocker
    //ServiceModelEndpoint 4.0.0.0
    //SMB Direct Connection
    //PacketDirect Queue Depth
    //ASP.NET
    //MSSQL$SQLEXPRESS:Buffer Manager
    //PhysicalDisk
    //Objects
    //IPsec AuthIP IPv4
    //.NET CLR Security
    //Print Queue
    //MSSQL$SQLEXPRESS:Availability Replica
    //MSSQL$SQLEXPRESS:Replication Logreader
    //TCPv6
    //Windows Time Service
    //Redirector
    //Hyper-V VM Remoting
    //WFPv6
    //Hyper-V VM Vid Partition
    //Storage Management WSP Spaces Runtime
    //Hyper-V Virtual Switch
    //Client Side Caching
    //ASP.NET State Service
    //.NET CLR Data
    //WorkflowServiceHost 4.0.0.0
    //RemoteFX Network
    //.NET Data Provider for SqlServer
    //Physical Network Interface Card Activity
    //IPHTTPS Global
    //XHCI TransferRing
    //MSSQL$SQLEXPRESS:Catalog Metadata
    //.NET CLR Networking
    //MSSQL$SQLEXPRESS:SQL Statistics
    //Hyper-V VM Worker Process Memory Manager
    //Hyper-V Virtual Network Adapter VRSS
    //Hyper-V Legacy Network Adapter
    //WFP
    //MSSQL$SQLEXPRESS:Transactions
    //MSSQL$SQLEXPRESS:Deprecated Features
    //FileSystem Disk Activity
    //Database ==> Databases
    //Storage Spaces Tier
    //MSDTC Bridge 3.0.0.0
    //HTTP Service
    //MSSQL$SQLEXPRESS:Broker TO Statistics
    //MSSQL$SQLEXPRESS:Backup Device
    //WFPv4
    //Hyper-V Virtual SMB
    //WFP Classify
    //XHCI Interrupter
    //Hyper-V Replica VM
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> GetCategoryNames()
    {
        PerformanceCounterCategory[] pcc = PerformanceCounterCategory.GetCategories();
        return pcc.Select(x => x.CategoryName);
    }
    /// <summary>
    /// 
    //        2
    //4
    //3
    //7
    //_Total
    //1
    //0
    //6
    //5
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetInstanceNames(string categoryName)
    {
        PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
        return diskCounter.GetInstanceNames();
    }

    /// <summary>
    /// Current Disk Queue Length
    //% Disk Time
    //Avg.Disk Queue Length
    //% Disk Read Time
    //Avg.Disk Read Queue Length
    //% Disk Write Time
    //Avg.Disk Write Queue Length
    //Avg.Disk sec/ Transfer
    //Avg.Disk sec/ Read
    //Avg.Disk sec/ Write
    //Disk Transfers/ sec
    //Disk Reads/ sec
    //Disk Writes/ sec
    //Disk Bytes/ sec
    //Disk Read Bytes / sec
    //Disk Write Bytes / sec
    //Avg.Disk Bytes/ Transfer
    //Avg.Disk Bytes/ Read
    //Avg.Disk Bytes/ Write
    //% Idle Time
    //Split IO / Sec
    /// </summary>
    /// <param name="categoryName"></param>
    /// <param name="instaneName"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetCounterNames(string categoryName, string instaneName)
    {
        PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
        PerformanceCounter[] counters = diskCounter.GetCounters(instaneName);
        return counters.Select(x => x.CounterName);
    }

    public static IEnumerable<T> GetCounterPresenters<T>(string categoryName, string instaneName, Func<string, string, string, T> creator) where T : IPerformanceCounterPresenter
    {
        PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
        PerformanceCounter[] counters = diskCounter.GetCounters(instaneName);
        return counters.Select(x => creator.Invoke(categoryName, instaneName, x.CounterName));
    }


    public static IEnumerable<T> GetCounterPresenters<T>(string categoryName, Func<string, string, T> creator) where T : IPerformanceCounterPresenter
    {
        PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
        PerformanceCounter[] counters = diskCounter.GetCounters();
        return counters.Select(x => creator.Invoke(categoryName, x.CounterName));
    }

    public static IEnumerable<T> GetCounterPresenters<T>(string categoryName, Func<string, string, string, T> creator) where T : IPerformanceCounterPresenter
    {
        if (PerformanceCounterCategory.Exists(categoryName))
        {
            PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
            string[] instances = diskCounter.GetInstanceNames();
            foreach (string instaneName in instances)
            {
                if (!PerformanceCounterCategory.InstanceExists(instaneName, diskCounter.CategoryName))
                    continue;
                PerformanceCounter[] counters = diskCounter.GetCounters(instaneName);
                foreach (PerformanceCounter x in counters)
                {
                    if (!PerformanceCounterCategory.CounterExists(x.CounterName, diskCounter.CategoryName))
                        continue;
                    yield return creator.Invoke(diskCounter.CategoryName, instaneName, x.CounterName);
                }
            }
        }
    }
}

public interface IPerformanceCounterPresenter
{
    void Start();
    void Stop();
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class PerformanceCounterInfoAttribute : Attribute
{
    public string CategoryName { get; set; } = string.Empty;
    public string CounterName { get; set; } = string.Empty;
    public string InstanceName { get; set; } = string.Empty;
    public string DisplayFormat { get; set; } = "{0}";
    public bool UseSum { get; set; }
}

public abstract class PerformanceCounterPresenterBase : DisplayBindableBase, IPerformanceCounterPresenter
{
    private Timer _timer = new Timer();
    private List<PerformanceCounter> _performanceCounters = new List<PerformanceCounter>();
    protected string DisplayFormat { get; set; } = "{0}";

    public PerformanceCounterPresenterBase()
    {
        {
            PerformanceCounterInfoAttribute attribute = this.GetType().GetCustomAttribute<PerformanceCounterInfoAttribute>();
            if (attribute != null)
            {
                CategoryName = attribute.CategoryName;
                CounterName = attribute.CounterName;
                InstanceName = attribute.InstanceName;
                this.DisplayFormat = attribute.DisplayFormat;
            }
        }
        _performanceCounters = CreatePerformanceCounters().ToList();
        _timer.Interval = 1000;
        _timer.Elapsed += Timer_Elapsed;
    }

    public double Interval
    {
        get { return _timer.Interval; }
        set { _timer.Interval = value; }
    }

    public PerformanceCounterPresenterBase(string categoryName, string instanceName, string counterName)
    {
        CategoryName = categoryName;
        CounterName = counterName;
        InstanceName = instanceName;

        _performanceCounters = CreatePerformanceCounters().ToList();
        _timer.Interval = 1000;
        _timer.Elapsed += Timer_Elapsed;
    }

    public string CategoryName { get; set; }
    public string CounterName { get; set; }
    public string InstanceName { get; set; }

    private float _minValue = float.MinValue;
    public float MinValue
    {
        get { return _minValue; }
        set
        {
            _minValue = value;
            RaisePropertyChanged();
            this.OnMaxMinValueChanged();
        }
    }

    private float _maxValue = float.MaxValue;
    public float MaxValue
    {
        get { return _maxValue; }
        set
        {
            _maxValue = value;
            RaisePropertyChanged();
            this.OnMaxMinValueChanged();
        }
    }

    protected virtual void OnMaxMinValueChanged()
    {


    }

    private bool _useAlarm;
    public bool UseAlarm
    {
        get { return _useAlarm; }
        set
        {
            _useAlarm = value;
            RaisePropertyChanged();
        }
    }

    protected virtual IEnumerable<PerformanceCounter> CreatePerformanceCounters()
    {
        if (string.IsNullOrEmpty(CategoryName) || string.IsNullOrEmpty(CounterName) || !PerformanceCounterCategory.Exists(CategoryName))
            yield break;

        if (!PerformanceCounterCategory.CounterExists(CounterName, CategoryName))
            yield break;

        if (!string.IsNullOrEmpty(InstanceName))
        {
            if (!PerformanceCounterCategory.InstanceExists(InstanceName, CategoryName))
                yield break;

            yield return new PerformanceCounter(CategoryName, CounterName, InstanceName);
        }
        else
        {
            string[] instances = PerformanceCounterManager.GetInstanceNames(CategoryName).ToArray();
            if (instances.Length == 0)
            {
                yield return new PerformanceCounter(CategoryName, CounterName);
            }
            else
            {
                foreach (string item in instances)
                {
                    yield return new PerformanceCounter(CategoryName, CounterName, item);
                }
            }
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        NextValue();
    }

    public virtual string NextValue()
    {
        try
        {
            return ConvertToValues(_performanceCounters.Select(x => x.NextValue()).ToList());
        }
        catch
        {
            return null;
        }
    }

    protected virtual string ConvertToValue(float value)
    {
        return string.Format(this.DisplayFormat, value);
    }

    protected virtual bool CheckValue(float value)
    {
        if (this.UseAlarm == false)
            return true;
        string v = this.ConvertToValue(value);

        if (value > this.MaxValue)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IocMessage.ShowSnackInfo($"{this.Name}参数值过高:{v}");
            });
            return false;
        }
        if (value < this.MinValue)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IocMessage.ShowSnackInfo($"{this.Name}参数值过低:{v}");
            });
            return false;
        }
        return true;
    }

    protected virtual string ConvertToValues(IEnumerable<float> values)
    {
        float sum = values.Sum(x => x);
        this.CheckValue(sum);
        return ConvertToValue(sum);
    }
    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }


    public RelayCommand StartCommand => new RelayCommand(x =>
    {
        Start();
    });


    public RelayCommand StopCommand => new RelayCommand(x =>
    {
        Stop();
    });
}


public class PerformanceCounterValuePresenter : PerformanceCounterPresenterBase
{
    public PerformanceCounterValuePresenter(string categoryName, string instanceName, string counterName) : base(categoryName, instanceName, counterName)
    {

    }

    protected override string ConvertToValue(float value)
    {
        return Value = string.Format(this.DisplayFormat, value);
    }

    private string _value;
    public string Value
    {
        get { return _value; }
        private set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }
}



[Display(Name = "CPU使用率", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.ProcessorTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class CpuCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "CPU用户时间", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.UserTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class CpuUserTimeCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "CPU特权时间", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.PrivilegedTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class CpuPrivilegedTimeCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "CPU空闲时间", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.IdleTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class CpuIdleTimeCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "CPU中断时间", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.InterruptTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class CpuInterruptTimeCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "CPU DPC时间", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.DPCTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class CpuDPCTimeCounter : PerformanceCounterValuePresenterBase
{

}

//[Display(Name = "CPU使用率", GroupName = "CPU")]
//[Counter(CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total")]
//public class CpuLineCounter : CounterLinePresenterBase
//{
//    public CpuLineCounter()
//    {
//        this.MaxValue = 80.0f;
//        this.UseAlarm = true;
//    }

//    protected override void DrawData(List<double> data)
//    {
//        //Application.Current?.Dispatcher?.BeginInvoke(DispatcherPriority.Input, new Action(() =>
//        //{
//        //    this.DynimacLinePresenter.RefreshData(data);
//        //    this.DynimacLinePresenter.LoadyAxis(data, 100.0, 0.0);
//        //}));

//        this.DelayInvoke(() =>
//        {
//            this.DynimacLinePresenter.RefreshData(data);
//            this.DynimacLinePresenter.LoadyAxis(data, 100.0, 0.0);
//        });

//    }
//}


[Display(Name = "CPU速度", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.DPCRate, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}")]
public class CpuDPCRateCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "CPU中断", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.InterruptsPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class CpuInterruptsRateCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "CPU队列", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Processor, CounterName = PerformanceCounterNames.DPCsQueuedPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class CpuDPCsRateCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "处理器队列长度", GroupName = "CPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.System, CounterName = PerformanceCounterNames.ProcessorQueueLength, DisplayFormat = "{0:f0}")]
public class ProcessorQueueLengthCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "可用内存", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.AvailableMBytes)]
public class RamCounter : PerformanceCounterSizeValuePresenterBase
{
    protected override string ConvertToValue(float value)
    {
        long l = (long)value;
        return Value = this.FormatSize(l * 1024 * 1024).ToString();
    }
}

[Display(Name = "内存使用率", GroupName = "内存")]
public class MemoryUsageCounter : PerformanceCounterValuePresenterBase
{
    public override string NextValue()
    {
        if (!OperatingSystem.IsWindows())
            return Value = string.Empty;

        MemoryStatus status = new();
        if (!GlobalMemoryStatusEx(status) || status.ullTotalPhys == 0)
            return Value = string.Empty;

        double value = (status.ullTotalPhys - status.ullAvailPhys) * 100.0 / status.ullTotalPhys;
        return Value = $"{value:f2} %";
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatus lpBuffer);

    [StructLayout(LayoutKind.Sequential)]
    private class MemoryStatus
    {
        public uint dwLength = (uint)Marshal.SizeOf<MemoryStatus>();
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }
}

[Display(Name = "已提交内存", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.CommitLimit)]
public class CommitCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "内存提交率", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.CommittedBytesInUsePercent, DisplayFormat = "{0:f2} %")]
public class CommittedBytesInUseCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "已提交字节", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.CommittedBytes)]
public class CommittedBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "分页缓冲池", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.PoolPagedBytes)]
public class PoolPagedCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "非分页缓冲池", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.PoolNonpagedBytes)]
public class PoolNonpagedCounter : PerformanceCounterSizeValuePresenterBase
{
    protected override string ConvertToValue(float value)
    {
        long l = (long)value;
        return Value = this.FormatSize(l).ToString();
    }
}

[Display(Name = "页面交换", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.PagesPerSec, DisplayFormat = "{0:f2}/s")]
public class PagesPerSecCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "页面读取", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.PageReadsPerSec, DisplayFormat = "{0:f2}/s")]
public class PageReadsPerSecCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "页面写入", GroupName = "内存")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Memory, CounterName = PerformanceCounterNames.PageWritesPerSec, DisplayFormat = "{0:f2}/s")]
public class PageWritesPerSecCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "磁盘读取速度")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskReadsPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class ReadPhysicalDiskCounter : PerformanceCounterValuePresenterBase
{

}


[Display(Name = "磁盘写入速度")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskWritesPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class WritePhysicalDiskCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "磁盘读取字节", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskReadBytesPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class DiskReadBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "磁盘写入字节", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskWriteBytesPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class DiskWriteBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "磁盘传输字节", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskBytesPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class DiskBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "磁盘队列长度", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.CurrentDiskQueueLength, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2}")]
public class CurrentDiskQueueLengthCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "磁盘使用率", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class DiskTimeCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "磁盘空闲率", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.DiskIdleTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class DiskIdleTimeCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "逻辑磁盘空闲率", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.LogicalDisk, CounterName = PerformanceCounterNames.FreeSpacePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class LogicalDiskFreeSpaceCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "逻辑磁盘可用空间", GroupName = "磁盘")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.LogicalDisk, CounterName = PerformanceCounterNames.FreeMegabytes, InstanceName = InstanceCounterNames.Total)]
public class LogicalDiskFreeMegabytesCounter : PerformanceCounterSizeValuePresenterBase
{
    protected override string ConvertToValue(float value)
    {
        long l = (long)value;
        return Value = this.FormatSize(l * 1024 * 1024).ToString();
    }
}

[Display(Name = "磁盘平均写入时间")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.AvgDiskSecPerWrite, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class AvgWritePhysicalDiskCounter : PerformanceCounterValuePresenterBase
{

}


[Display(Name = "磁盘平均读取时间")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.PhysicalDisk, CounterName = PerformanceCounterNames.AvgDiskSecPerRead, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class AvgReadPhysicalDiskCounter : PerformanceCounterValuePresenterBase
{

}

//[Display(Name = "GPU使用率")]
//[Counter(CategoryName = "GPU Engine", CounterName = "Utilization Percentage",DisplayFormat = "{0} Bytes/sec")]
//public class GPUCounter : PerformanceCounterPresenterBase
//{
//    protected override string ConvertToValue(float value)
//    {
//        return string.Format("{0} Bytes/sec", value);
//    }
//}

[Display(Name = "网络下载速度")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.BytesReceivedPerSec, DisplayFormat = "{0}/s")]
public class ReceivedNetworkCounter : PerformanceCounterSizeValuePresenterBase
{

}

//[Display(Name = "网络下载速度", GroupName = "网络")]
//[Counter(CategoryName = "Network Interface", CounterName = "Bytes Received/sec")]
//public class LineReceivedNetworkCounter : CounterLinePresenterBase
//{

//}

[Display(Name = "网络上传速度")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.BytesSentPerSec, DisplayFormat = "{0}/s")]
public class SendNetworkCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "网络总速度", GroupName = "网络")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.BytesTotalPerSec, DisplayFormat = "{0}/s")]
public class TotalNetworkCounter : PerformanceCounterSizeValuePresenterBase
{

}

//[Display(Name = "网络上传速度", GroupName = "网络")]
//[Counter(CategoryName = PerformanceCounterKeys.NetworkInterface, CounterName = "Bytes Sent/sec")]
//public class SendReceivedNetworkCounter : CounterLinePresenterBase
//{

//}


[Display(Name = "网络带宽")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.CurrentBandwidth, DisplayFormat = "{0}")]
public class BandWidthNetworkCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "数据包")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.PacketsPerSec, DisplayFormat = "{0:f2}/s")]
public class PacketsNetworkCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "接收数据包", GroupName = "网络")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.PacketsReceivedPerSec, DisplayFormat = "{0:f2}/s")]
public class PacketsReceivedNetworkCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "发送数据包", GroupName = "网络")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.NetworkInterface, CounterName = PerformanceCounterNames.PacketsSentPerSec, DisplayFormat = "{0:f2}/s")]
public class PacketsSentNetworkCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "TCP连接数", GroupName = "网络")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.TCPv4, CounterName = PerformanceCounterNames.ConnectionsEstablished, DisplayFormat = "{0:f0}")]
public class TcpConnectionsEstablishedCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "UDP接收数据报", GroupName = "网络")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.UDPv4, CounterName = PerformanceCounterNames.DatagramsReceivedPerSec, DisplayFormat = "{0:f2}/s")]
public class UdpDatagramsReceivedCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "UDP发送数据报", GroupName = "网络")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.UDPv4, CounterName = PerformanceCounterNames.DatagramsSentPerSec, DisplayFormat = "{0:f2}/s")]
public class UdpDatagramsSentCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "进程数量", GroupName = "系统")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.System, CounterName = PerformanceCounterNames.Processes, DisplayFormat = "{0:f0}")]
public class ProcessCountCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "线程数量", GroupName = "系统")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.System, CounterName = PerformanceCounterNames.Threads, DisplayFormat = "{0:f0}")]
public class ThreadCountCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "句柄数量", GroupName = "系统")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Objects, CounterName = PerformanceCounterNames.Handles, DisplayFormat = "{0:f0}")]
public class HandleCountCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "系统调用", GroupName = "系统")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.System, CounterName = PerformanceCounterNames.SystemCallsPerSec, DisplayFormat = "{0:f2}/s")]
public class SystemCallsCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "进程CPU", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.ProcessorTimePercent, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f2} %")]
public class ProcessCpuCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "进程工作集", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.WorkingSet, InstanceName = InstanceCounterNames.Total)]
public class ProcessWorkingSetCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "进程私有内存", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.PrivateBytes, InstanceName = InstanceCounterNames.Total)]
public class ProcessPrivateBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "进程线程数", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.ThreadCount, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f0}")]
public class ProcessThreadCountCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "进程句柄数", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.HandleCount, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0:f0}")]
public class ProcessHandleCountCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "进程文件读取", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.FileReadBytesPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class ProcessFileReadBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "进程文件写入", GroupName = "进程")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.Process, CounterName = PerformanceCounterNames.FileWriteBytesPerSec, InstanceName = InstanceCounterNames.Total, DisplayFormat = "{0}/s")]
public class ProcessFileWriteBytesCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "GPU使用率", GroupName = "GPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.GPUEngine, CounterName = PerformanceCounterNames.UtilizationPercentage, DisplayFormat = "{0:f2} %")]
public class GPUCounter : PerformanceCounterValuePresenterBase
{

}

[Display(Name = "专用显存", GroupName = "GPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.GPUAdapterMemory, CounterName = PerformanceCounterNames.DedicatedUsage)]
public class GPUDedicatedMemoryCounter : PerformanceCounterSizeValuePresenterBase
{

}

[Display(Name = "共享显存", GroupName = "GPU")]
[PerformanceCounterInfo(CategoryName = PerformanceCounterCategoryNames.GPUAdapterMemory, CounterName = PerformanceCounterNames.SharedUsage)]
public class GPUSharedMemoryCounter : PerformanceCounterSizeValuePresenterBase
{

}
