global using H.Extensions.Setting;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;

namespace H.Styles.Controls;

public class ScrollViewerKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Default");
}

[Display(Name = "滚动条设置", GroupName = SettingGroupNames.GroupControl, Description = "设置滚动条参数")]
public class ScrollViewerSetting : Settable<ScrollViewerSetting>
{
    private double _width = 12.0;
    [Range(10.0, 80.0)]
    [DefaultValue(12.0)]
    [Display(Name = "滚动条宽度", Description = "设置全局滚动条宽度")]
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            RaisePropertyChanged();
        }
    }
}

public static partial class Extension
{
    public static IApplicationBuilder UseScrollViewerSetting(this IApplicationBuilder builder, Action<ScrollViewerSetting> option = null)
    {
        IocSetting.Instance.Add(ScrollViewerSetting.Instance);
        option?.Invoke(ScrollViewerSetting.Instance);
        return builder;
    }
}
