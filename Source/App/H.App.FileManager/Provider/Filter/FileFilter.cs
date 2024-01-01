using H.Controls.FilterBox;
using H.Providers.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace H.App.FileManager
{
    [Display(Name = "扩展名")]
    public class FileFilter : FilterBase
    {
        public string Extensions { get; set; }
        public override bool IsMatch(object obj)
        {
            if (obj is ModelViewModel<fm_dd_file> file)
            {
                return Extensions.Split(' ').Any(x => x.Equals(file.Model.Extend?.Trim('.'), StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }
}
