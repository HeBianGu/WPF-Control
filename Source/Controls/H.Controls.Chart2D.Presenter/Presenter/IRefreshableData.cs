// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Extensions.Mvvm.ViewModels;
using System.Windows.Media;

namespace H.Controls.Chart2D.Presenter.Presenter;

public interface IChartDesignPresenter : IDesignPresenter, IDisplayBindable
{
    public ObservableCollection<string> xDisplay { get; set; }
    public DoubleCollection Data { get; set; }
    void UpdateData(IEnumerable<Tuple<string, double>> tuples);
}
