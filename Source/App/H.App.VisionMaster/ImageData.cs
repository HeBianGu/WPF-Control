using H.Mvvm;
using System.ComponentModel;
using System.Windows.Media;

namespace H.App.VisionMaster;

public interface IImageData
{
    string FilePath { get; set; }
    ImageSource ResultImageSource { get; set; }
}

public class ImageData : BindableBase, IImageData
{
    public ImageData(string filePath)
    {
        this.FilePath = filePath;
        //this.ResultImageSource = TypeDescriptor.GetConverter(typeof(ImageSource)).ConvertFromString(filePath) as ImageSource;
    }
    public string FilePath { get; set; }
    private ImageSource _resultImageSource;
    public ImageSource ResultImageSource
    {
        get { return _resultImageSource; }
        set
        {
            _resultImageSource = value;
            RaisePropertyChanged();
        }
    }
}
