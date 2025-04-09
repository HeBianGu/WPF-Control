global using H.Services.Message.IODialog;
global using System.ComponentModel.DataAnnotations;
global using H.Common.Attributes;
global using H.Common.Commands;
global using H.Services.Message;

namespace H.Controls.ZoomBox.Extension
{
    [Icon("\xEB9F")]
    [Display(Name = "图片")]
    public class ShowZoomViewImageFileCommand : DisplayMarkupCommandBase
    {
        public string FilePath { get; set; }
        public bool UseCache { get; set; } = true;
        public override async Task ExecuteAsync(object parameter)
        {
            var path = parameter?.ToString() ?? this.FilePath;
            if (string.IsNullOrEmpty(path))
            {
                IocMessage.IOFileDialog.ShowOpenImageFile(x =>
                {
                    path = x;
                    if (this.UseCache)
                        this.FilePath = path;
                });
            }
            await IocMessage.Dialog.ShowZoomViewImage(path);
        }
    }
}
