using H.Controls.FilterBox;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.FileManager
{
    [Display(Name = "观看")]
    public class WatchedFileFilter : FilterBase
    {
        public bool Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelBindable<fm_dd_file> file)
            {
                return file.Model.Watched == this.Value;
            }
            return false;
        }
    }
}
