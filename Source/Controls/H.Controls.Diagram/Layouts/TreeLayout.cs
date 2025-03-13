// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Extensions.TypeConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Diagram.Layouts;

[DisplayName("TreeLayout")]
[TypeConverter(typeof(DisplayNameConverter))]
public class TreeLayout : Layout
{
    private double _span = 200.0;
    public double Span
    {
        get { return _span; }
        set
        {
            _span = value;
            RaisePropertyChanged();
            this.Diagram?.RefreshLayout();
        }
    }


    private HorizontalAlignment _alignment = HorizontalAlignment.Center;
    public HorizontalAlignment Alignment
    {
        get { return _alignment; }
        set
        {
            _alignment = value;
            RaisePropertyChanged();
            this.Diagram?.RefreshLayout();
        }
    }

    private Orientation _orientation;
    /// <summary> 树型节点的排列方式 水平和竖直 </summary>
    public Orientation Orientation
    {
        get { return _orientation; }
        set
        {
            _orientation = value;
            RaisePropertyChanged();
            this.Diagram?.RefreshLayout();
        }
    }

    public override void DoLayout(params Node[] nodes)
    {
        IEnumerable<TreeNode> roots = this.GetRoots(nodes);
        this.LayoutRoots(roots);

        ////  Do ：设置没有连线的Port不显示
        //foreach (var node in nodes)
        //{
        //    node.RefreshPortVisiblity();
        //}

        this.TranformAll(roots);

        foreach (Node node in nodes)
        {
            this.DoLayoutNode(node);

            this.DoLayoutLink(node);
        }
    }

    protected virtual void LayoutRoots(IEnumerable<TreeNode> roots)
    {
        Func<double, double> getDeep = l => l * this.Span * 1;

        foreach (TreeNode node in roots)
        {
            node.MeasureNode();
            Point center = new Point(this.Diagram.Width / 2.0 - node.DesiredSize.Width / 2, this.Diagram.Height / 2.0 - node.DesiredSize.Height / 2);

            node.ArrangeNode(center, l => l);
        }
    }

    protected virtual void ArrangeNode(TreeNode node, Point point, Func<Point, Point> transfor)
    {
        NodeLayer.SetPosition(node, transfor.Invoke(point));
        node.Location = point;
        double y = point.Y - node.NodeDesiredSize.Height / 2;
        foreach (TreeNode item in node.GetChildren())
        {
            item.MeasureNode();
            y += item.NodeDesiredSize.Height;
            int level = item.GetLevel();
            double x = level * this.Span + point.X;
            Point center = new Point(x, y - item.NodeDesiredSize.Height / 2);
            this.ArrangeNode(item, center, transfor);
        }
    }

    public override void DoLayoutLink(Link link)
    {
        if (link.ToNode == null || link.FromNode == null) return;

        Point from = NodeLayer.GetPosition(link.FromNode);

        Point to = NodeLayer.GetPosition(link.ToNode);

        //  Do ：设置连线连接到节点上
        if (this.Orientation == Orientation.Vertical)
        {
            from = new Point(from.X + link.FromNode.DesiredSize.Width / 2, from.Y);
            to = new Point(to.X - link.ToNode.DesiredSize.Width / 2, to.Y);
        }
        else
        {
            from = new Point(from.X, from.Y + link.FromNode.DesiredSize.Height / 2);
            to = new Point(to.X, to.Y - link.ToNode.DesiredSize.Height / 2);
        }

        LinkLayer.SetEnd(link, to);
        LinkLayer.SetStart(link, from);
        link.Update();
    }

    /// <summary> 获取子项树需要的宽度和高度 </summary>
    public Size MeasureNode(Node node)
    {
        Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);
        double height = 0;
        double weight = 0;
        foreach (Link link in node.LinksOutOf)
        {
            Size size = this.MeasureNode(link.ToNode);
            height += size.Height;
            weight += size.Width;
        }
        this.Measure(childConstraint);
        //  Do ：获取子项和当前项的最大范围
        weight = Math.Max(weight, this.DesiredSize.Width);
        height = Math.Max(height, this.DesiredSize.Height);
        return new Size(weight, height);
    }

    //  Do ：待实现
    public Size DesiredSize { get; set; }
    protected void Measure(Size size)
    {
        //this.DesiredSize
    }


    protected virtual void TranformAll(IEnumerable<TreeNode> roots)
    {
        foreach (TreeNode item in roots)
        {
            if (this.Orientation == Orientation.Horizontal)
                item.TranformAll(new Vector(0, 0));
            if (this.Orientation == Orientation.Vertical)
                item.TranformAll(new Vector(item.DesiredSize.Width / 2.0, 0));
        }
    }

    protected virtual IEnumerable<TreeNode> GetRoots(IEnumerable<Node> nodes)
    {
        return nodes.Where(l => l.LinksInto == null || l.LinksInto.Count == 0).OfType<TreeNode>()?.ToList();
    }
}
