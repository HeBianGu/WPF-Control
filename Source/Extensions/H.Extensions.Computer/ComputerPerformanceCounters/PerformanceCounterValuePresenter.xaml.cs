// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Computer.ComputerPerformanceCounters;
public abstract class PerformanceCounterValuePresenterBase : PerformanceCounterPresenterBase, IPerformanceCounterPresenter
{
    protected override string ConvertToValue(float value)
    {
        return this.Value = string.Format(this.DisplayFormat, value);
    }

    private string _value;
    public string Value
    {
        get { return _value; }
        protected set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }
}
public abstract class PerformanceCounterSizeValuePresenterBase : PerformanceCounterValuePresenterBase, IPerformanceCounterPresenter
{
    protected string FormatSize(long size)
    {
        double TB = 1024 * 1024 * 1024 * 1024.0;
        int GB = 1024 * 1024 * 1024;
        int MB = 1024 * 1024;
        int KB = 1024;

        long KSize = size;
        bool isMinus = KSize < 0;
        string result;
        KSize = Math.Abs(KSize);
        if (KSize / TB >= 1)
            result = Math.Round(KSize / (float)TB, 2).ToString() + "T";
        else if (KSize / GB >= 1)
            result = Math.Round(KSize / (float)GB, 2).ToString() + "G";
        else if (KSize / MB >= 1)

            result = Math.Round(KSize / (float)MB, 2).ToString() + "MB";
        else if (KSize / KB >= 1)

            result = Math.Round(KSize / (float)KB, 2).ToString() + "KB";
        else
            result = KSize.ToString() + "Byte";
        return isMinus ? "-" + result : result;
    }

    protected override string ConvertToValue(float value)
    {
        long l = (long)value;
        string d = this.FormatSize(l);
        return Value = string.Format(this.DisplayFormat, d);
    }
}

