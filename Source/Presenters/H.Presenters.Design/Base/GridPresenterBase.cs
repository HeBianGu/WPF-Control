global using H.Controls.Adorner.Draggable;
global using H.Themes.Layouts;
global using System.Windows.Controls;
using H.Themes.Layouts;

namespace H.Presenters.Design.Base;

public abstract class GridPresenterBase : PanelPresenterBase
{
    public GridPresenterBase()
    {
        this.Background = Brushes.Transparent;
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.MinRowHeight = (double)Application.Current.FindResource(LayoutKeys.RowHeight);
    }
    private Brush _lineBrush = Brushes.LightGray;
    [Display(Name = "网线颜色", GroupName = "常用,样式")]
    public Brush LineBrush
    {
        get { return _lineBrush; }
        set
        {
            _lineBrush = value;
            RaisePropertyChanged();
        }
    }


    private double _lineThickness = 1;
    [Display(Name = "网线粗细", GroupName = "常用,样式")]
    public double LineThickness
    {
        get { return _lineThickness; }
        set
        {
            _lineThickness = value;
            RaisePropertyChanged();
        }
    }

    private double _minRowHeight;
    [Display(Name = "最小行高", GroupName = "常用,样式")]
    public double MinRowHeight
    {
        get { return _minRowHeight; }
        set
        {
            _minRowHeight = value;
            RaisePropertyChanged();
        }
    }


    public override void DragEnter(UIElement element, DragEventArgs e)
    {
        //IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
        //if (adorner.GetData() is ICloneable cloneable)
        //{
        //    object value = cloneable.Clone();
        //    if (value is DesignPresenterBase design)
        //    {
        //        System.Diagnostics.Debug.WriteLine("DragEnter");
        //        var grid = element.GetChild<Grid>();
        //        if (grid.HitTestRow(out int r) && grid.HitTestColumn(out int c))
        //        {
        //            design.Row = r;
        //            design.Column = c;
        //            System.Diagnostics.Debug.WriteLine($"{r}_{c}");
        //        }
        //    }
        //    this.Presenters.Add(value);
        //    _dropBackup = value;
        //}
    }

    public override bool IsHitTest(UIElement element, DragEventArgs e)
    {
        return element.GetContent() != _dropBackup;
    }

    public override void DragOver(UIElement element, DragEventArgs e)
    {
        var p = e.GetPosition(element);
        var grid = element.GetChild<Grid>();
        if (_dropBackup == null)
        {
            IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
            if (adorner.GetData() is IDesignPresenter value)
            {
                if (grid.HitTestRow(p, out int r) && grid.HitTestColumn(p, out int c))
                {
                    value.Row = r;
                    value.Column = c;
                    value.Opacity = 0.5;
                }
                this.Presenters.Add(value);
                _dropBackup = value;
            }
        }
        else
        {
            if (grid.HitTestRow(p, out int r) && grid.HitTestColumn(p, out int c))
            {
                if (_dropBackup.Row == r && _dropBackup.Column == c)
                    return;
                _dropBackup.Row = r;
                _dropBackup.Column = c;
                //this.Presenters.Remove(_dropBackup);
                //this.Presenters.Add(_dropBackup);
                element.InvalidateVisual();
            }
        }
    }
}
