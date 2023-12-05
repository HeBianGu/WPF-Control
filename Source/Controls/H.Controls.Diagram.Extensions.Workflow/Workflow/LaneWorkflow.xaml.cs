// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Diagram.Extension;
using H.Themes.Default;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;


namespace H.Controls.Diagram.Extensions.Workflow
{
    //[Display()]
    [Display(Name = "泳道图", GroupName = "流程图", Order = 1)]
    public class LaneWorkflow : WorkflowBase
    {
        public LaneWorkflow()
        {
            this.Layout = new LaneLayout();
        }
    }

    public class LaneLayout : LocationLayout
    {

    }

    public abstract class LaneNodeDataBase : WorkflowNodeBase
    {
        public LaneNodeDataBase()
        {
            this.Text = "标题";
            this.Fill = Brushes.Transparent;
            this.HeaderBrush = Brushes.Transparent;
        }

        private double _headerHeight = (double)Application.Current.FindResource(LayoutKeys.RowHeight);
        /// <summary> 说明  </summary>
        public double HeaderHeight
        {
            get { return _headerHeight; }
            set
            {
                _headerHeight = value;
                RaisePropertyChanged();
            }
        }

        private Brush _headerBrush;
        /// <summary> 说明  </summary>
        public Brush HeaderBrush
        {
            get { return _headerBrush; }
            set
            {
                _headerBrush = value;
                RaisePropertyChanged();
            }
        }

        private CornerRadius _cornerRadius = new CornerRadius(2);
        /// <summary> 说明  </summary>
        public CornerRadius CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                RaisePropertyChanged();
            }
        }

    }
    [Display(Name = "垂直泳道", GroupName = "泳道图形状", Order = 0, Description = "垂直泳道")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    public class VerticalLaneNodeData : LaneNodeDataBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Width = 200;
            this.Height = 800;
        }

        protected override Geometry GetGeometry()
        {
            return Geometry.Empty;
        }
    }
    [Display(Name = "水平泳道", GroupName = "泳道图形状", Order = 0, Description = "水平泳道")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    public class HorizontalLaneNodeData : LaneNodeDataBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Width = 800;
            this.Height = 200;
        }

        protected override Geometry GetGeometry()
        {
            return Geometry.Empty;
        }
    }


}
