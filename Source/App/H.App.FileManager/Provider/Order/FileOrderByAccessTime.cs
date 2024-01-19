
using H.Controls.OrderBox;
using H.Providers.Mvvm;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace H.App.FileManager
{
    [Display(Name = "按访问时间")]
    public class FileOrderByAccessTime : OrderBase
    {
        public override IEnumerable Where(IEnumerable from)
        {
            if (this.UseDesc)
                return from.OfType<IModelViewModel<fm_dd_file>>().OrderByDescending(x => x.Model.UDATE);
            return from.OfType<IModelViewModel<fm_dd_file>>().OrderBy(x => x.Model.UDATE);

        }
    }
}
