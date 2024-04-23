// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;

namespace H.Controls.PropertyGrid.Media.Animation
{
    [TypeConverter(typeof(IterativeEquationConverter))]
    public class IterativeEquation<T>
    {
        #region Constructors

        public IterativeEquation(IterativeAnimationEquationDelegate<T> equation)
        {
            _equation = equation;
        }

        internal IterativeEquation()
        {
        }

        #endregion

        public virtual T Evaluate(TimeSpan currentTime, T from, T to, TimeSpan duration)
        {
            return _equation(currentTime, from, to, duration);
        }

        #region Private Fields

        private readonly IterativeAnimationEquationDelegate<T> _equation;

        #endregion
    }
}
