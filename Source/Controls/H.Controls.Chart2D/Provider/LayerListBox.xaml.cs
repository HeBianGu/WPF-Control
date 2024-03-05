// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Chart2D
{
    /// <summary> 曲线视图 </summary>
    public class LayerListBox : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LayerListBox), "S.LayerListBox.Default");
        public static ComponentResourceKey PointKey => new ComponentResourceKey(typeof(LayerListBox), "S.LayerListBox.Point.Default");

        static LayerListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayerListBox), new FrameworkPropertyMetadata(typeof(LayerListBox)));
        }
    }
}
