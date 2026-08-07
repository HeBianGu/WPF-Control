// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Controls.ShapeBox;
using H.Controls.ShapeBox.Extension;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State.Adds.Base;
using H.Controls.ShapeBox.State.Base;
using H.Services.Message;
using System.Windows.Input;

namespace H.Components.VisionDiagram.Base;
[Icon(FontIcons.StatusSecured)]
[Display(Name = "绘制九点标定")]
public class DrawNinePointCalibrationState : AddShapeState<CalibrationPointShape>
{
    private const int CalibrationPointCount = 9;

    private readonly INinePointCalibrationNodeData _calibrationNodeData;

    public DrawNinePointCalibrationState(INinePointCalibrationNodeData calibrationNodeData)
    {
        _calibrationNodeData = calibrationNodeData;

    }

    public override void Enter()
    {
        base.Enter();
        this._shapes = this.CreateShapes(_calibrationNodeData.PixelPoints).ToList();
    }

    private IEnumerable<IShape> CreateShapes(IEnumerable<Point> points)
    {
        List<IShape> shapes = new List<IShape>();
        for (int i = 0; i < points.Count(); i++)
        {
            var p = points.ElementAt(i);
            var shape = CreateShape();
            shape.Center = p;
            shape.Index = i + 1;
            shapes.Add(shape);
        }
        this.Shape.Index = points.Count() + 1;
        return shapes;
    }

    public DrawNinePointCalibrationState(INinePointCalibrationNodeData calibrationNodeData, IShapes shapes) : base(shapes)
    {
        _calibrationNodeData = calibrationNodeData;
    }

    private double _worldOriginX;
    [Unit("mm")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "世界坐标原点X", Description = "左上角/第一个点的世界坐标X")]
    public double WorldOriginX
    {
        get { return _worldOriginX; }
        set
        {
            _worldOriginX = value;
            RaisePropertyChanged();
        }
    }

    private double _worldOriginY;
    [Unit("mm")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "世界坐标原点Y", Description = "左上角/第一个点的世界坐标Y")]
    public double WorldOriginY
    {
        get { return _worldOriginY; }
        set
        {
            _worldOriginY = value;
            RaisePropertyChanged();
        }
    }

    private double _stepX = 10;
    [Unit("mm")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "X间距", Description = "网格列间距")]
    [Range(0.0001, double.MaxValue)]
    public double StepX
    {
        get { return _stepX; }
        set
        {
            _stepX = value;
            RaisePropertyChanged();
        }
    }

    private double _stepY = 10;
    [Unit("mm")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "Y间距", Description = "网格行间距")]
    [Range(0.0001, double.MaxValue)]
    public double StepY
    {
        get { return _stepY; }
        set
        {
            _stepY = value;
            RaisePropertyChanged();
        }
    }

    private int _rows = 3;
    [Display(Name = "行数")]
    [Range(1, 10)]
    public int Rows
    {
        get { return _rows; }
        set
        {
            _rows = value;
            RaisePropertyChanged();
        }
    }

    private int _cols = 3;
    [Display(Name = "列数")]
    [Range(1, 10)]
    public int Cols
    {
        get { return _cols; }
        set
        {
            _cols = value;
            RaisePropertyChanged();
        }
    }

    protected override CalibrationPointShape CreateShape()
    {
        var r = new CalibrationPointShape
        {
            Radius = 60,
            UseDimension = false,
            UseCross = true
        };
        r.TitleBackground = Brushes.Red;
        r.TitleForeground = Brushes.Chartreuse;
        return r;
    }

    private List<IShape> _shapes = new List<IShape>();

    protected override void ClearStateShape()
    {
        this._shapes.Clear();
        base.ClearStateShape();
    }
    //protected override void OnClick(Queue<Point> points)
    //{
    //    this._shapes = this.CreateShapes(points).ToList();
    //    if (points.Count >= CalibrationPointCount)
    //        Sumit();
    //}

    protected override async void Sumit()
    {
        if (_clickPoints.Count != CalibrationPointCount)
        {
            Cancel();
            await IocMessage.ShowDialogMessage($"九点标定需要采集 {CalibrationPointCount} 个点，当前：{_clickPoints.Count}");
            return;
        }

        // 让用户输入世界坐标网格参数，然后生成 3x3 世界点
        bool? r = await IocMessage.Form.ShowEdit(this, null, null, x => x.UseCommand = false);
        if (r != true)
        {
            Cancel();
            return;
        }

        if (Rows * Cols != CalibrationPointCount)
        {
            Cancel();
            await IocMessage.ShowDialogMessage($"行×列必须等于 {CalibrationPointCount}（当前 {Rows}×{Cols}={Rows * Cols}）");
            return;
        }

        var pixelPoints = _clickPoints.ToList();
        var worldPoints = BuildWorldGridPoints(WorldOriginX, WorldOriginY, StepX, StepY, Rows, Cols);

        _calibrationNodeData.SetPoints(pixelPoints, worldPoints);
        Clear();
    }

    public override void Cancel()
    {
        base.Cancel();
        ClearStateShape();
    }

    private static List<Point> BuildWorldGridPoints(double originX, double originY, double stepX, double stepY, int rows, int cols)
    {
        var result = new List<Point>(rows * cols);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                double x = originX + (c * stepX);
                double y = originY + (r * stepY);
                result.Add(new Point(x, y));
            }
        }

        return result;
    }

    //protected override void OnPreviewMouseMove(Point p)
    //{
    //    this.Shape.Center = p;
    //    this.DrawPreviewShape();
    //}

    public override void MouseMove(object sender, MouseEventArgs e)
    {
        base.MouseMove(sender, e);
    }

    //protected override IEnumerable<IPreviewShape> GetPreviewShapes()
    //{
    //    return base.GetPreviewShapes().Concat(this._shapes.OfType<IPreviewShape>());
    //}

    protected override IEnumerable<IShape> GetStateShapes()
    {
        foreach (var item in this._shapes.OfType<CalibrationPointShape>())
        {
            item.Radius = 12 / this.GetStateShapeView().Scale;
        }
        return base.GetStateShapes().Concat(this._shapes);
    }
}

/// <summary>
/// 九点标定显示用的点（最小实现）
/// </summary>
public class CalibrationPointShape : CircleShape
{
    public int Index { get; set; }
    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        base.MatrixDrawing(view, drawingContext, pen, fill);

        var topp = new Point(this.Center.X, this.Center.Y - this.Radius - 2 / view.Scale);
        drawingContext.DrawTextAtTopCenter($" 坐标：{(int)this.Center.X},{(int)this.Center.Y} ", topp, this.TitleForeground, 12 / view.Scale, this.TitleBackground);

        var bottom = new Point(this.Center.X, this.Center.Y + this.Radius + 2 / view.Scale);
        drawingContext.DrawTextAtBottomCenter($" 序号：{this.Index.ToString()} ", bottom, this.TitleForeground, 12 / view.Scale, this.TitleBackground);
    }
}