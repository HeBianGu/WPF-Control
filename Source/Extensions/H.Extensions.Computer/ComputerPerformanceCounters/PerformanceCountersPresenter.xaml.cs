// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace H.Extensions.Computer.ComputerPerformanceCounters;

public interface IPerformanceCountersPresenter
{
    void Start();
    void Stop();
}

public class PerformanceCountersPresenter : DisplayBindableBase, IPerformanceCountersPresenter
{
    public PerformanceCountersPresenter()
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
        return this.GetType().Assembly.GetTypes()
            .Where(p => typeof(IPerformanceCounterPresenter).IsAssignableFrom(p) && !p.IsAbstract)
            .Select(p => Activator.CreateInstance(p)).OfType<IPerformanceCounterPresenter>()
            .OrderBy(p => p.GetType().GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? 0);
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
