// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows;

namespace H.Controls.Adorner.Adorner;

public interface IDynimacAdorner
{
    Point Offset { get; set; }
    void UpdatePosition(Point location);
}
