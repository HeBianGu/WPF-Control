// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

public class DiagramTemplate : DisplayBindableBase, IDiagramTemplate
{
    public DiagramTemplate()
    {

    }

    public DiagramTemplate(IDiagramData diagram)
    {
        this.Diagram = diagram;
        this.Name = diagram.Name;
        this.GroupName = diagram.GroupName;
    }
    private IDiagramData _diagram;
    [Browsable(false)]
    public IDiagramData Diagram
    {
        get { return _diagram; }
        set
        {
            _diagram = value;
            RaisePropertyChanged();
        }
    }

    private string _name;
    /// <summary> 说明  </summary>
    [Display(Name = "名称")]
    public new string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }
}
