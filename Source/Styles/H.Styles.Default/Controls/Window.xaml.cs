using H.Extensions.Setting;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace H.Styles.Default
{
    public class WindowKeys
    {
        public static ComponentResourceKey Button => new ComponentResourceKey(typeof(WindowKeys), "S.Window.Button");
        public static ComponentResourceKey WindowChrome => new ComponentResourceKey(typeof(WindowKeys), "S.Window.WindowChrome");
    }

    [Display(Name = "窗口设置", GroupName = SettingGroupNames.GroupSystem, Description = "设置窗口参数")]
    public class WindowSetting : Setting<WindowSetting>
    {
        private string _backImagePath;
        [DefaultValue("pack://application:,,,/H.Extensions.BackgroundImage;component/b41.png")]
        [Displayer(Name = "窗口背景图片")]
        public string BackImagePath
        {
            get { return _backImagePath; }
            set
            {
                _backImagePath = value;
                RaisePropertyChanged();
            }
        }

        private double _opacity;
        [DefaultValue(0.3)]
        [Displayer(Name = "图片透明度")]
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
        [Displayer(Name = "图片拉伸")]
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
        [Displayer(Name = "主窗口关闭提示", Description = "当主窗口点击关闭时会提示是否关闭窗口")]
        public bool UseNoticeOnMainWindowClose
        {
            get { return _useNoticeOnMainWindowClose; }
            set
            {
                _useNoticeOnMainWindowClose = value;
                RaisePropertyChanged();
            }
        }

    }

    public static partial class Extension
    {
        public static IApplicationBuilder UseWindowSetting(this IApplicationBuilder builder, Action<WindowSetting> option = null)
        {
            SettingDataManager.Instance.Add(WindowSetting.Instance);
            option?.Invoke(WindowSetting.Instance);
            return builder;
        }
    }

}
