// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

[Icon("\xF0E3")]
public abstract class NodeDataBase : DisplayBindableBase, ICloneable
{
    public NodeDataBase()
    {
        this.ID = Guid.NewGuid().ToString();
    }

    public virtual object Clone()
    {
        object result = Activator.CreateInstance(GetType());
        IEnumerable<PropertyInfo> ps = GetType().GetProperties().Where(x => x.CanRead && x.CanWrite);
        foreach (PropertyInfo p in ps)
        {
            p.SetValue(result, p.GetValue(this));
        }
        return result;
    }
}
