// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


using H.Components.VisionDiagram.Base;
using H.Controls.ShapeBox.State.Base;

namespace H.Components.VisionDiagram.NodeDatas;
public abstract class NinePointCalibrationNodeData<T> : ROINodeData<T>, INinePointCalibrationNodeData where T : class, IVisionImage
{
    private readonly List<Point> _pixelPoints = new();
    private readonly List<Point> _worldPoints = new();

    // 3x3 Homography matrix (pixel -> world)
    // [xw]   [h11 h12 h13] [xp]
    // [yw] = [h21 h22 h23] [yp]  / (h31*xp + h32*yp + h33)
    // [1 ]   [h31 h32 h33] [1 ]
    private double[,] _h;

    [JsonIgnore]
    IReadOnlyList<Point> INinePointCalibrationNodeData.PixelPoints => this.PixelPointCollection?.ToList().AsReadOnly() ?? new List<Point>().AsReadOnly();
    [JsonIgnore]
    IReadOnlyList<Point> INinePointCalibrationNodeData.WorldPoints => this.WorldPointCollection?.ToList().AsReadOnly() ?? new List<Point>().AsReadOnly();

    private PointCollection _PixelPointCollection;
    [Tab(VisionTabNames.RunParameters)]
    [ReadOnly(true)]
    [Display(Name = "标定像素坐标点集合", GroupName = VisionTabNames.RunParameters, Description = "标定的像素点集合")]
    public PointCollection PixelPointCollection
    {
        get { return _PixelPointCollection; }
        set
        {
            _PixelPointCollection = value;
            RaisePropertyChanged();
        }
    }

    private PointCollection _WorldPointCollection;
    [Tab(VisionTabNames.RunParameters)]
    [ReadOnly(true)]
    [Display(Name = "标定世界坐标点集合", GroupName = VisionTabNames.RunParameters, Description = "标定的像素点集合")]
    public PointCollection WorldPointCollection
    {
        get
        {
            return _WorldPointCollection;
        }
        set
        {
            _WorldPointCollection = value;
            RaisePropertyChanged();
        }
    }

    public bool IsValid => _h != null && _pixelPoints.Count >= 4 && _pixelPoints.Count == _worldPoints.Count;


