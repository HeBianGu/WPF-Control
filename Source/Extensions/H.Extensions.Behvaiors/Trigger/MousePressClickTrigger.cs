﻿// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
namespace H.Extensions.Behvaiors
{
    /// <summary>
    /// 按住一定时间间隔才会执行
    /// </summary>
    public class MousePressClickTrigger : MousePressTrigger
    {
        public MousePressClickTrigger()
        {
            UseInvokeOnDown = false;
        }
        protected override void ElapsedInvokeActions()
        {
            this.InvokeActions(null);
            Stop();
        }
    }
}
