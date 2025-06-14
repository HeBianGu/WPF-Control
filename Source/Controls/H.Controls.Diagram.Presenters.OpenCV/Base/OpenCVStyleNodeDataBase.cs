// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenter.LinkDatas;
using H.Controls.Diagram.Presenter.NodeDatas.Base;
using H.Controls.Diagram.Presenter.PortDatas;
using System.Windows.Controls;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public abstract class OpenCVStyleNodeDataBase : SelectableFromNodeDataBase
{
    private double _flagLength;
    [DefaultValue(10)]
    [Display(Name = "标签宽度", GroupName = "样式")]
    public double FlagLength
    {
        get { return _flagLength; }
        set
        {
            _flagLength = value;
            RaisePropertyChanged();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Width = 120;
        this.Height = 35;
        this.CornerRadius = 2.0;
        this.Fill = Brushes.White;
    }

    protected override void InitPortDatas()
    {
        List<IPortData> ds = this.CreatePortDatas().ToList();
        //foreach (IPortData item in ds)
        //{
        //    item.BuildTextData();
        //}
        this._defaultPortDatas = ds.ToList();
        this.PortDatas = ds.ToList();
    }

    //protected override IEnumerable<IPortData> CreatePortDatas()
    //{
    //    {
    //        IPortData port = CreatePortData();
    //        port.Dock = Dock.Top;
    //        port.PortType = PortType.Input;
    //        yield return port;
    //    }
    //    {
    //        IPortData port = CreatePortData();
    //        port.Dock = Dock.Bottom;
    //        port.PortType = PortType.OutPut;
    //        yield return port;
    //    }
    //}

    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Input;
            yield return port;
        }
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Left;
            port.PortType = PortType.Input;
            yield return port;
        }
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Right;
            port.PortType = PortType.OutPut;
            yield return port;
        }
    }

    public override IFlowablePortData CreatePortData()
    {
        return new OpenCVFlowablePortData(this.ID, PortType.Both) { Width = 6, Height = 6 };
    }

    public class OpenCVFlowablePortData : FlowablePortData
    {
        public OpenCVFlowablePortData(string nodeID, PortType portType) : base(nodeID, portType)
        {
            this.Fill = Brushes.White;
        }
        public override ILinkData CreateLinkData()
        {
            return new FlowableLinkData()
            {
                FromNodeID = this.NodeID,
                FromPortID = this.ID,
                Text = this.Text ?? this.Name ?? this.Description,
                Stroke = Brushes.Orange,

            };
        }
    }
}