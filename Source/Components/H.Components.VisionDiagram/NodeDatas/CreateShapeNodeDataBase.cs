// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.VisionDiagram.Extensions;
global using H.Components.VisionDiagram.Geometrys;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State.Adds;
using H.Controls.ShapeBox.State.Base;

namespace H.Components.VisionDiagram.NodeDatas;
public class CreateLineShapeState : AddLineShapeState
{
    private readonly ICreateShapeNodeData _createShapeNodeData;
    public CreateLineShapeState(ICreateShapeNodeData createShapeNodeData)
    {
        this._createShapeNodeData = createShapeNodeData;
    }

    protected override void Sumit()
    {
        base.Sumit();
        this._createShapeNodeData.Shape = this.Shape;
        this._createShapeNodeData.ResultLine = this.Shape.ToVisionLine();
        this.DrawStateShape(this.Shape);
    }

    public override void Enter()
    {
        if (this._createShapeNodeData.ResultLine.IsEmpty == false)
            this.Shape = this._createShapeNodeData.ResultLine.ToLineShape();
        else
            this.DrawStateShape();
        base.Enter();
    }
}

public class CreateCircleShapeState : AddCircleShapeState
{
    private readonly ICreateShapeNodeData _createShapeNodeData;
    public CreateCircleShapeState(ICreateShapeNodeData createShapeNodeData)
    {
        this._createShapeNodeData = createShapeNodeData;
    }

    protected override void Sumit()
    {
        base.Sumit();
        this._createShapeNodeData.Shape = this.Shape;
        this._createShapeNodeData.ResultCircle = this.Shape.ToVisionCircle();
        this.DrawStateShape(this.Shape);
    }

    public override void Enter()
    {
        if (this._createShapeNodeData.ResultCircle.IsEmpty == false)
            this.Shape = this._createShapeNodeData.ResultCircle.ToCircleShape();
        else
            this.DrawStateShape();
        base.Enter();
    }
}

public class CreatePointShapeState : AddPointShapeState
{
    private readonly ICreateShapeNodeData _createShapeNodeData;
    public CreatePointShapeState(ICreateShapeNodeData createShapeNodeData)
    {
        this._createShapeNodeData = createShapeNodeData;
    }

    protected override void Sumit()
    {
        base.Sumit();
        this._createShapeNodeData.Shape = this.Shape;
        this._createShapeNodeData.ResultPoint = this.Shape.Point;
        this.DrawStateShape(this.Shape);
    }

    public override void Enter()
    {
        if (this._createShapeNodeData.ResultPoint != default)
            this.Shape = new PointShape(this._createShapeNodeData.ResultPoint);
        else
            this.DrawStateShape();
        base.Enter();
    }
}

public interface ICreateShapeNodeData
{
    IShape Shape { get; set; }

    VisionCircle ResultCircle { get; set; }
    VisionLine ResultLine { get; set; }
    Point ResultPoint { get; set; }
}

public abstract class CreateShapeNodeDataBase<T> : ROINodeData<T>, ICreateShapeNodeData where T : class, IVisionImage
{
    private IShape _Shape;
    public IShape Shape
    {
        get { return _Shape; }
        set
        {
            _Shape = value;
            RaisePropertyChanged();
        }
    }

    private Point _ResultPoint;
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "点结果", GroupName = VisionTabNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public Point ResultPoint
    {
        get
        {
            return _ResultPoint;
        }
        set
        {
            _ResultPoint = value;
        }
    }

    private VisionCircle _ResultCircle;
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "圆结果", GroupName = VisionTabNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public VisionCircle ResultCircle
    {
        get
        {
            return _ResultCircle;
        }
        set
        {
            _ResultCircle = value;
        }
    }

    private VisionLine _ResultLine;
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "线结果", GroupName = VisionTabNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public VisionLine ResultLine
    {
        get
        {
            return _ResultLine;
        }
        set
        {
            _ResultLine = value;
        }
    }

    public override IEnumerable<IExpression> GetExpressions(Predicate<object> predicate = null)
    {
        Predicate<object> p = x =>
        {
            if (x is VisionLine visionLine && visionLine.IsEmpty)
                return false;
            if (x is VisionCircle visionCircle && visionCircle.IsEmpty)
                return false;
            if (x is Point point && point == default)
                return false;
            return predicate?.Invoke(x) != false;
        };
        return base.GetExpressions(p);
    }

    protected override IEnumerable<IViewState> CreateViewStates()
    {
        return base.CreateViewStates().Concat(CreateCreateShapeViewStates());
    }

    protected virtual IEnumerable<IViewState> CreateCreateShapeViewStates()
    {
        yield return new CreatePointShapeState(this);
        yield return new CreateLineShapeState(this);
        yield return new CreateCircleShapeState(this);
    }

    public IResultPresenter GetCreateShapeResultPresenter()
    {
        return this.Shape.ToAutoResultPresenter(this.Name);
    }

}

