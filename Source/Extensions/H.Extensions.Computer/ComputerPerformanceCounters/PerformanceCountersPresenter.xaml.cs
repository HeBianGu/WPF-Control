// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Reflection;

namespace H.Extensions.Computer.ComputerPerformanceCounters;

public class PerformanceCountersPresenter : DisplayBindableBase
{
    protected PerformanceCountersPresenter()
    {
        this.PerformanceCounterPresenters = new ObservableCollection<IPerformanceCounterPresenter>(this.GetPerformanceCounterPresenters());
    }
    private ObservableCollection<IPerformanceCounterPresenter> _PerformanceCounterPresenters = new ObservableCollection<IPerformanceCounterPresenter>();
    public ObservableCollection<IPerformanceCounterPresenter> PerformanceCounterPresenters
    {
        get { return _PerformanceCounterPresenters; }
        set
        {
            _PerformanceCounterPresenters = value;
            RaisePropertyChanged();
        }
    }

    protected virtual IEnumerable<IPerformanceCounterPresenter> GetPerformanceCounterPresenters()
    {
        return this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => typeof(IPerformanceCounterPresenter).IsAssignableFrom(p.PropertyType))
            .Select(p => p.GetValue(this) as IPerformanceCounterPresenter)
            .Where(p => p != null);
    }


    public void Start()
    {
        foreach (var item in this.PerformanceCounterPresenters)
        {
            item.Start();
        }
    }

    public void Stop()
    {
        foreach (var item in this.PerformanceCounterPresenters)
        {
            item.Stop();
        }
    }
}
