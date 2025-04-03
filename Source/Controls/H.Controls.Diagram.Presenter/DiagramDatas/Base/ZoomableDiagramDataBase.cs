global using H.Controls.Diagram.Layers;
global using H.Controls.Diagram.Parts.Base;
using H.Extensions.FontIcon;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class ZoomableDiagramDataBase : DiagramDataBase, IZoomableDiagramData
{
    [Icon(FontIcons.Zoom)]
    [Display(Name = "缩放定位", GroupName = "操作", Order = 5)]
    public virtual DisplayCommand ZoomAllCommand => new DisplayCommand(e =>
    {
        this.GetTargetElement<Diagram>().ZoomToFit();
    }, x => this.GetTargetElement<Diagram>() != null);
    protected void ZoomToFit()
    {
        this.GetTargetElement<Diagram>()?.ZoomToFit();
    }

    //[Icon(FontIcons.SIPMove)]
    //[Display(Name = "平移定位", GroupName = "操作", Order = 5)]
    //public virtual DisplayCommand PanToCenterCommand => new DisplayCommand(e =>
    //{
    //    if (e is Diagram diagram)
    //        diagram.PanToCenter();
    //}, x => this.Nodes.Count > 0);

    //public void ZoomToFit()
    //{
    //    if (this.Nodes.Count == 0)
    //        return;
    //    double xmax = this.Nodes.Max(x => x.Location.X + x.ActualWidth / 2);
    //    double xmin = this.Nodes.Min(x => x.Location.X - x.ActualWidth / 2);
    //    double ymax = this.Nodes.Max(x => x.Location.Y + x.ActualHeight / 2);
    //    double ymin = this.Nodes.Min(x => x.Location.Y - x.ActualHeight / 2);
    //    Rect rect = new Rect(new Point(xmin, ymin), new Point(xmax, ymax));
    //    this.ZoomTo(rect);
    //}

    //public void PanToCenter()
    //{
    //    double xmax = this.Nodes.Max(x => x.Location.X + x.ActualWidth / 2);
    //    double xmin = this.Nodes.Min(x => x.Location.X - x.ActualWidth / 2);
    //    double ymax = this.Nodes.Max(x => x.Location.Y + x.ActualHeight / 2);
    //    double ymin = this.Nodes.Min(x => x.Location.Y - x.ActualHeight / 2);
    //    Point center = new Point((xmin + xmax) / 2, (ymin + ymax) / 2);
    //    //this.LocateCenterCallBack.Invoke(center);
    //    this.ZoomTo(center);
    //}

    //protected override void OnPreivewPart()
    //{
    //    base.OnPreivewPart();
    //    this.PanTo(this.SelectedPart);

    //}
    //protected override void OnNextPart()
    //{
    //    base.OnNextPart();
    //    this.PanTo(this.SelectedPart);
    //}

    //public void PanTo(Part part)
    //{
    //    if (part is Link link)
    //    {
    //        Point point1 = LinkLayer.GetStart(link);
    //        Point point2 = LinkLayer.GetEnd(link);
    //        Point center = new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
    //        //this.LocateCenterCallBack?.Invoke(center);
    //        this.ZoomTo(center);
    //    }
    //    else
    //    {
    //        Rect rect = part.Bound;
    //        Point center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
    //        //this.LocateCenterCallBack?.Invoke(center);
    //        this.ZoomTo(center);
    //    }
    //}

    //public void ZoomTo(Part part)
    //{
    //    if (part is Link link)
    //    {
    //        Point point1 = LinkLayer.GetStart(link);
    //        Point point2 = LinkLayer.GetEnd(link);
    //        Rect rect = new Rect(point1, point2);
    //        this.ZoomTo(rect);
    //        //this.LocateRectCallBack.Invoke(rect);
    //    }
    //    else
    //    {
    //        Rect rect = part.Bound;
    //        this.ZoomTo(rect);
    //        //this.LocateRectCallBack.Invoke(rect);
    //    }
    //}

    //protected void ZoomTo(Rect rect)
    //{
    //    this.GetDiagram().ZoomTo(rect);
    //}

    //public void ZoomToFit()
    //{
    //    this.GetDiagram().ZoomToFit();
    //}

    //public void ZoomTo(Point point)
    //{
    //    ////  ToDo：目前显示不正确
    //    //return;
    //    this.GetDiagram().ZoomToFit();
    //    //this.GetDiagram().ZoomTo(point);
    //}

    //private Diagram GetDiagram()
    //{
    //    var node = this.Nodes.FirstOrDefault();
    //    if (node == null)
    //        return null;
    //    return node.GetDiagram();
    //}
}
