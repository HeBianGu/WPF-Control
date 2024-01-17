using System;
using System.ComponentModel;

namespace H.Controls.ColorPicker.Models
{
    public class NotifyableObject : INotifyPropertyChanged
    {
        [field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}