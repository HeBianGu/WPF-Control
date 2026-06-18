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
