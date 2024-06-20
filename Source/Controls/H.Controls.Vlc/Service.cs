// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Vlc
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum FullScreenType
    {
        [Display(Name = "鼠标悬停")]
        MouseOver,
        [Display(Name = "默认")]
        Default,
        [Display(Name = "无")]
        None,
        [Display(Name = "鹰眼")]
        ScrollViewerTransfor
    }
}
