using H.Controls.FilterBox;
using H.Mvvm;

namespace H.App.FileManager
{
    public class FavoriteFileFilter : FilterBase
    {
        public bool Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelBindable<fm_dd_file> file)
            {
                return file.Model.Favorite == this.Value;
            }
            return false;
        }
    }
}
