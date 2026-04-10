// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Common;
using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.Controls.Chart2D.Presenter.Presenter;
[Icon(FontIcons.EmojiTabFavorites)]
[Display(Name = "饼状图")]
public class PiePresenter : Chart2DPresenterBase
{
    public PiePresenter()
    {
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
        this.UpdateData(tuples);
    }

    public override void UpdateData(IEnumerable<Tuple<string, double>> tuples)
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

    private double _len = double.NaN;
    [DefaultValue(double.NaN)]
    [Display(Name = "显示半径", GroupName = "常用,样式")]
    public double Len
    {
        get { return _len; }
        set
        {
            _len = value;
            RaisePropertyChanged();
        }
    }
}
