// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace H.Controls.Diagram.Parts;

public interface ITreeNode
{
    Size MeasureNode();
}

public class TreeNode : Node, ITreeNode
{
    public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(TreeNode), "S.TreeNode.Default");

    static TreeNode()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeNode), new FrameworkPropertyMetadata(typeof(TreeNode)));
    }

    public bool IsExpanded
    {
        get { return (bool)GetValue(IsExpandedProperty); }
        set { SetValue(IsExpandedProperty, value); }
    }


    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool), typeof(TreeNode), new PropertyMetadata(true, (d, e) =>
        {
            TreeNode control = d as TreeNode;

            if (control == null) return;

            //bool config = e.NewValue as bool; 

            //  Do ：更新显示隐藏
            //control.Foreach(l => l.Visibility = l.GetVisiblity() ? Visibility.Visible : Visibility.Collapsed); 

            //control.GetParent<Diagram>().RefreshData();

            control.GetParent<Diagram>().RefreshLayout();

        }));

    public Size NodeDesiredSize { get; protected set; }

    /// <summary> 后续遍历 先执行子节点 </summary>
    public void Foreach(Action<TreeNode> action)
    {
        action?.Invoke(this);
        List<TreeNode> children = this.GetChildren();
        foreach (TreeNode child in children)
        {
            child.Foreach(action);
        }
    }

    public void TranformAll(Vector vector)
    {
        this.Foreach(x =>
        {
            Point p = NodeLayer.GetPosition(x);
            Point point = p + vector;
            NodeLayer.SetPosition(x, point);
            x.Location = point;
        });
    }

    /// <summary> 获取父节点 </summary>
    public TreeNode GetParent()
    {
        return this.LinksInto.Select(l => l.FromNode as TreeNode)?.FirstOrDefault();
    }

    /// <summary> 获取所有子节点 </summary>
    public List<TreeNode> GetChildren()
    {
        return this.LinksOutOf.Select(l => l.ToNode as TreeNode)?.ToList();
    }

    /// <summary> 获取当前节点是否应该显示 </summary>
    protected override bool GetVisiblity()
    {
        Predicate<TreeNode> predicate = null;

        predicate = p =>
        {
            if (p == null) return true;

            return !p.IsExpanded ? false : predicate(p.GetParent());
        };

        return predicate.Invoke(this.GetParent());
    }

    //public override void RefreshVisiblity()
    //{
    //    //  Do ：根据父节点IsExpanded属性设置当前节点是否可见
    //    this.Visibility = this.GetVisiblity() ? Visibility.Visible : Visibility.Collapsed;

    //    var parts = this.GetParts();

    //    ////  Do ：全部隐藏
    //    //parts.ForEach(l=>l.Visibility=Visibility.Hidden);

    //    //  Do ：根据当前节点展开和可见状态 设置Part哪些可见哪些不可见
    //    if (this.Visibility != Visibility.Visible)
    //    {
    //        parts.ForEach(l => l.Visibility = Visibility.Collapsed);
    //    }
    //    else
    //    {
    //        if (this.IsExpanded)
    //        {
    //            parts.ForEach(l => l.Visibility = Visibility.Visible);
    //        }
    //        else
    //        {
    //            this.LinksOutOf.ForEach(l => l.Visibility = Visibility.Collapsed);

    //            this.LinksOutOf.Select(l => l.ToPort)?.ToList()?.ForEach(l => l.Visibility = Visibility.Collapsed);
    //        }
    //    }
    //} 


    public override void Delete()
    {
        //  Do ：删除所有子节点
        List<TreeNode> children = this.GetChildren();

        foreach (TreeNode child in children)
        {
            child.Delete();
        }

        base.Delete();
    }

    /// <summary> 获取子项树需要的宽度和高度 </summary>
    public virtual Size MeasureNode()
    {

        //System.Diagnostics.Debug.WriteLine("MeasureNode");

        Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);
        double height = 0;
        double weight = 0;

        foreach (Link link in this.LinksOutOf)
        {
            if (link.ToNode is TreeNode node)
            {
                Size size = node.MeasureNode();
                height += size.Height;
                weight += size.Width;
            }
        }

        this.Measure(childConstraint);

        //  Do ：获取子项和当前项的最大范围
        weight = Math.Max(weight, this.DesiredSize.Width);
        height = Math.Max(height, this.DesiredSize.Height);

        return this.NodeDesiredSize = new Size(weight, height);
    }

    public int GetLevel()
    {
        int level = 0;
        Action<TreeNode> action = x =>
          {
              if (x.GetParent() == null)
                  return;
              level++;
          };
        action(this);
        return level;
    }



    public double Span
    {
        get { return (double)GetValue(SpanProperty); }
        set { SetValue(SpanProperty, value); }
    }


    public static readonly DependencyProperty SpanProperty =
        DependencyProperty.Register("Span", typeof(double), typeof(TreeNode), new FrameworkPropertyMetadata(110.0, (d, e) =>
         {
             TreeNode control = d as TreeNode;

             if (control == null) return;

             if (e.OldValue is double o)
             {

             }

             if (e.NewValue is double n)
             {

             }

         }));



    public virtual void ArrangeNode(Point point, Func<Point, Point> transfor)
    {
        //System.Diagnostics.Debug.WriteLine("ArrangeNode");
        NodeLayer.SetPosition(this, transfor.Invoke(point));
        this.Location = point;
        double y = point.Y - this.NodeDesiredSize.Height / 2;
        foreach (TreeNode item in this.GetChildren())
        {
            item.MeasureNode();
            y += item.NodeDesiredSize.Height;
            int level = item.GetLevel();
            double x = level * this.Span + point.X;
            Point center = new Point(x, y - item.NodeDesiredSize.Height / 2);
            item.ArrangeNode(center, transfor);
        }
    }
}

public class SpaceTreeNode : TreeNode
{
    public TreeNode Node { get; set; }
}
