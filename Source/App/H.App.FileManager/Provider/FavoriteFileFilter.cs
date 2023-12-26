
using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.App.FileManager
{
    public class FavoriteFileFilter : IFilter
    {
        public string DisplayName { get; set; }
        public bool Value { get; set; }
        public bool IsMatch(object obj)
        {
            if (obj is ModelViewModel<fm_dd_file> file)
            {
                return file.Model.Favorite == Value;
            }
            return false;
        }

        public override string ToString()
        {
            return DisplayName ?? GetType().Name;
        }
    }
}
