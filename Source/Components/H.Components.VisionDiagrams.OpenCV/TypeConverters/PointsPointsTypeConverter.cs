// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.Components.VisionDiagrams.OpenCV.TypeConverters;
/// <summary>
/// Converts an array of Point arrays (Point[][]) to and from a string.
/// Format: (X1,Y1;X2,Y2)|(X3,Y3;X4,Y4)
/// </summary>
public class PointsPointsTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                return null;

            try
            {
                // 按 '|' 分割成多个轮廓字符串
                var contourStrings = stringValue.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                var contours = contourStrings.Select(contourStr =>
                {
                    // 去掉括号并按 ';' 分割成点字符串
                    var pointStrings = contourStr.Trim('(', ')').Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    var points = pointStrings.Select(pointStr =>
                    {
                        // 按 ',' 分割成坐标值
                        var coords = pointStr.Split(',');
                        if (coords.Length == 2 &&
                            int.TryParse(coords[0], NumberStyles.Integer, culture, out int x) &&
                            int.TryParse(coords[1], NumberStyles.Integer, culture, out int y))
                        {
                            return new OpenCvSharp.Point(x, y);
                        }
                        throw new FormatException($"Invalid point format: '{pointStr}'");
                    }).ToArray();

                    return points;
                }).ToArray();

                return contours;
            }
            catch (Exception ex)
            {
                throw new FormatException($"Cannot convert string '{stringValue}' to Point[][].", ex);
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is OpenCvSharp.Point[][] contours)
        {
            if (contours == null || contours.Length == 0)
                return string.Empty;

            // 将每个轮廓转换为字符串
            var contourStrings = contours.Select(contour =>
            {
                if (contour == null || contour.Length == 0)
                    return "()";

                // 将每个点转换为 "X,Y" 格式
                var pointStrings = contour.Select(p => $"{p.X},{p.Y}");
                return $"({string.Join(";", pointStrings)})";
            });

            // 用 '|' 连接所有轮廓字符串
            return string.Join("|", contourStrings);
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
