// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.Triggers;

/// <summary>
/// 按住一定时间间隔才会执行
/// </summary>
public class MousePressClickTrigger : MousePressTrigger
{
    public MousePressClickTrigger()
    {
        this.UseInvokeOnDown = false;
    }
    protected override void ElapsedInvokeActions()
    {
        this.InvokeActions(null);
        Stop();
    }
}
