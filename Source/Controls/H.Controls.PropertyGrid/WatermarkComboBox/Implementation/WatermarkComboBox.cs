// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.PropertyGrid
{
    public class WatermarkComboBox : ComboBox
    {
        #region Properties

        #region Watermark

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(object), typeof(WatermarkComboBox), new UIPropertyMetadata(null));
        public object Watermark
        {
            get
            {
                return GetValue(WatermarkProperty);
            }
            set
            {
                SetValue(WatermarkProperty, value);
            }
        }

        #endregion //Watermark

        #region WatermarkTemplate

        public static readonly DependencyProperty WatermarkTemplateProperty = DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(WatermarkComboBox), new UIPropertyMetadata(null));
        public DataTemplate WatermarkTemplate
        {
            get
            {
                return (DataTemplate)GetValue(WatermarkTemplateProperty);
            }
            set
            {
                SetValue(WatermarkTemplateProperty, value);
            }
        }

        #endregion //WatermarkTemplate

        #endregion //Properties

        #region Constructors

        static WatermarkComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkComboBox), new FrameworkPropertyMetadata(typeof(WatermarkComboBox)));
        }

        #endregion //Constructors

        #region Base Class Overrides




        #endregion
    }
}
