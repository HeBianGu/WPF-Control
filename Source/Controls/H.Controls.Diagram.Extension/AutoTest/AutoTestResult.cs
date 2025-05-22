// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Extensions.Mvvm.ViewModels;

namespace H.Controls.Diagram.Extension.AutoTest;

public class AutoTestResult : SelectBindable<ats_dd_result>
{
    public AutoTestResult() : base(new ats_dd_result())
    {

    }
    public AutoTestResult(ats_dd_result model) : base(model)
    {

    }

    private IDiagramData _diagram;
    public IDiagramData Diagram
    {
        get { return _diagram; }
        set
        {
            _diagram = value;
            RaisePropertyChanged();
        }
    }

}
