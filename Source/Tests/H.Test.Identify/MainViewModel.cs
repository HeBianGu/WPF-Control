using H.Controls.Chart2D;
using H.Modules.Operation;
using H.Mvvm.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace H.Test.Identify
{
    public class MainViewModel : Bindable
    {
        protected override void Loaded(object obj)
        {
            base.Loaded(obj);

            {
                var data = OperationDataProvider.Instance.GetUserLoginData();
                this.Bar = new BarPresenter(data);

                this.Radar = new RadarPresenter(data);
            }
            {
                var data = OperationDataProvider.Instance.GetLastDayLoginData();
                this.Line = new LinePresenter(data);
            }

            {
                var data = OperationDataProvider.Instance.GetUserMethodsData();
                this.Pie = new PiePresenter(data);
            }
        }

        private BarPresenter _bar;
        /// <summary> 说明  </summary>
        public BarPresenter Bar
        {
            get { return _bar; }
            set
            {
                _bar = value;
                RaisePropertyChanged();
            }
        }

        private PiePresenter _pie;
        /// <summary> 说明  </summary>
        public PiePresenter Pie
        {
            get { return _pie; }
            set
            {
                _pie = value;
                RaisePropertyChanged();
            }
        }

        private RadarPresenter _radar;
        /// <summary> 说明  </summary>
        public RadarPresenter Radar
        {
            get { return _radar; }
            set
            {
                _radar = value;
                RaisePropertyChanged();
            }
        }


        private LinePresenter _line;
        /// <summary> 说明  </summary>
        public LinePresenter Line
        {
            get { return _line; }
            set
            {
                _line = value;
                RaisePropertyChanged();
            }
        }


    }
}
