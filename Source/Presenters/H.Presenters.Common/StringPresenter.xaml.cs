using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Presenters.Common
{
    public class StringPresenter : DisplayBindableBase, IStringPresenter
    {
        private string _value;
        public string Value
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
