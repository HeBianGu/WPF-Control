// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard.EasingFunction;

/// <summary> 指定次幂 </summary>
public class PowerEasingFunction : EasingFunctionBase
{
    public PowerEasingFunction()
        : base()
    {

    }

    public int Pow { get; set; } = 7;

    protected override double EaseInCore(double normalizedTime)
    {
        return Math.Pow(normalizedTime, this.Pow);
    }

    protected override Freezable CreateInstanceCore()
    {
        return new PowerEasingFunction();
    }
}
