using H.Controls.FilterBox;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.FileManager
{
    [Display(Name = "评分")]
    public class ScoreFileFilter : FilterBase
    {
        public int Value { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelBindable<fm_dd_file> file)
            {
                return file.Model.Score == this.Value;
            }
            return false;
        }
    }
}
