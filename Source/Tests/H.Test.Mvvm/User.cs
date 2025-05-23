using H.Mvvm.ViewModels.Base;

namespace H.Test.Mvvm
{
    public class User : Bindable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

    }
}
