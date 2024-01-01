
using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Presenters.Common
{
    public class PercentPresenter : DisplayViewModelBase, IPercentPresenter
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
