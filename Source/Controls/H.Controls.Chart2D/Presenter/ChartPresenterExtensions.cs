// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
namespace H.Controls.Chart2D
{
    public static class ChartPresenterExtensions
    {
        public static PiePresenter ToPiePresenter(this IEnumerable<double> data)
        {
            return new PiePresenter(data);
        }
        public static PiePresenter ToPiePresenter(this IChartDataProvider dataProvider)
        {
            return new PiePresenter(dataProvider);
        }
        public static PiePresenter ToPiePresenter(this IEnumerable<Tuple<string, double>> tuples)
        {
            return new PiePresenter(tuples);
        }

        public static PiePresenter ToPiePresenter<T>(this IEnumerable<T> datas, Func<T, string> nameSelecter, Func<T, double> valueSelecter)
        {
            var tuples = datas.ToTuples(nameSelecter, valueSelecter);
            return new PiePresenter(tuples);
        }

        public static IEnumerable<Tuple<string, double>> ToTuples<T>(this IEnumerable<T> datas, Func<T, string> nameSelecter, Func<T, double> valueSelecter)
        {
            return datas.Select(x => Tuple.Create(nameSelecter.Invoke(x), valueSelecter.Invoke(x)));
        }
    }


}
