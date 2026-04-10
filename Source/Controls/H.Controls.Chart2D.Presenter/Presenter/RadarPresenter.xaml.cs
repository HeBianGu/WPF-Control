// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.Controls.Chart2D.Presenter.Presenter;
[Icon(FontIcons.Communications)]
[Display(Name = "雷达图")]
public class RadarPresenter : Chart2DPresenterBase
{
    public RadarPresenter()
    {
        this.Height = 500;
        IEnumerable<double> data = Enumerable.Range(0, 6).Select(x => x + (random.NextDouble() * 10));
        this.RefreshData(data);
    }
    public RadarPresenter(IEnumerable<double> data) : this()
    {
        this.RefreshData(data);
    }



    public RadarPresenter(IChartDataProvider dataProvider) : this()
    {
        this.xDisplay = dataProvider.GetData().Select(x => x.Item1).ToObservable();
        IEnumerable<double> data = dataProvider.GetData().Select(x => x.Item2);
        this.RefreshData(data);
    }

    public RadarPresenter(IEnumerable<Tuple<string, double>> tuples) : this()
    {
        this.xDisplay = tuples.Select(x => x.Item1).ToObservable();
        IEnumerable<double> data = tuples.Select(x => x.Item2);
        this.RefreshData(data);
    }

    public override void UpdateData(IEnumerable<Tuple<string, double>> tuples)
    {
        this.xDisplay = tuples.Select(x => x.Item1).ToObservable();
        IEnumerable<double> data = tuples.Select(x => x.Item2);
        this.RefreshData(data);
    }

    public void RefreshData(IEnumerable<double> data)
    {
        this.LoadyAxis(data);
        this.LoadxAxis(data);
        this.Data = new DoubleCollection(data);
    }

    protected virtual void LoadyAxis(IEnumerable<double> data, double max = 360.0, double min = 0.0)
    {
        _yAxis.Clear();
        double span = 360.0 / data.Count();
        for (int i = 0; i < data.Count(); i++)
        {
            _yAxis.Add(span * i);
        }
    }

    protected virtual void LoadxAxis(IEnumerable<double> data, double max = double.PositiveInfinity, double min = 0.0, int count = 5)
    {
        IEnumerable<double> ds = this.Load(data, max, min, 5);
        this.xAxis = new DoubleCollection(ds);
    }

    private DoubleCollection _xAxis = new DoubleCollection();
    [Display(Name = "X轴", GroupName = "常用,数据")]
    public DoubleCollection xAxis
    {
        get { return _xAxis; }
        set
        {
            _xAxis = value;
            RaisePropertyChanged();
        }
    }

    private DoubleCollection _yAxis = new DoubleCollection();
    [Display(Name = "Y轴", GroupName = "常用,数据")]
    public DoubleCollection yAxis
    {
        get { return _yAxis; }
        set
        {
            _yAxis = value;
            RaisePropertyChanged();
        }
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
