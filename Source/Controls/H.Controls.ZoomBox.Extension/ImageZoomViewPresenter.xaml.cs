using H.Mvvm.ViewModels.Base;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ZoomBox.Extension
{
    [Icon("\xEB9F")]
    [Display(Name = "图片")]
    public class ImageZoomViewPresenter : DisplayBindableBase, IImageZoomViewPresenter
    {
        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged();
            }
        }
    }
}
