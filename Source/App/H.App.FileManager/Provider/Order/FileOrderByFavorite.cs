
using H.Controls.OrderBox;
using H.Providers.Mvvm;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.App.FileManager
{
    [Display(Name = "按喜欢")]
    public class FileOrderByFavorite : OrderBase
    {
        public override IEnumerable Where(IEnumerable from)
        {
            if (this.UseDesc)
                return from.OfType<IModelViewModel<fm_dd_file>>().OrderByDescending(x => x.Model.Favorite);
            return from.OfType<IModelViewModel<fm_dd_file>>().OrderBy(x => x.Model.Favorite);

        }
    }
}
