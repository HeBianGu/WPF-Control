﻿using H.Services.Common;
using H.Mvvm;
using System.Collections;

namespace H.Controls.OrderBox
{
    public abstract class OrderBase : DisplayBindableBase, IOrderable
    {
        private bool _useDesc;
        public bool UseDesc
        {
            get { return _useDesc; }
            set
            {
                _useDesc = value;
                RaisePropertyChanged();
            }
        }
        public abstract IEnumerable Where(IEnumerable from);

        public override string ToString()
        {
            return this.Name ?? base.ToString();
        }
    }

}
