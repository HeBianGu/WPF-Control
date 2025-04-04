﻿using H.Mvvm.ViewModels.Base;
using System.ComponentModel;

namespace H.Mvvm
{
    public class DisplayBindable<T> : DisplayBindableBase
    {
        public DisplayBindable(T t)
        {
            this.Model = t;

        }

        private T _model;
        [Browsable(false)]
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }
    }

}
