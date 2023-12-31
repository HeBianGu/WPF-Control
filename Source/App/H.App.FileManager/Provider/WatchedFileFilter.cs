
using H.Controls.FilterBox;
using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace H.App.FileManager
{
    [Display(Name = "观看")]
    public class WatchedFileFilter : FilterBase
    {
        public bool Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelViewModel<fm_dd_file> file)
            {
                return file.Model.Watched == Value;
            }
            return false;
        }
    }
}
