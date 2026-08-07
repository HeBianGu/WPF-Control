using H.Mvvm.ViewModels.Base;

namespace H.Components.VisionDiagrams.OpenCV.Base.Calibration;

public class PixelWorldPointKeyValuePair : BindableBase
{
    private double _PixelPointX;
    public double PixelPointX
    {
        get { return _PixelPointX; }
        set
        {
            _PixelPointX = value;
            RaisePropertyChanged();
        }
    }


    private double _PixelPointY;

    public double PixelPointY
    {
        get { return _PixelPointY; }
        set
        {
            _PixelPointY = value;
            RaisePropertyChanged();
        }
    }


    private double _WorldPointX;

    public double WorldPointX
    {
        get { return _WorldPointX; }
        set
        {
            _WorldPointX = value;
            RaisePropertyChanged();
        }
    }


    private double _WorldPointY;

    public double WorldPointY
    {
        get { return _WorldPointY; }
        set
        {
            _WorldPointY = value;
            RaisePropertyChanged();
        }
    }


    private double _Angle;

    public double Angle
    {
        get { return _Angle; }
        set
        {
            _Angle = value;
            RaisePropertyChanged();
        }
    }


    public WpfPoint PixelPoint => new System.Windows.Point(this.PixelPointX, this.PixelPointY);
    public WpfPoint WorldPoint => new System.Windows.Point(this.WorldPointX, this.WorldPointY);

    public bool IsVaild()
    {
        return this.PixelPointX != 0 || this.PixelPointY != 0 || this.WorldPointX != 0 || this.WorldPointY != 0;
    }

}
