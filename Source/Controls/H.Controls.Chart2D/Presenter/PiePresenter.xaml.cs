// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Extensions.Common;
using H.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;

namespace H.Controls.Chart2D
{
    [Icon("\xE9D2")]
    [Display(Name = "饼状图")]
    public class PiePresenter : Chart2DPresenterBase
    {
        public PiePresenter()
        {
            this.Height = 500;
            this.Foreground = ChartBrushKeys.ChartColors.ToObservable();
            for (int i = 1; i < 5; i++)
            {
                this.Data.Add(random.Next(10));
            }
        }
        public PiePresenter(IEnumerable<double> data) : this()
        {
            this.RefreshData(data);
        }

        public PiePresenter(IChartDataProvider dataProvider) : this()
        {
            this.xDisplay = dataProvider.GetData().Select(x => x.Item1).ToObservable();
            IEnumerable<double> data = dataProvider.GetData().Select(x => x.Item2);
            this.RefreshData(data);
        }

        public PiePresenter(IEnumerable<Tuple<string, double>> tuples) : this()
        {
            this.xDisplay = tuples.Select(x => x.Item1).ToObservable();
            IEnumerable<double> data = tuples.Select(x => x.Item2);
            this.RefreshData(data);
        }

        public void RefreshData(IEnumerable<double> data)
        {
            this.Data = new DoubleCollection(data);
        }
        private ObservableCollection<Color> _foreground = new ObservableCollection<Color>();
        [Display(Name = "显示颜色", GroupName = "常用,数据")]
        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public ObservableCollection<Color> Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }
    }


}
