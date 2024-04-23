using H.Controls.FilterBox;
using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.FileManager
{
    public class FavoriteFileFilter : FilterBase
    {
        public bool Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelBindable<fm_dd_file> file)
            {
                return file.Model.Favorite == Value;
            }
            return false;
        }
    }
}
