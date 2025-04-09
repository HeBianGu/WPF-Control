// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Chart2D
{
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
    }
}
