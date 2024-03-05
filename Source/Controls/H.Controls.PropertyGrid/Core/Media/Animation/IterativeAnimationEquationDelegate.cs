// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid.Media.Animation
{
    public delegate T IterativeAnimationEquationDelegate<T>(TimeSpan currentTime, T from, T to, TimeSpan duration);
}
