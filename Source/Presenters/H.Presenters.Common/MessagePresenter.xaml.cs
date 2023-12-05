using H.Providers.Mvvm;

namespace H.Presenters.Common
{
    public class MessagePresenter : DisplayerViewModelBase
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
