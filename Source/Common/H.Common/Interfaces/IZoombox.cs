namespace H.Common.Interfaces;

public interface IZoombox
{
    void FillToBounds();
    void FitToBounds();
    void Zoom(double percentage);
    void Zoom(double percentage, Point relativeTo);
    void ZoomTo(double scale);
    void ZoomTo(double scale, Point relativeTo);
    void ZoomTo(Point position);
    void ZoomTo(Rect region);
}