using H.Extensions.Revertible;
using H.Services.Common;
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Services.Revertible;

namespace H.Test.Revertible
{
    internal class MainViewModel : RevertiblePropertyChangedBase
    {
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                string oldValue = _value;
                var persenter = new PropertyChangedRevertiblePrensenter<string>(nameof(this.Value), _value, value);
                IocRevertible.Commit(() =>
                {
                    _value = value;
                    RaisePropertyChanged();

                }, () =>
                    {
                        _value = oldValue;
                        RaisePropertyChanged();
                    }, null, persenter);
            }
        }

        private string _value1;
        /// <summary> 说明  </summary>
        public string Value1
        {
            get { return _value1; }
            set
            {
                this.SetRevertiableProperty(v => _value1 = v, _value1, value);
            }
        }

    }
}
