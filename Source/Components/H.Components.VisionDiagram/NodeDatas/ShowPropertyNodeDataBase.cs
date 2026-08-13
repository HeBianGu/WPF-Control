// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;
public abstract class ShowPropertyNodeDataBase : StateNodeDataBase
{
    public override object GetPropertyPresenter()
    {
        return new InvokeCommandsPropertyPresenter(this, this.GetUseTabNames());
    }

    public virtual IEnumerable<string> GetUseTabNames()
    {
        return typeof(VisionTabNames).GetFields(BindingFlags.Public | BindingFlags.Static)
               .Where(x => x.FieldType == typeof(string))
               .Select(x => x.GetValue(null)?.ToString())
               .Where(x => !string.IsNullOrEmpty(x));
    }

}

