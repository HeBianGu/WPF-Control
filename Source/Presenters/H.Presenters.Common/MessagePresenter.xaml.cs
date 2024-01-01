using H.Providers.Mvvm;

namespace H.Presenters.Common
{
    public class MessagePresenter : DisplayViewModelBase
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
