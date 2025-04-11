namespace H.Modules.Guide;

public interface IGuideOptions
{
    int AnimationDuration { get; set; }
    Color CoverColor { get; set; }
    double CoverOpacity { get; set; }
    Brush Stroke { get; set; }
    DoubleCollection StrokeDashArray { get; set; }
    double StrokeThickness { get; set; }
    double TextMaxWidth { get; set; }
    bool UseOnLoad { get; set; }
}
