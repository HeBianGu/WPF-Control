using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using H.Extensions.DataBase;
using H.Common;

namespace H.Modules.Operation
{
    public class OperationDataProvider : LazyInstance<OperationDataProvider>
    {
        public IChartDataProvider GetUserLoginData()
        {
            return this.GetUserMethodsData("Login");
        }

        public IChartDataProvider GetUserMethodsData(params string[] methodNames)
        {
            IEnumerable<IGrouping<string, hi_dd_operation>> logins = this.Datas.Where(x => methodNames.Count() == 0 || methodNames.Any(l => l == x.Method)).GroupBy(x => x.UserName);
            return new OperationChartDataProvider(logins.Select(x => Tuple.Create($"{x.Key}[{x.Count()}]", (double)x.Count())));
        }

        protected List<hi_dd_operation> Datas => Ioc<IStringRepository<hi_dd_operation>>.Instance.GetList();

        public IChartDataProvider GetLastDayLoginData(int count = 7)
        {
            IEnumerable<IGrouping<DateTime, hi_dd_operation>> datas = this.Datas.GroupBy(x => x.CDATE.Date);
            List<Tuple<string, double>> tuples = new List<Tuple<string, double>>();
            for (int i = 0; i < count; i++)
            {
                DateTime date = DateTime.Now.AddDays(i - count + 1).Date;
                IGrouping<DateTime, hi_dd_operation> data = datas.FirstOrDefault(x => x.Key == date);
                if (data == null)
                {
                    tuples.Add(Tuple.Create(date.ToString("yyyy-MM-dd"), 0.0));
                }
                else
                {
                    tuples.Add(Tuple.Create(date.ToString("yyyy-MM-dd"), (double)data.Count()));
                }

            }
            return new OperationChartDataProvider(tuples);
        }
    }

    public class OperationChartDataProvider : IChartDataProvider
    {
        private IEnumerable<Tuple<string, double>> _data;
        public OperationChartDataProvider(IEnumerable<Tuple<string, double>> data)
        {
            _data = data;
        }

        public IEnumerable<Tuple<string, double>> GetData()
        {
            return _data;
        }
    }
}
