// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Computer.ComputerPerformanceCounters;

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
