using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace H.Controls.Diagram.Presenter.NodeDatas;

public abstract class ImageNodeDataBase : FlowableNodeData
{
    public ImageNodeDataBase()
    {
        this.ImageSource = this.CreateImageSource();
    }

    [XmlIgnore]
    [Display(Name = "浏览文件", GroupName = "操作,工具")]
    public RelayCommand OpenCommand => new RelayCommand((s, e) =>
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
        openFileDialog.Filter = this.GetFilter();
        openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
        openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
        openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
        openFileDialog.Multiselect = false;//设置多选
        if (openFileDialog.ShowDialog() != true) return;

        this.OpenFilePath(openFileDialog.FileName);
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
