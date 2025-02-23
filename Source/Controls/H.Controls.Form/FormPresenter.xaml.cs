using H.Mvvm;
using H.Services.Common;
using System.Collections.ObjectModel;
using System.Windows;

namespace H.Controls.Form
{
    public class FormPresenter : DisplayBindableBase, IFormOption
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

        public string ExceptPropertyNames { get; set; }
        public double MessageWidth { get; set; }
        public object SelectObject { get; set; }
        public string Title { get; set; }
        public bool UseArray { get; set; }
        public bool UseAsync { get; set; }
        public bool UseBoolen { get; set; }
        public bool UseClass { get; set; }
        public bool UseCommand { get; set; }
        public bool UseCommandOnly { get; set; }
        public bool UseDateTime { get; set; }
        public bool UseDeclaredOnly { get; set; }
        public bool UseDisplayOnly { get; set; }
        public bool UseEnum { get; set; }
        public bool UseEnumerator { get; set; }
        public bool UseGroup { get; set; }
        public string UseGroupNames { get; set; }
        public bool UseInterface { get; set; }
        public bool UseNull { get; set; }
        public bool UseOrder { get; set; }
        public bool UseOrderByName { get; set; }
        public bool UseOrderByType { get; set; }
        public bool UsePresenter { get; set; }
        public bool UsePrimitive { get; set; }
        public string UsePropertyNames { get; set; }
        public bool UseString { get; set; }
        public bool UseTypeConverter { get; set; }
        public bool UseTypeConverterOnly { get; set; }
        public double MinWidth { get; set; }
        public double Width { get; set; }
        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
    }

    public class StaticFormPresenter : FormPresenter
    {
        public StaticFormPresenter(object value) : base(value)
        {

        }
    }

    public class ItemsFormPresenter : DisplayBindableBase
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
