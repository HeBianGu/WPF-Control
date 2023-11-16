// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace H.Controls.Diagram
{
    //public interface INoteDisplay
    //{
    //    FillStyle FillStyle { get; set; }

    //    BorderStyle BorderStyle { get; set; }

    //    EffectStyle EffectStyle { get; set; }
    //}

    //public class FillStyle : NotifyPropertyChangedBase
    //{
    //    public FillStyle()
    //    {
    //        this.Background = Application.Current.FindResource(BrushKeys.BackgroundDefault) as Brush;
    //    }

    //    private Color _color;
    //    /// <summary> 说明  </summary>
    //    public Color Color
    //    {
    //        get { return _color; }
    //        set
    //        {
    //            if (_color == value)
    //                return;

    //            _color = value;
    //            RaisePropertyChanged();

    //            this.Background = new SolidColorBrush(this.Color) { Opacity = this.Opacity };
    //        }
    //    }

    //    private double _opacity = 1;
    //    /// <summary> 说明  </summary>
    //    public double Opacity
    //    {
    //        get { return _opacity; }
    //        set
    //        {
    //            if (_opacity == value)
    //                return;
    //            _opacity = value;
    //            RaisePropertyChanged();

    //            this.Background = new SolidColorBrush(this.Color) { Opacity = this.Opacity };
    //        }
    //    }

    //    private Brush _background;
    //    /// <summary> 说明  </summary>
    //    public Brush Background
    //    {
    //        get { return _background; }
    //        set
    //        {
    //            _background = value;
    //            RaisePropertyChanged();

    //            if (value is SolidColorBrush solid)
    //            {
    //                this.Color = solid.Color;
    //                this.Opacity = solid.Opacity;
    //            }
    //        }
    //    }
    //}

    //public class BorderStyle: FillStyle
    //{
    //    public Thickness BorderThickness { get; set; }

    //    public CornerRadius CornerRadius { get; set; }
    //}

    //public class EffectStyle
    //{
    //    public double BlurRadius { get; set; }

    //    public double Direction { get; set; }

    //    public RenderingBias RenderingBias { get; set; }

    //    public double ShadowDepth { get; set; }
    //}
}
