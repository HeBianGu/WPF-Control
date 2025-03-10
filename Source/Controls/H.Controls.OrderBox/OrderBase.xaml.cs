using H.Services.Common;
using System.Collections;
using System.Windows;
using System;
using H.Mvvm.ViewModels.Base;

namespace H.Controls.OrderBox
{
    public abstract class OrderBase : DisplayBindableBase, IOrderWhereable
    {
        private bool _useDesc;
        public bool UseDesc
        {
            get { return _useDesc; }
            set
            {
                _useDesc = value;
                RaisePropertyChanged();
                //this.OrderChanged?.Invoke()
            }
        }
        public abstract IEnumerable Where(IEnumerable from);

        public override string ToString()
        {
            return this.Name ?? base.ToString();
        }

        //public event EventHandler<EventArgs> OrderChanged
        //{
        //    add { WeakEventManager<OrderBase, EventArgs>.AddHandler(this, nameof(OrderChanged), value); }
        //    remove { WeakEventManager<OrderBase, EventArgs>.RemoveHandler(this, nameof(OrderChanged), value); }
        //}
    }

}
