using H.Controls.FilterBox;
using H.Mvvm;

namespace H.App.FileManager
{
    public class SeeLaterFileFilter : FilterBase
    {
        public bool Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelBindable<fm_dd_file> file)
            {
                return file.Model.SeeLater == this.Value;
            }
            return false;
        }
    }
}
