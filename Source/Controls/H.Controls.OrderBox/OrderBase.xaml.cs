using H.Providers.Ioc;
using H.Providers.Mvvm;
using System.Collections;

namespace H.Controls.OrderBox
{
    public abstract class OrderBase : DisplayerViewModelBase, IOrder
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
