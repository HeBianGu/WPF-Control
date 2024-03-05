// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;

namespace H.Controls.PropertyGrid
{
    public abstract class DefinitionBase : DependencyObject
    {
        private bool _isLocked;

        internal bool IsLocked
        {
            get { return _isLocked; }
        }

        internal void ThrowIfLocked<TMember>(Expression<Func<TMember>> propertyExpression)
        {
            //In XAML, when using any properties of PropertyDefinition, the error of ThrowIfLocked is always thrown => prevent it !
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (this.IsLocked)
            {
                string propertyName = ReflectionHelper.GetPropertyOrFieldName(propertyExpression);
                string message = string.Format(
                    @"Cannot modify {0} once the definition has beed added to a collection.",
                    propertyName);
                throw new InvalidOperationException(message);
            }
        }

        internal virtual void Lock()
        {
            if (!_isLocked)
            {
                _isLocked = true;
            }
        }
    }
}
