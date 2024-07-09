
using H.Controls.OrderBox;
using H.Mvvm;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.App.FileManager
{
    [Display(Name = "按视频预览图数量")]
    public class FileOrderByVideoImageCount : OrderBase
    {
        public override IEnumerable Where(IEnumerable from)
        {
            if (this.UseDesc)
                return from.OfType<IModelBindable<fm_dd_file>>().OrderByDescending(x =>
                {
                    if (x.Model is fm_dd_video video)
                        return video.Images.Count;
                    return 0;
                });
            return from.OfType<IModelBindable<fm_dd_file>>().OrderBy(x =>
            {
                if (x.Model is fm_dd_video video)
                    return video.Images.Count;
                return 0;
            });
        }
    }
}
