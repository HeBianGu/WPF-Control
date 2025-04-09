using H.Services.Common.MainWindow;
using System.Windows.Media;

namespace H.Styles.Controls;

public class WindowKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(WindowKeys), "S.Window.Default");

    [Obsolete("用FontIconButton替换")]
    public static ComponentResourceKey Button => new ComponentResourceKey(typeof(WindowKeys), "S.Window.Button");
    public static ComponentResourceKey FontIconButton => new ComponentResourceKey(typeof(WindowKeys), "S.Window.FontIconButton");

    public static ComponentResourceKey WindowChrome => new ComponentResourceKey(typeof(WindowKeys), "S.Window.WindowChrome");

}

[Display(Name = "窗口设置", GroupName = SettingGroupNames.GroupControl, Description = "设置窗口参数")]
public class WindowSetting : Settable<WindowSetting>
{
    private string _backImagePath;
    [DefaultValue("pack://application:,,,/H.Extensions.BackgroundImage;component/b41.png")]
    [Display(Name = "窗口背景图片")]
    public string BackImagePath
    {
        get { return _backImagePath; }
        set
        {
            _backImagePath = value;
            RaisePropertyChanged();
        }
    }

    private bool _useBackImage;
    [DefaultValue(false)]
    [Display(Name = "启用窗口背景图片")]
    public bool UseBackImage
    {
        get { return _useBackImage; }
        set
        {
            _useBackImage = value;
            RaisePropertyChanged();
        }
    }


    private double _opacity;
    [DefaultValue(0.3)]
    [Display(Name = "图片透明度")]
    public double Opacity
    {
        get { return _opacity; }
        set
        {
            _opacity = value;
            RaisePropertyChanged();
        }
    }


    private Stretch _stretch;
    [DefaultValue(Stretch.UniformToFill)]
    [Display(Name = "图片拉伸")]
    public Stretch Stretch
    {
        get { return _stretch; }
        set
        {
            _stretch = value;
            RaisePropertyChanged();
        }
    }

    private bool _useNoticeOnMainWindowClose;
    [DefaultValue(true)]
    [Display(Name = "主窗口关闭提示", Description = "当主窗口点击关闭时会提示是否关闭窗口")]
    public bool UseNoticeOnMainWindowClose
    {
        get { return _useNoticeOnMainWindowClose; }
        set
        {
            _useNoticeOnMainWindowClose = value;
            RaisePropertyChanged();
        }
    }

    private bool _useSaveOnMainWindowClose;
    [DefaultValue(true)]
    [Display(Name = "主窗口关闭保存数据", Description = "当主窗口点击关闭时主窗口关闭保存数据")]
    public bool UseSaveOnMainWindowClose
    {
        get { return _useSaveOnMainWindowClose; }
        set
        {
            _useSaveOnMainWindowClose = value;
            RaisePropertyChanged();
        }
    }
}


[Display(Name = "主窗口设置", GroupName = SettingGroupNames.GroupControl, Description = "设置主窗口设置参数")]
public class MainWindowOption : IocOptionInstance<MainWindowOption>
{
    private double _width;
    [ReadOnly(true)]
    [Display(Name = "宽度", Description = "设置主窗口高度")]
    [DefaultValue(1100.0)]
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            RaisePropertyChanged();
        }
    }

    private double _height;
    [ReadOnly(true)]
    [Display(Name = "高度", Description = "设置主窗口高度")]
    [DefaultValue(700.0)]
    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            RaisePropertyChanged();
        }
    }

    private WindowStartupLocation _WindowStartupLocation;
    [ReadOnly(true)]
    [Display(Name = "位置", Description = "设置主窗口居中位置")]
    [DefaultValue(WindowStartupLocation.CenterScreen)]
    public WindowStartupLocation WindowStartupLocation
    {
        get { return _WindowStartupLocation; }
        set
        {
            _WindowStartupLocation = value;
            RaisePropertyChanged();
        }
    }

    private WindowState _windowState;
    [ReadOnly(true)]
    [Display(Name = "状态", Description = "设置主窗口状态，最大、最小和常规")]
    [DefaultValue(WindowState.Normal)]
    public WindowState WindowState
    {
        get { return _windowState; }
        set
        {
            _windowState = value;
            RaisePropertyChanged();
        }
    }
}

public class MainWindowSavableService : IMainWindowSavableService
{
    public string Name => "主窗口配置";

    public void Load(Window window)
    {
        window.WindowStartupLocation = MainWindowOption.Instance.WindowStartupLocation;
        window.WindowState = MainWindowOption.Instance.WindowState;
        window.Width = MainWindowOption.Instance.Width;
        window.Height = MainWindowOption.Instance.Height;
    }

    public bool Save(out string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (Application.Current.MainWindow is Window window)
            {
                MainWindowOption.Instance.WindowStartupLocation = window.WindowStartupLocation;
                MainWindowOption.Instance.WindowState = window.WindowState;
                MainWindowOption.Instance.Width = window.Width;
                MainWindowOption.Instance.Height = window.Height;
            }
        });
        return MainWindowOption.Instance.Save(out message);
    }
}
