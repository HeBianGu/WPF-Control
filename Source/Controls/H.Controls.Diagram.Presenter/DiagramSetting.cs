// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Setting;
using H.Services.Setting;
using H.Themes;

namespace H.Controls.Diagram.Presenter;

[Display(Name = "绘图面板", GroupName = SettingGroupNames.GroupApp)]
public class DiagramSetting : LazySettableInstance<DiagramSetting>
{
    private Color _symbolDefaultBackground = (Application.Current.FindResource(BrushKeys.Foreground) as SolidColorBrush).Color;
    [Display(Name = "符号默认背景色")]
    public Color SymbolDefaultBackground
    {
        get { return _symbolDefaultBackground; }
        set
        {
            _symbolDefaultBackground = value;
            RaisePropertyChanged();
        }
    }

    private Color _nodeDataDefaultFill = (Application.Current.FindResource(BrushKeys.Background) as SolidColorBrush).Color;
    [Display(Name = "节点默认背景色")]
    public Color NodeDataDefaultFill
    {
        get { return _nodeDataDefaultFill; }
        set
        {
            _nodeDataDefaultFill = value;
            RaisePropertyChanged();
        }
    }

    private Color _nodeDataDefaultStroke = (Application.Current.FindResource(BrushKeys.Foreground) as SolidColorBrush).Color;
    [Display(Name = "节点默认前景色")]
    public Color NodeDataDefaultStroke
    {
        get { return _nodeDataDefaultStroke; }
        set
        {
            _nodeDataDefaultStroke = value;
            RaisePropertyChanged();
        }
    }

    private Color _nodeDataDefaultForeground = (Application.Current.FindResource(BrushKeys.Foreground) as SolidColorBrush).Color;
    [Display(Name = "节点默认字体色")]
    public Color NodeDataDefaultForeground
    {
        get { return _nodeDataDefaultForeground; }
        set
        {
            _nodeDataDefaultForeground = value;
            RaisePropertyChanged();
        }
    }
}
