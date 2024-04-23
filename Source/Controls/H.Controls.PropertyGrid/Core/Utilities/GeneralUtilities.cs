// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    internal sealed class GeneralUtilities : DependencyObject
    {
        private GeneralUtilities() { }

        #region StubValue attached property

        internal static readonly DependencyProperty StubValueProperty = DependencyProperty.RegisterAttached(
          "StubValue",
          typeof(object),
          typeof(GeneralUtilities),
          new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        internal static object GetStubValue(DependencyObject obj)
        {
            return obj.GetValue(GeneralUtilities.StubValueProperty);
        }

        internal static void SetStubValue(DependencyObject obj, object value)
        {
            obj.SetValue(GeneralUtilities.StubValueProperty, value);
        }

        #endregion StubValue attached property

        public static object GetPathValue(object sourceObject, string path)
        {
            GeneralUtilities targetObj = new GeneralUtilities();
            BindingOperations.SetBinding(targetObj, GeneralUtilities.StubValueProperty, new Binding(path) { Source = sourceObject });
            object value = GeneralUtilities.GetStubValue(targetObj);
            BindingOperations.ClearBinding(targetObj, GeneralUtilities.StubValueProperty);
            return value;
        }

        public static object GetBindingValue(object sourceObject, Binding binding)
        {
            Binding bindingClone = new Binding()
            {
                BindsDirectlyToSource = binding.BindsDirectlyToSource,
                Converter = binding.Converter,
                ConverterCulture = binding.ConverterCulture,
                ConverterParameter = binding.ConverterParameter,
                FallbackValue = binding.FallbackValue,
                Mode = BindingMode.OneTime,
                Path = binding.Path,
                StringFormat = binding.StringFormat,
                TargetNullValue = binding.TargetNullValue,
                XPath = binding.XPath
            };

            bindingClone.Source = sourceObject;

            GeneralUtilities targetObj = new GeneralUtilities();
            BindingOperations.SetBinding(targetObj, GeneralUtilities.StubValueProperty, bindingClone);
            object value = GeneralUtilities.GetStubValue(targetObj);
            BindingOperations.ClearBinding(targetObj, GeneralUtilities.StubValueProperty);
            return value;
        }

        internal static bool CanConvertValue(object value, object targetType)
        {
            return (value != null)
                    && (!object.Equals(value.GetType(), targetType))
                    && (!object.Equals(targetType, typeof(object)));
        }
    }
}
