﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
#if !VS2008
using System.ComponentModel.DataAnnotations;
#endif
using System.ComponentModel;

namespace H.Controls.PropertyGrid
{
    public class UpDownEditor<TEditor, TType> : TypeEditor<TEditor> where TEditor : UpDownBase<TType>, new()
    {
        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            this.Editor.TextAlignment = System.Windows.TextAlignment.Left;
        }
        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = UpDownBase<TType>.ValueProperty;
        }

#if !VS2008
        internal void SetMinMaxFromRangeAttribute(PropertyDescriptor propertyDescriptor, TypeConverter converter)
        {
            if (propertyDescriptor == null)
                return;

            RangeAttribute rangeAttribute = PropertyGridUtilities.GetAttribute<RangeAttribute>(propertyDescriptor);
            if (rangeAttribute != null)
            {
                this.Editor.Maximum = (TType)converter.ConvertFrom(rangeAttribute.Maximum.ToString());
                this.Editor.Minimum = (TType)converter.ConvertFrom(rangeAttribute.Minimum.ToString());
            }
        }
#endif
    }

    public class ByteUpDownEditor : UpDownEditor<ByteUpDown, byte?>
    {
        protected override ByteUpDown CreateEditor()
        {
            return new PropertyGridEditorByteUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(byte)));
#endif
        }
    }

    public class DecimalUpDownEditor : UpDownEditor<DecimalUpDown, decimal?>
    {
        protected override DecimalUpDown CreateEditor()
        {
            return new PropertyGridEditorDecimalUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(decimal)));
#endif
        }
    }

    public class DoubleUpDownEditor : UpDownEditor<DoubleUpDown, double?>
    {
        protected override DoubleUpDown CreateEditor()
        {
            return new PropertyGridEditorDoubleUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
            this.Editor.AllowInputSpecialValues = AllowedSpecialValues.Any;
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(double)));
#endif
        }
    }

    public class IntegerUpDownEditor : UpDownEditor<IntegerUpDown, int?>
    {
        protected override IntegerUpDown CreateEditor()
        {
            return new PropertyGridEditorIntegerUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(int)));
#endif
        }
    }

    public class LongUpDownEditor : UpDownEditor<LongUpDown, long?>
    {
        protected override LongUpDown CreateEditor()
        {
            return new PropertyGridEditorLongUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(long)));
#endif
        }
    }

    public class ShortUpDownEditor : UpDownEditor<ShortUpDown, short?>
    {
        protected override ShortUpDown CreateEditor()
        {
            return new PropertyGridEditorShortUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(short)));
#endif
        }
    }

    public class SingleUpDownEditor : UpDownEditor<SingleUpDown, float?>
    {
        protected override SingleUpDown CreateEditor()
        {
            return new PropertyGridEditorSingleUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
            this.Editor.AllowInputSpecialValues = AllowedSpecialValues.Any;
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(float)));
#endif
        }
    }

    public class DateTimeUpDownEditor : UpDownEditor<DateTimeUpDown, DateTime?>
    {
        protected override DateTimeUpDown CreateEditor()
        {
            return new PropertyGridEditorDateTimeUpDown();
        }
        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(DateTime)));
#endif
        }
    }

    public class TimeSpanUpDownEditor : UpDownEditor<TimeSpanUpDown, TimeSpan?>
    {
        protected override TimeSpanUpDown CreateEditor()
        {
            return new PropertyGridEditorTimeSpanUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(TimeSpan)));
#endif
        }
    }

    internal class SByteUpDownEditor : UpDownEditor<SByteUpDown, sbyte?>
    {
        protected override SByteUpDown CreateEditor()
        {
            return new PropertyGridEditorSByteUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(sbyte)));
#endif
        }
    }

    internal class UIntegerUpDownEditor : UpDownEditor<UIntegerUpDown, uint?>
    {
        protected override UIntegerUpDown CreateEditor()
        {
            return new PropertyGridEditorUIntegerUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(uint)));
#endif
        }
    }

    internal class ULongUpDownEditor : UpDownEditor<ULongUpDown, ulong?>
    {
        protected override ULongUpDown CreateEditor()
        {
            return new PropertyGridEditorULongUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(ulong)));
#endif
        }
    }

    internal class UShortUpDownEditor : UpDownEditor<UShortUpDown, ushort?>
    {
        protected override UShortUpDown CreateEditor()
        {
            return new PropertyGridEditorUShortUpDown();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            base.SetControlProperties(propertyItem);
#if !VS2008
            this.SetMinMaxFromRangeAttribute(propertyItem.PropertyDescriptor, TypeDescriptor.GetConverter(typeof(ushort)));
#endif
        }
    }



    public class PropertyGridEditorByteUpDown : ByteUpDown
    {
        static PropertyGridEditorByteUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorByteUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorByteUpDown)));
        }
    }

    public class PropertyGridEditorDecimalUpDown : DecimalUpDown
    {
        static PropertyGridEditorDecimalUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorDecimalUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorDecimalUpDown)));
        }
    }

    public class PropertyGridEditorDoubleUpDown : DoubleUpDown
    {
        static PropertyGridEditorDoubleUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorDoubleUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorDoubleUpDown)));
        }
    }

    public class PropertyGridEditorIntegerUpDown : IntegerUpDown
    {
        static PropertyGridEditorIntegerUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorIntegerUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorIntegerUpDown)));
        }
    }

    public class PropertyGridEditorLongUpDown : LongUpDown
    {
        static PropertyGridEditorLongUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorLongUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorLongUpDown)));
        }
    }

    public class PropertyGridEditorShortUpDown : ShortUpDown
    {
        static PropertyGridEditorShortUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorShortUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorShortUpDown)));
        }
    }

    public class PropertyGridEditorSingleUpDown : SingleUpDown
    {
        static PropertyGridEditorSingleUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorSingleUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorSingleUpDown)));
        }
    }

    public class PropertyGridEditorDateTimeUpDown : DateTimeUpDown
    {
        static PropertyGridEditorDateTimeUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorDateTimeUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorDateTimeUpDown)));
        }
    }

    public class PropertyGridEditorTimeSpanUpDown : TimeSpanUpDown
    {
        static PropertyGridEditorTimeSpanUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorTimeSpanUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorTimeSpanUpDown)));
        }
    }

    internal class PropertyGridEditorSByteUpDown : SByteUpDown
    {
        static PropertyGridEditorSByteUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorSByteUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorSByteUpDown)));
        }
    }

    internal class PropertyGridEditorUIntegerUpDown : UIntegerUpDown
    {
        static PropertyGridEditorUIntegerUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorUIntegerUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorUIntegerUpDown)));
        }
    }

    internal class PropertyGridEditorULongUpDown : ULongUpDown
    {
        static PropertyGridEditorULongUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorULongUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorULongUpDown)));
        }
    }

    internal class PropertyGridEditorUShortUpDown : UShortUpDown
    {
        static PropertyGridEditorUShortUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorUShortUpDown), new FrameworkPropertyMetadata(typeof(PropertyGridEditorUShortUpDown)));
        }
    }

}
