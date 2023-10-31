using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Presenters.Common
{
    public class PercentPresenter : DisplayerViewModelBase, IPercentPresenter
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }
}
