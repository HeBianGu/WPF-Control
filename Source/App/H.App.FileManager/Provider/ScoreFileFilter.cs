
using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.App.FileManager
{
    public class ScoreFileFilter : IFilter
    {
        public string DisplayName { get; set; }
        public int Value { get; set; }
        public bool IsMatch(object obj)
        {
            if (obj is ModelViewModel<fm_dd_file> file)
            {
                return file.Model.Score == Value;
            }
            return false;
        }

        public override string ToString()
        {
            return DisplayName ?? GetType().Name;
        }
    }
}
