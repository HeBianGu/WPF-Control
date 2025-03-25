// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
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
