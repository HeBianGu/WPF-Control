using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Controls.FilterBox
{
    public abstract class FilterBase : DisplayerViewModelBase, IFilter
    {
        public abstract bool IsMatch(object obj);

        public override string ToString()
        {
            return this.Name ?? GetType().Name;
        }
    }
}
