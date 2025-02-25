using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace H.Styles.Default
{
    public class ScrollViewerKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ScrollViewerKeys), "S.ScrollViewer.Default");
    }

    [Display(Name = "滚动条设置", GroupName = SettingGroupNames.GroupControl, Description = "设置滚动条参数")]
    public class ScrollViewerSetting : Extensions.Setting.Settable<ScrollViewerSetting>
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
            SettingDataManager.Instance.Add(ScrollViewerSetting.Instance);
            option?.Invoke(ScrollViewerSetting.Instance);
            return builder;
        }
    }
}
