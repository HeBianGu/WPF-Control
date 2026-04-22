// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;

namespace H.App.AIDI.Presenters;
[Display(Name = "③评估管理", Description = "管理模型的评估和结果分析", GroupName = "流程")]
public class EvaluationTabPresenter : TabPresenterBase
{
    public EvaluationTabPresenter(IModulePresenter modulePresenter) : base(modulePresenter)
    {

    }
}