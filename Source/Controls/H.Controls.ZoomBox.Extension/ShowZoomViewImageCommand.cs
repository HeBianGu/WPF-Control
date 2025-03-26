using H.Mvvm;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;
using H.Services.Common;
using H.Common.Attributes;
using H.Common.Commands;
using H.Services.Message;

namespace H.Controls.ZoomBox.Extension
{
    [Icon("\xEB9F")]
    [Display(Name = "图片")]
    public class ShowZoomViewImageCommand : DisplayMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is ImageSource source)
                await IocMessage.Dialog.ShowZoomViewImage(source);
            if (parameter is string filePath)
                await IocMessage.Dialog.ShowZoomViewImage(filePath);
        }
    }
}
