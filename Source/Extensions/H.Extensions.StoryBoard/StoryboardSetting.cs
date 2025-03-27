// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.StoryBoard;

public static class StoryboardSetting
{
    [DefaultValue(20)]
    [Range(0, 60)]
    public static int DesiredFrameRate { get; set; } = 25;
}
