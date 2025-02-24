using H.Mvvm;
using H.Services.Common;
using System.Collections.ObjectModel;
using System.Windows;

namespace H.Controls.Form
{
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
