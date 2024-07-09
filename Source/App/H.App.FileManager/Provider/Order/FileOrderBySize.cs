
using H.Controls.OrderBox;
using H.Mvvm;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.App.FileManager
{
    [Display(Name = "按大小")]
    public class FileOrderBySize : OrderBase
    {
        public override IEnumerable Where(IEnumerable from)
        {
            if (this.UseDesc)
                return from.OfType<IModelBindable<fm_dd_file>>().OrderByDescending(x => x.Model.Size);
            return from.OfType<IModelBindable<fm_dd_file>>().OrderBy(x => x.Model.Size);
        }
    }
}
