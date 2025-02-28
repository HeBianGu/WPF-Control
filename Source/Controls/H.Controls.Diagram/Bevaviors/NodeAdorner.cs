// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Bevaviors;


//public class NodeAdorner : BorderAdorner
//{
//    Port left;
//    Port right;
//    Port top;
//    Port bottom;

//    private VisualCollection visualCollection;

//    public NodeAdorner(Node adornedElement) : base(adornedElement)
//    {
//        visualCollection = new VisualCollection(this);

//        left = new Port() { ParentNode = adornedElement, Dock = Dock.Left };
//        right = new Port() { ParentNode = adornedElement, Dock = Dock.Right };
//        top = new Port() { ParentNode = adornedElement, Dock = Dock.Top };
//        bottom = new Port() { ParentNode = adornedElement, Dock = Dock.Bottom };

//        visualCollection.Add(left);
//        visualCollection.Add(right);
//        visualCollection.Add(top);
//        visualCollection.Add(bottom);


//        left.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
//        right.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
//        top.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
//        bottom.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

//        //this.MouseDown += NodeAdorner_MouseDown;
//        //this.PreviewMouseRightButtonDown += NodeAdorner_MouseRightButtonDown;

//    }

//    //private void NodeAdorner_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//    //{
//    //    var diagram= this.AdornedElement.GetParent<Diagram>();

//    //    if (diagram == null) return;

//    //    diagram.RaiseEvent(e);
//    //}

//    //private void NodeAdorner_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
//    //{
//    //    this.AdornedElement.RaiseEvent(e);
//    //}

//    public Size CornerSize { get; set; } = new Size(5, 5);

//    public Brush CornerFill { get; set; } = Brushes.Red;

//    protected override void OnRender(DrawingContext dc)
//    {
//        Rect rect = new Rect(this.AdornedElement.RenderSize);

//        left.Arrange(new Rect(rect.Left - left.DesiredSize.Width, rect.Height / 2 - left.DesiredSize.Height / 2, left.DesiredSize.Width, left.DesiredSize.Height));
//        right.Arrange(new Rect(rect.Right, rect.Height / 2 - right.DesiredSize.Height / 2, right.DesiredSize.Width, right.DesiredSize.Height));
//        top.Arrange(new Rect(rect.Width / 2 - top.DesiredSize.Height / 2, rect.Top - top.DesiredSize.Height, top.DesiredSize.Width, top.DesiredSize.Height));
//        bottom.Arrange(new Rect(rect.Width / 2 - bottom.DesiredSize.Height / 2, rect.Bottom, bottom.DesiredSize.Width, bottom.DesiredSize.Height));

//        dc.DrawRectangle(this.Fill, new Pen(this.Stroke, this.StrokeThickness), rect);


//        ////  Do ：四个角
//        //{
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.TopLeft - new Vector(CornerSize.Width, CornerSize.Height), CornerSize));
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.TopRight - new Vector(0, CornerSize.Height), CornerSize));
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.BottomLeft - new Vector(CornerSize.Width, 0), CornerSize));
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.BottomRight - new Vector(0, 0), CornerSize));
//        //}

//        ////  Do ：四个边
//        //{
//        //    Point center = rect.GetCenter();

//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(rect.Left - CornerSize.Width, center.Y - CornerSize.Height / 2.0), CornerSize));
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(rect.Right - 0, center.Y - CornerSize.Height / 2.0), CornerSize));
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(center.X - CornerSize.Width / 2.0, rect.Top - CornerSize.Height), CornerSize));
//        //    dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(center.X - CornerSize.Width / 2.0, rect.Bottom - 0), CornerSize));
//        //}
//    }

//    protected override Visual GetVisualChild(int index)
//    {
//        return visualCollection[index];
//    }
//    protected override int VisualChildrenCount
//    {
//        get
//        {
//            return visualCollection.Count;
//        }
//    }
//}