    protected override IEnumerable<IViewState> CreateViewStates()
    {
        foreach (var item in base.CreateViewStates())
        {
            yield return item;
        }
        yield return new DrawNinePointCalibrationState(this);
    }

    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        return base.Invoke(previors, diagram);
    }

    public void SetPoints(IEnumerable<Point> pixelPoints, IEnumerable<Point> worldPoints)
    {
        _pixelPoints.Clear();
        _worldPoints.Clear();

        if (pixelPoints != null)
            _pixelPoints.AddRange(pixelPoints);

        if (worldPoints != null)
            _worldPoints.AddRange(worldPoints);

        this.PixelPointCollection = new PointCollection(pixelPoints);
        this.WorldPointCollection = new PointCollection(worldPoints);
        Recalculate();
        RaisePropertyChanged(nameof(IsValid));
        RaisePropertyChanged(nameof(PixelPointCollection));
        RaisePropertyChanged(nameof(WorldPointCollection));
    }

    public Point PixelToWorld(Point pixelPoint)
    {
        if (!IsValid)
            return default;

        double x = pixelPoint.X;
        double y = pixelPoint.Y;

        double w = _h[2, 0] * x + _h[2, 1] * y + _h[2, 2];
        if (Math.Abs(w) < 1e-12)
            return default;

        double wx = (_h[0, 0] * x + _h[0, 1] * y + _h[0, 2]) / w;
        double wy = (_h[1, 0] * x + _h[1, 1] * y + _h[1, 2]) / w;

        return new Point(wx, wy);
    }

    private void Recalculate()
    {
        _h = null;

        if (_pixelPoints.Count < 4)
            return;

        if (_pixelPoints.Count != _worldPoints.Count)
            return;

        _h = HomographySolver.Solve(_pixelPoints, _worldPoints);
    }

    private static class HomographySolver
    {
        public static double[,] Solve(IReadOnlyList<Point> src, IReadOnlyList<Point> dst)
        {
            // Using DLT with h33 = 1 constraint -> solve linear system of 8 unknowns
            // For each correspondence:
            // x' = (h11 x + h12 y + h13) / (h31 x + h32 y + 1)
            // y' = (h21 x + h22 y + h23) / (h31 x + h32 y + 1)
            //
            // Rearranged:
            // [ x y 1 0 0 0 -x*x' -y*x' ] [h11 h12 h13 h21 h22 h23 h31 h32]^T = x'
            // [ 0 0 0 x y 1 -x*y' -y*y' ] [...] = y'

            int n = src.Count;
            int rows = n * 2;
            int cols = 8;

            var a = new double[rows, cols];
            var b = new double[rows];

            for (int i = 0; i < n; i++)
            {
                double x = src[i].X;
                double y = src[i].Y;
                double xp = dst[i].X;
                double yp = dst[i].Y;

                int r1 = i * 2;
                int r2 = r1 + 1;

                a[r1, 0] = x;
                a[r1, 1] = y;
                a[r1, 2] = 1;
                a[r1, 3] = 0;
                a[r1, 4] = 0;
                a[r1, 5] = 0;
                a[r1, 6] = -x * xp;
                a[r1, 7] = -y * xp;
                b[r1] = xp;

                a[r2, 0] = 0;
                a[r2, 1] = 0;
                a[r2, 2] = 0;
                a[r2, 3] = x;
                a[r2, 4] = y;
                a[r2, 5] = 1;
                a[r2, 6] = -x * yp;
                a[r2, 7] = -y * yp;
                b[r2] = yp;
            }

            // Least squares: solve (A^T A) h = (A^T b)
            var ata = MultiplyTransposeSelf(a);
            var atb = MultiplyTransposeVector(a, b);

            double[] h = SolveLinearSystem(ata, atb);

            return new double[,]
            {
                { h[0], h[1], h[2] },
                { h[3], h[4], h[5] },
                { h[6], h[7], 1.0 }
            };
        }

        private static double[,] MultiplyTransposeSelf(double[,] a)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);

            var result = new double[cols, cols];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < rows; k++)
                    {
                        sum += a[k, i] * a[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        private static double[] MultiplyTransposeVector(double[,] a, double[] b)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);

            var result = new double[cols];

            for (int i = 0; i < cols; i++)
            {
                double sum = 0;
                for (int k = 0; k < rows; k++)
                {
                    sum += a[k, i] * b[k];
                }
                result[i] = sum;
            }

            return result;
        }

        private static double[] SolveLinearSystem(double[,] m, double[] v)
        {
            // Gaussian elimination with partial pivoting
            int n = v.Length;
            var a = (double[,])m.Clone();
            var b = (double[])v.Clone();

            for (int i = 0; i < n; i++)
            {
                int pivot = i;
                double max = Math.Abs(a[i, i]);

                for (int r = i + 1; r < n; r++)
                {
                    double value = Math.Abs(a[r, i]);
                    if (value > max)
                    {
                        max = value;
                        pivot = r;
                    }
                }

                if (max < 1e-12)
                    throw new InvalidOperationException("Homography solve failed: matrix is singular.");

                if (pivot != i)
                {
                    SwapRows(a, i, pivot);
                    (b[i], b[pivot]) = (b[pivot], b[i]);
                }

                double diag = a[i, i];
                for (int c = i; c < n; c++)
                    a[i, c] /= diag;
                b[i] /= diag;

                for (int r = 0; r < n; r++)
                {
                    if (r == i)
                        continue;

                    double factor = a[r, i];
                    if (Math.Abs(factor) < 1e-12)
                        continue;

                    for (int c = i; c < n; c++)
                        a[r, c] -= factor * a[i, c];

                    b[r] -= factor * b[i];
                }
            }

            return b;
        }

        private static void SwapRows(double[,] a, int r1, int r2)
        {
            int cols = a.GetLength(1);
            for (int c = 0; c < cols; c++)
                (a[r1, c], a[r2, c]) = (a[r2, c], a[r1, c]);
        }
    }
}


