
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Linq;

namespace H.App.FileManager
{
    public class FileFilter : IFilter
    {
        public string DisplayName { get; set; }
        public string Extensions { get; set; }
        public bool IsMatch(object obj)
        {
            if (obj is ModelViewModel<fm_dd_file> file)
            {
                return Extensions.Split(' ').Any(x => x.Equals(file.Model.Extend.Trim('.'), StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }

        public override string ToString()
        {
            return DisplayName ?? GetType().Name;
        }
    }
}
