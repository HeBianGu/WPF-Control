// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Runtime.Serialization;
namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public abstract class PortableNodeData : NodeData, IPortableNodeData
{
    public List<IPortData> PortDatas { get; set; } = new List<IPortData>();
    protected List<IPortData> _defaultPortDatas;
    protected PortableNodeData()
    {
        this.InitPortDatas();
    }

    protected virtual void InitPortDatas()
    {
        List<IPortData> ds = this.CreatePortDatas().ToList();
        foreach (IPortData item in ds)
        {
            item.BuildTextData();
        }
        this._defaultPortDatas = ds.ToList();
        this.PortDatas = ds.ToList();
    }

    protected virtual IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Left;
            port.PortType = PortType.Both;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Right;
            port.PortType = PortType.Both;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Both;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.Both;
            yield return port;
        }
    }

    public override object Clone()
    {
        PortableNodeData data = base.Clone() as PortableNodeData;
        data.PortDatas.Clear();
        data.InitPortDatas();
        return data;
    }

    #region - Serializing -
    [OnSerializing]
    internal void OnSerializingMethod(StreamingContext context)
    {
        this.OnSerializing(context);
    }

    protected virtual void OnSerializing(StreamingContext context)
    {

    }
    [OnSerialized]
    internal void OnSerializedMethod(StreamingContext context)
    {
        this.OnSerialized(context);
    }
    protected virtual void OnSerialized(StreamingContext context)
    {

    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        this.PortDatas = this.PortDatas.Except(this._defaultPortDatas).ToList();
        this.OnDeserialized(context);
    }
    protected virtual void OnDeserialized(StreamingContext context)
    {

    }
    [OnDeserializing]
    internal void OnDeserializingMethod(StreamingContext context)
    {
        this.OnDeserializing(context);
    }
    protected virtual void OnDeserializing(StreamingContext context)
    {

    }
    #endregion

}
