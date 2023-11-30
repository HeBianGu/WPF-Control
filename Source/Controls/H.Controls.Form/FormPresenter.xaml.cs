using H.Providers.Mvvm;
using System.Collections.ObjectModel;

namespace H.Controls.Form
{
    public class FormPresenter : DisplayerViewModelBase
    {
        public FormPresenter(object value)
        {
            this._value = value;
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        private bool _usePropertyView;
        public bool UsePropertyView
        {
            get { return _usePropertyView; }
            set
            {
                _usePropertyView = value;
                RaisePropertyChanged();
            }
        }
    }

    public class StaticFormPresenter : FormPresenter
    {
        public StaticFormPresenter(object value) : base(value)
        {

        }
    }

    public class ItemsFormPresenter : DisplayerViewModelBase
    {
        private ObservableCollection<object> _objs = new ObservableCollection<object>();

        public ObservableCollection<object> Objs
        {
            get { return _objs; }
            set
            {
                _objs = value;
                RaisePropertyChanged("Objs");
            }
        }
    }
}
