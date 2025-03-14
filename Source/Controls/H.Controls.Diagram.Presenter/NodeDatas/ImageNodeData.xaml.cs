using H.Extensions.FontIcon;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace H.Controls.Diagram.Presenter.NodeDatas;

public interface IFilePathable
{
    string SrcFilePath { get; set; }
}

public interface IImageNodeData
{
    ImageSource ImageSource { get; set; }
}

public abstract class ImageNodeDataBase : FlowableNodeData, IImageNodeData
{
    public ImageNodeDataBase()
    {
        this.ImageSource = this.CreateImageSource();
    }

    [Icon(FontIcons.OpenFile)]
    [Display(Name = "浏览文件", GroupName = "操作,工具")]
    public DisplayCommand OpenCommand => new DisplayCommand(e =>
    {
        var r = IocMessage.IOFileDialog.ShowOpenFile(x => x.Filter = this.GetFilter());
        if (r == null)
            return;
        this.OpenFilePath(r);
    });

    protected virtual string GetFilter()
    {
        return "图片|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;";
    }

    protected virtual void OpenFilePath(string name)
    {
        this.FilePath = name;
    }

    private ImageSource _imageSource;
    [XmlIgnore]
    [Browsable(false)]
    public ImageSource ImageSource
    {
        get { return _imageSource; }
        set
        {
            _imageSource = value;
            RaisePropertyChanged();
        }
    }

    private string _filePath;
    [Display(Name = "文件路径", GroupName = "常用")]
    [ReadOnly(true)]
    public string FilePath
    {
        get { return _filePath; }
        set
        {
            if (_filePath == value)
                return;
            _filePath = value;
            RaisePropertyChanged();

            if (string.IsNullOrEmpty(value))
                return;
            Application.Current.Dispatcher.Invoke(() =>
              {
                  this.ImageSource = this.CreateImage(value);
              });
        }
    }

    private double _titleFontSize = 15;
    [Display(Name = "字号", GroupName = "常用")]
    public double TitleFontSize
    {
        get { return _titleFontSize; }
        set
        {
            _titleFontSize = value;
            RaisePropertyChanged();
        }
    }

    protected virtual ImageSource CreateImage(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
        if (uri == null)
            return null;
        try
        {
            BitmapImage bitmap = new BitmapImage(uri);
            return bitmap;
        }
        catch (Exception ex)
        {
            IocLog.Instance?.Error(ex);
            return null;
        }
    }

    protected abstract ImageSource CreateImageSource();
}

public class ImageNodeData : ImageNodeDataBase
{
    protected override ImageSource CreateImageSource()
    {
        return this.ImageSource;
    }
}
