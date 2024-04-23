// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace H.Controls.PropertyGrid
{
    internal static class PropertyChangedExt
    {
        #region Notify Methods

        public static void Notify<TMember>(
          this INotifyPropertyChanged sender,
          PropertyChangedEventHandler handler,
          Expression<Func<TMember>> expression)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");

            if (expression == null)
                throw new ArgumentNullException("expression");

            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The expression must target a property or field.", "expression");

            string propertyName = PropertyChangedExt.GetPropertyName(body, sender.GetType());

            PropertyChangedExt.NotifyCore(sender, handler, propertyName);
        }

        public static void Notify(this INotifyPropertyChanged sender, PropertyChangedEventHandler handler, string propertyName)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");

            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            ReflectionHelper.ValidatePropertyName(sender, propertyName);

            PropertyChangedExt.NotifyCore(sender, handler, propertyName);
        }

        private static void NotifyCore(INotifyPropertyChanged sender, PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region PropertyChanged Verification Methods

        internal static bool PropertyChanged(string propertyName, PropertyChangedEventArgs e, bool targetPropertyOnly)
        {
            string target = e.PropertyName;
            if (target == propertyName)
                return true;

            return (!targetPropertyOnly)
                && string.IsNullOrEmpty(target);
        }

        internal static bool PropertyChanged<TOwner, TMember>(
          Expression<Func<TMember>> expression,
          PropertyChangedEventArgs e,
          bool targetPropertyOnly)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The expression must target a property or field.", "expression");

            return PropertyChangedExt.PropertyChanged(body, typeof(TOwner), e, targetPropertyOnly);
        }

        internal static bool PropertyChanged<TOwner, TMember>(
          Expression<Func<TOwner, TMember>> expression,
          PropertyChangedEventArgs e,
          bool targetPropertyOnly)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The expression must target a property or field.", "expression");

            return PropertyChangedExt.PropertyChanged(body, typeof(TOwner), e, targetPropertyOnly);
        }

        private static bool PropertyChanged(MemberExpression expression, Type ownerType, PropertyChangedEventArgs e, bool targetPropertyOnly)
        {
            string propertyName = PropertyChangedExt.GetPropertyName(expression, ownerType);

            return PropertyChangedExt.PropertyChanged(propertyName, e, targetPropertyOnly);
        }

        #endregion

        private static string GetPropertyName(MemberExpression expression, Type ownerType)
        {
            Type targetType = expression.Expression.Type;
            if (!targetType.IsAssignableFrom(ownerType))
                throw new ArgumentException("The expression must target a property or field on the appropriate owner.", "expression");

            return ReflectionHelper.GetPropertyOrFieldName(expression);
        }
    }
}
