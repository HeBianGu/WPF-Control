using H.Mvvm;
using H.Services.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace H.Controls.Form
{
    public class TabFormPresenter : FormPresenter
    {
        public TabFormPresenter()
        {

        }
        public TabFormPresenter(object value) : base(value)
        {

        }

        public ObservableCollection<string> TabNames { get; set; } = new ObservableCollection<string>();
    }
}
