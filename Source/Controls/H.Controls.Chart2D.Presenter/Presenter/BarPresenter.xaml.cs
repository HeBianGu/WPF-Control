// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Common;
using H.Common.Attributes;

namespace H.Controls.Chart2D.Presenter.Presenter;
[Icon("\xE9D2")]
[Display(Name = "柱状图")]
public class BarPresenter : LinePresenter
{
    public BarPresenter()
    {

    }
    public BarPresenter(IEnumerable<double> data) : base(data)
    {

    }

    public BarPresenter(IChartDataProvider dataProvider) : base(dataProvider)
    {

    }

    public BarPresenter(IEnumerable<Tuple<string, double>> tuples) : base(tuples)
    {

    }
}
