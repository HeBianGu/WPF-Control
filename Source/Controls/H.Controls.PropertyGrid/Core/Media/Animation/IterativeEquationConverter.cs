// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Globalization;

namespace H.Controls.PropertyGrid.Media.Animation
{
    public class IterativeEquationConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext, Type type)
        {
            return type == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type type)
        {
            return type == typeof(IterativeEquation<double>);
        }

        public override object ConvertFrom(
          ITypeDescriptorContext typeDescriptorContext,
          CultureInfo cultureInfo,
          object value)
        {
            IterativeEquation<double> result = null;

            if (value is string)
            {
                switch (value as string)
                {
                    case "BackEaseIn":
                        result = PennerEquations.BackEaseIn;
                        break;
                    case "BackEaseInOut":
                        result = PennerEquations.BackEaseInOut;
                        break;
                    case "BackEaseOut":
                        result = PennerEquations.BackEaseOut;
                        break;
                    case "BounceEaseIn":
                        result = PennerEquations.BounceEaseIn;
                        break;
                    case "BounceEaseInOut":
                        result = PennerEquations.BounceEaseInOut;
                        break;
                    case "BounceEaseOut":
                        result = PennerEquations.BounceEaseOut;
                        break;
                    case "CircEaseIn":
                        result = PennerEquations.CircEaseIn;
                        break;
                    case "CircEaseInOut":
                        result = PennerEquations.CircEaseInOut;
                        break;
                    case "CircEaseOut":
                        result = PennerEquations.CircEaseOut;
                        break;
                    case "CubicEaseIn":
                        result = PennerEquations.CubicEaseIn;
                        break;
                    case "CubicEaseInOut":
                        result = PennerEquations.CubicEaseInOut;
                        break;
                    case "CubicEaseOut":
                        result = PennerEquations.CubicEaseOut;
                        break;
                    case "ElasticEaseIn":
                        result = PennerEquations.ElasticEaseIn;
                        break;
                    case "ElasticEaseInOut":
                        result = PennerEquations.ElasticEaseInOut;
                        break;
                    case "ElasticEaseOut":
                        result = PennerEquations.ElasticEaseOut;
                        break;
                    case "ExpoEaseIn":
                        result = PennerEquations.ExpoEaseIn;
                        break;
                    case "ExpoEaseInOut":
                        result = PennerEquations.ExpoEaseInOut;
                        break;
                    case "ExpoEaseOut":
                        result = PennerEquations.ExpoEaseOut;
                        break;
                    case "Linear":
                        result = PennerEquations.Linear;
                        break;
                    case "QuadEaseIn":
                        result = PennerEquations.QuadEaseIn;
                        break;
                    case "QuadEaseInOut":
                        result = PennerEquations.QuadEaseInOut;
                        break;
                    case "QuadEaseOut":
                        result = PennerEquations.QuadEaseOut;
                        break;
                    case "QuartEaseIn":
                        result = PennerEquations.QuartEaseIn;
                        break;
                    case "QuartEaseInOut":
                        result = PennerEquations.QuartEaseInOut;
                        break;
                    case "QuartEaseOut":
                        result = PennerEquations.QuartEaseOut;
                        break;
                    case "QuintEaseIn":
                        result = PennerEquations.QuintEaseIn;
                        break;
                    case "QuintEaseInOut":
                        result = PennerEquations.QuintEaseInOut;
                        break;
                    case "QuintEaseOut":
                        result = PennerEquations.QuintEaseOut;
                        break;
                    case "SineEaseIn":
                        result = PennerEquations.SineEaseIn;
                        break;
                    case "SineEaseInOut":
                        result = PennerEquations.SineEaseInOut;
                        break;
                    case "SineEaseOut":
                        result = PennerEquations.SineEaseOut;
                        break;
                }
            }

            return result;
        }
    }
}
