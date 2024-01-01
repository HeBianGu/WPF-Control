using H.Controls.FilterBox;
using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.FileManager
{
    [Display(Name = "按清晰度")]
    public class FavoriteFileFilter : FilterBase
    {
        public bool Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelViewModel<fm_dd_file> file)
            {
                return file.Model.Favorite == Value;
            }
            return false;
        }
    }
}
