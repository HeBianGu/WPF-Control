using H.Controls.Chart2D;
using H.Extensions.Common;
using H.Mvvm.ViewModels.Base;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Extension.AutoTest;

public class AutoTestReportViewPresenter : BindableBase
{
    private int _days = 30;
    public int Days
    {
        get { return _days; }
        set
        {
            _days = value;
            RaisePropertyChanged();
        }
    }

    public AutoTestReportViewPresenter()
    {
        Random random = new Random();
        {

            PointData seriesData = new PointData();
            for (int i = 0; i <= this.Days; i++)
            {
                DateTime current = DateTime.Now.AddDays(i - this.Days).Date;
                this.LoginData.xAxis.Add(i - this.Days);
                this.LoginData.xDisplay.Add(current.ToString("dd"));
                seriesData.xDatas.Add(i - this.Days);
                seriesData.yDatas.Add(random.NextDouble() * 100);
            }
            this.LoginData.SeriesDatas.Add(seriesData);
            this.LoginData.BuildyAxis(0, 100);
        }

        {
            System.Collections.Generic.IEnumerable<Tuple<double, string>> data = Enumerable.Range(0, 2).Select(x => Tuple.Create(random.NextDouble() * 100, x == 0 ? "合格" : "不合格"));
            DoubleData seriesData = new DoubleData();
            seriesData.Build(data);
            this.UserData.SeriesDatas.Add(seriesData);
            this.UserData.SeriesDatas = this.UserData.SeriesDatas.ToObservable();
        }
    }
    private ChartData _loginData = new ChartData();
    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public ChartData LoginData
    {
        get { return _loginData; }
        set
        {
            _loginData = value;
            RaisePropertyChanged();
        }
    }

    private ChartData _userData = new ChartData();
    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public ChartData UserData
    {
        get { return _userData; }
        set
        {
            _userData = value;
            RaisePropertyChanged();
        }
    }
}
