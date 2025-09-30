// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Provider.Tree;

public abstract class PartTreeNodeBase : TreeNodeBase<Part>
{
    public PartTreeNodeBase(Part model) : base(model)

    {

    }
    public void RefreshSelected()
    {
        this.IsSelected = this.Model.IsSelected;
        if (this.IsSelected == true && this.Parent != null)
            this.Parent.IsExpanded = true;
    }
}
