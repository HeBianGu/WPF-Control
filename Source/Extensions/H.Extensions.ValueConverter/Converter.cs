// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Common;
using H.Mvvm.Commands;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Extensions.ValueConverter;

public static partial class Converter
{
    #region - TimeSpan -
    public static IValueConverter GetTimeSpanStrFromSeconds => new ConverterBase<int, string>(x => TimeSpan.FromSeconds(x).TimespanToString());
    public static IValueConverter GetTimeSpanStrFromMilliseconds => new ConverterBase<int, string>(x => TimeSpan.FromMilliseconds(x).TimespanToString());
    public static IValueConverter GetTimeSpanStrFromMinutes => new ConverterBase<int, string>(x => TimeSpan.FromMinutes(x).TimespanToString());
    public static IValueConverter GetTimeSpanStrFromDays => new ConverterBase<int, string>(x => TimeSpan.FromDays(x).TimespanToString());
    public static IValueConverter GetTimeSpanStrFromHours => new ConverterBase<int, string>(x => TimeSpan.FromHours(x).TimespanToString());
    public static IValueConverter GetTimeSpanStrFromTicks => new ConverterBase<long, string>(x => TimeSpan.FromTicks(x).TimespanToString());
    public static IValueConverter GetTimeSpanStr => new ConverterBase<TimeSpan, string>(x => x.TimespanToString());

    public static string TimespanToDislay(this TimeSpan t)
    {
        StringBuilder sb = new StringBuilder();
        if (t.TotalDays > 0)
            sb.Append($"{t.Days}天 ");
        if (t.TotalHours > 0)
            sb.Append($"{t.Hours}小时 ");
        if (t.TotalMinutes > 0)
            sb.Append($"{t.Minutes}分 ");
        if (t.TotalSeconds > 0)
            sb.Append($"{t.Seconds}秒");
        return sb.ToString();
    }


    public static string TimespanToString(this TimeSpan t)
    {
        StringBuilder sb = new StringBuilder();
        if (t.TotalDays > 0)
            sb.Append($"{t.Days}.");
        if (t.TotalHours > 0)
            sb.Append($"{t.Hours.ToString().PadLeft(2, '0')}:");
        if (t.TotalMinutes > 0)
            sb.Append($"{t.Minutes.ToString().PadLeft(2, '0')}:");
        if (t.TotalSeconds > 0)
            sb.Append($"{t.Seconds.ToString().PadLeft(2, '0')}");
        return sb.ToString();
    }


    #endregion

    #region - Math -
    public static IValueConverter GetMathAbs => new ConverterBase<double, double>(x => Math.Abs(x));
    public static IValueConverter GetMathPow => new ConverterBase<double, double, double>((x, y) => Math.Pow(x, y));
    public static IValueConverter GetMathSqrt => new ConverterBase<double, double>(x => Math.Sqrt(x));
    public static IValueConverter GetMathFloor => new ConverterBase<double, double>(x => Math.Floor(x));
    public static IValueConverter GetMathExp => new ConverterBase<double, double>(x => Math.Exp(x));
    public static IValueConverter GetMathLog => new ConverterBase<double, double>(x => Math.Log(x));
    public static IValueConverter GetMathSin => new ConverterBase<double, double>(x => Math.Sin(x));
    public static IValueConverter GetMathCos => new ConverterBase<double, double>(x => Math.Cos(x));
    public static IValueConverter GetMathTan => new ConverterBase<double, double>(x => Math.Tan(x));
    public static IValueConverter GetMathMin => new ConverterBase<double, double, double>((x, y) => Math.Min(x, y));
    public static IValueConverter GetMathMax => new ConverterBase<double, double, double>((x, y) => Math.Max(x, y));
    public static IValueConverter GetMathRound => new ConverterBase<double, int, double>((x, y) => Math.Round(x, y));

    public static IValueConverter GetMathAddition => new ConverterBase<double, double, double>((x, y) => x + y);
    public static IValueConverter GetMathMultiplication => new ConverterBase<double, double, double>((x, y) => x * y);


    #endregion

    #region - Oparetion -
    public static IValueConverter GetGreaterThan => new ConverterBase<double, double, bool>((x, y) => x > y);
    public static IValueConverter GetLessThan => new ConverterBase<double, double, bool>((x, y) => x < y);

    #endregion

    #region - String -
    public static IValueConverter GetStringTrim => new ConverterBase<string, string>(x => x.Trim());
    public static IValueConverter GetStringToUpper => new ConverterBase<string, string>(x => x.ToUpper());
    public static IValueConverter GetStringToLower => new ConverterBase<string, string>(x => x.ToLower());
    public static IValueConverter GetStringToCharArray => new ConverterBase<string, char[]>(x => x.ToCharArray());
    public static IValueConverter GetStringIsNullOrEmpty => new ConverterBase<string, bool>(x => string.IsNullOrEmpty(x));
    public static IValueConverter GetStringContains => new ConverterBase<string, string, bool>((x, y) => x.Contains(y));
    public static IValueConverter GetStringEndsWith => new ConverterBase<string, string, bool>((x, y) => x.EndsWith(y));
    public static IValueConverter GetStringStartsWith => new ConverterBase<string, string, bool>((x, y) => x.StartsWith(y));
    public static IValueConverter GetStringPadLeft => new ConverterBase<string, int, string>((x, y) => x.PadLeft(y));
    public static IValueConverter GetStringPadRight => new ConverterBase<string, int, string>((x, y) => x.PadRight(y));
    public static IValueConverter GetStringSplit => new ConverterBase<string, char[], string[]>((x, y) => x.Split(y));
    public static IValueConverter GetStringSubstring => new ConverterBase<string, int, string>((x, y) => x.Substring(y));
    public static IValueConverter GetStringFormat => new ConverterBase<object, string, string>((x, y) => string.Format(y, x));
    public static IValueConverter GetStringJoin => new ConverterBase<string[], string, string>((x, y) => string.Join(y, x));

    public static IValueConverter GetBoolenToConnectState => new ConverterBase<bool, string>(x => x ? "已连接" : "未连接");
    public static IValueConverter GetBoolenNullToConnectState => new ConverterBase<bool?, string>(x => !x.HasValue ? "未启动" : x.Value ? "已连接" : "未连接") { DefaultR = "未启动" };
    public static IValueConverter GetBoolenToPassState => new ConverterBase<bool, string>(x => x ? "合格" : "不合格");

    public static IValueConverter GetIntLessZeroString => new ConverterBase<int, string, string>((x, y) => x < 0 ? y : x.ToString());

    #endregion

    #region - IConvertible -
    public static IValueConverter GetIConvertibler { get; } = new Lazy<IConvertibleConverter>().Value;
    #endregion

    #region - IEnumerable -
    public static IValueConverter GetIEnumerableMax => new IEnumerableConverterBase<object, object>(x => x.Max());
    public static IValueConverter GetIEnumerableMin => new IEnumerableConverterBase<object, object>(x => x.Min());
    public static IValueConverter GetIEnumerableCount => new IEnumerableConverterBase<object, object>(x => x.Count());
    public static IValueConverter GetIEnumerableFirstOrDefault => new IEnumerableConverterBase<object, object>(x => x.FirstOrDefault());
    public static IValueConverter GetIEnumerableLastOrDefault => new IEnumerableConverterBase<object, object>(x => x.LastOrDefault());

    public static IValueConverter GetIEnumerableTake => new IEnumerableTypeConverterBase<object, int>((x, p, t) =>
    {
        IList list = Activator.CreateInstance(x.GetType()) as IList;

        foreach (object item in x.Take(p))
        {
            list.Add(item);
        }
        return list;
    });
    public static IValueConverter GetIEnumerableCast => new IEnumerableTypeConverterBase<object, int>((x, p, t) =>
    {
        IList list = Activator.CreateInstance(x.GetType()) as IList;
        foreach (object item in x)
        {
            list.Add(item);
        }
        return list;
    });
    public static IValueConverter GetIEnumerableElementAt => new IEnumerableTypeConverterBase<object, int>((x, p, t) =>
    {
        return x.ElementAt(p);
    });

    public static IValueConverter GetIEnumerableDistinct => new IEnumerableTypeConverterBase<object>((x, t) =>
    {
        IList list = Activator.CreateInstance(x.GetType()) as IList;
        foreach (object item in x.Distinct())
        {
            list.Add(item);
        }
        return list;
    });
    public static IValueConverter GetIEnumerableExcept => new IEnumerableTypeConverterBase<object, IEnumerable<object>>((x, p, t) =>
    {
        IList list = Activator.CreateInstance(x.GetType()) as IList;
        foreach (object item in x.Except(p))
        {
            list.Add(item);
        }
        return list;
    });
    public static IValueConverter GetIEnumerableReverse => new IEnumerableTypeConverterBase<object>((x, t) =>
    {
        IList list = Activator.CreateInstance(x.GetType()) as IList;
        foreach (object item in x.Reverse())
        {
            list.Add(item);
        }
        return list;
    });
    public static IValueConverter GetIEnumerablePropertyList => new IEnumerableConverterBase<object, string, object>((x, p) =>
    {
        if (x.GetType().IsGenericType)
        {
            Type gType = x.GetGenericArgumentType();
            PropertyInfo property = gType.GetProperty(p);
            Type type = typeof(List<>);
            Type listType = type.MakeGenericType(property.PropertyType);
            IList list = Activator.CreateInstance(listType) as IList;
            foreach (object item in x)
            {
                object value = property.GetValue(item);
                list.Add(value);
            }
            return list;
        }
        return null;
    });
    #endregion

    #region - ICompare -
    public static IValueConverter GetComparable => new ConverterBase<IComparable, IComparable, int>((x, y) => x.CompareTo(y));
    #endregion

    #region - Type -
    public static IValueConverter GetObjType => new ConverterBase<object, Type>(x => x.GetType());
    public static IValueConverter GetObjTypeName => new ConverterBase<object, string>(x => x.GetType().Name);
    public static IValueConverter GetObjTypeFullName => new ConverterBase<object, string>(x => x.GetType().FullName);
    public static IValueConverter GetDiaplayName => new ConverterBase<object, string>(x => x.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? x.GetType().Name);
    public static IValueConverter GetDiaplayDescription => new ConverterBase<object, string>(x => x.GetType().GetCustomAttribute<DisplayAttribute>()?.Description);
    public static IValueConverter GetIsAssignableFrom => new ConverterBase<object, Type, bool>((x, y) =>
    {
        bool r = y.IsAssignableFrom(x.GetType());
        return r;
    });

    public static IValueConverter GetPropertyInfoDiaplayName => new ConverterBase<PropertyInfo, string>(x => x.GetCustomAttribute<DisplayAttribute>()?.Name ?? x.Name);
    public static IValueConverter GetIsValueType => new ConverterBase<object, bool>(x => x.GetType().IsValueType);
    public static IValueConverter GetIsClass => new ConverterBase<object, bool>(x => x.GetType().IsClass);
    public static IValueConverter GetIsEnum => new ConverterBase<object, bool>(x => x.GetType().IsEnum);
    public static IValueConverter GetIsGenericType => new ConverterBase<object, bool>(x => x.GetType().IsGenericType);
    public static IValueConverter IsInterface => new ConverterBase<object, bool>(x => x.GetType().IsInterface);
    public static IValueConverter GetIsAbstract => new ConverterBase<object, bool>(x => x.GetType().IsAbstract);
    public static IValueConverter GetIsArray => new ConverterBase<object, bool>(x => x.GetType().IsArray);

    public static IValueConverter GetCommands => new ConverterBase<object, IList<ICommand>>(x =>
    {
        var ps = x.GetType().GetProperties().Where(k => typeof(ICommand).IsAssignableFrom(k.PropertyType));
        List<ICommand> result = new List<ICommand>();
        foreach (var item in ps)
        {
            if (item.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                continue;
            DisplayAttribute da = item.GetCustomAttribute<DisplayAttribute>(true);
            if (item.GetValue(x) is ICommand command)
            {
                if (command is IDisplayCommand display)
                {
                    display.Name = display.Name ?? da.Name;
                    display.Description = display.Description ?? da.Description;
                    display.Order = da.Order;
                    display.GroupName = display.GroupName ?? da.GroupName;
                }
                result.Add(command);
            }
        }
        return result.OrderBy(k => k is IDisplayCommand d ? d.Order : 0).ToList();
    });
    #endregion

    #region - Path -
    public static IValueConverter GetDirectoryName => new ConverterBase<string, string>(x => System.IO.Path.GetDirectoryName(x));
    public static IValueConverter GetExtension => new ConverterBase<string, string>(x => System.IO.Path.GetExtension(x));
    public static IValueConverter GetFileName => new ConverterBase<string, string>(x => System.IO.Path.GetFileName(x));
    public static IValueConverter GetFileNameWithoutExtension => new ConverterBase<string, string>(x => System.IO.Path.GetFileNameWithoutExtension(x));
    public static IValueConverter GetFullPath => new ConverterBase<string, string>(x => System.IO.Path.GetFullPath(x));
    public static IValueConverter GetPathRoot => new ConverterBase<string, string>(x => System.IO.Path.GetPathRoot(x));
    public static IValueConverter GetChangeExtension => new ConverterBase<string, string, string>((x, y) => System.IO.Path.ChangeExtension(x, y));
    public static IValueConverter GetHasExtension => new ConverterBase<string, bool>(x => System.IO.Path.HasExtension(x));
    #endregion

    #region - File -
    public static IValueConverter GetFileReadAllText => new ConverterBase<string, string>(x => System.IO.File.ReadAllText(x));
    public static IValueConverter GetFileExists => new ConverterBase<string, bool>(x => System.IO.File.Exists(x)) { DefaultR = false };
    public static IValueConverter GetFileCreationTime => new ConverterBase<string, DateTime>(x => System.IO.File.GetCreationTime(x));
    public static IValueConverter GetFileLastAccessTime => new ConverterBase<string, DateTime>(x => System.IO.File.GetLastAccessTime(x));
    public static IValueConverter GetFileFileAttributes => new ConverterBase<string, System.IO.FileAttributes>(x => System.IO.File.GetAttributes(x));
    public static IValueConverter GetFileImageSource => new ConverterBase<string, ImageSource>(x => new BitmapImage(new Uri(x, UriKind.RelativeOrAbsolute)));
    public static IValueConverter GetFileLength => new ConverterBase<string, long>(x => new FileInfo(x).Length);
    //public static IValueConverter GetFileLengthDisplay => new ConverterBase<string, string>(x =>
    //{
    //    if (x == null) return null;
    //    if (!File.Exists(x.ToString()))
    //        return null;
    //    FileInfo info = new FileInfo(x.ToString());
    //    return XConverter.ByteSizeDisplayConverter.Convert(info.Length, null, null, null)?.ToString();
    //});
    #endregion

    #region - Image -
    public static IValueConverter GetImagePixelDisplay => new ConverterBase<string, string>(x =>
    {
        if (x == null)
            return null;
        string filePath = x.ToString();
        var tuple = filePath.ToImageEx().GetImagePixel();
        if (tuple == null)
            return null;
        return tuple.Item1 + "×" + tuple.Item2;
    });

    public static IValueConverter GetFileImageSourceInMemory => new ConverterBase<string, int, ImageSource>((x, p) =>
    {
        if (x == null)
            return null;
        string filePath = x.ToString();
        return filePath.ToImageEx().GetImageSource(p, 0, true);
    });

    #endregion

    #region - Directory -
    public static IValueConverter GetDirectoryExists => new ConverterBase<string, bool>(x => System.IO.Directory.Exists(x));
    public static IValueConverter GetDirectoryFiles => new ConverterBase<string, string[]>(x => System.IO.Directory.GetFiles(x));
    public static IValueConverter GetDirectoryCreationTime => new ConverterBase<string, DateTime>(x => System.IO.Directory.GetCreationTime(x));
    public static IValueConverter GetDirectoryDirectories => new ConverterBase<string, string[]>(x => System.IO.Directory.GetDirectories(x));
    public static IValueConverter GetDirectoryFileSystemEntries => new ConverterBase<string, string[]>(x => System.IO.Directory.GetFileSystemEntries(x));
    public static IValueConverter GetDirectoryLastAccessTime => new ConverterBase<string, DateTime>(x => System.IO.Directory.GetLastAccessTime(x));
    public static IValueConverter GetDirectoryLastWriteTime => new ConverterBase<string, DateTime>(x => System.IO.Directory.GetLastWriteTime(x));
    public static IValueConverter GetDirectoryParent => new ConverterBase<string, System.IO.DirectoryInfo>(x => System.IO.Directory.GetParent(x));

    public static IValueConverter GetAllFile => new ConverterBase<string, List<string>>(x => x.ToDirectoryEx().GetAllFiles());

    #endregion

    #region - Bool -
    public static IValueConverter GetTrueToFalse => new ConverterBase<bool, bool>(x => !x);
    public static IValueConverter GetIntToBoolen => new ConverterBase<int, bool>(x => x == 1);
    public static IValueConverter GetNullToFalse => new ConverterBase<object, bool>(x => x != null);
    public static IValueConverter GetTrue => new ConverterBase<object, bool>(x => true);
    public static IValueConverter GetFalse => new ConverterBase<object, bool>(x => false);
    #endregion

    #region - Visibility -
    public static IValueConverter GetBoolNullFalseToVisble => new ConverterBase<bool?, Visibility>(x => x != false ? Visibility.Collapsed : Visibility.Visible) { DefaultR = Visibility.Collapsed };
    public static IValueConverter GetBoolNullAndTrueToVisble => new ConverterBase<bool?, Visibility>(x => x != false ? Visibility.Visible : Visibility.Collapsed) { DefaultR = Visibility.Visible };

    public static IValueConverter GetTrueToCollapsed => new ConverterBase<bool, Visibility>(x => x ? Visibility.Collapsed : Visibility.Visible);
    public static IValueConverter GetTrueToHidden => new ConverterBase<bool, Visibility>(x => x ? Visibility.Hidden : Visibility.Visible);
    public static IValueConverter GetFalseToHidden => new ConverterBase<bool, Visibility>(x => x ? Visibility.Visible : Visibility.Hidden);
    public static IValueConverter GetTrueToVisible => new ConverterBase<bool, Visibility>(x => x ? Visibility.Visible : Visibility.Collapsed);

    public static IValueConverter GetVisibleToTrue => new ConverterBase<Visibility, bool>(x => x == Visibility.Visible, x => x ? Visibility.Visible : Visibility.Collapsed);
    public static IMultiValueConverter GetTrueAllToVisible => new MultiConverterBase<bool, Visibility>(x => x.All(l => l == true) ? Visibility.Visible : Visibility.Collapsed);
    public static IValueConverter GetNullToCollapsed => new ConverterBase<object, Visibility>(x => x == null ? Visibility.Collapsed : Visibility.Visible) { DefaultR = Visibility.Collapsed };
    public static IValueConverter GetNullToVisible => new ConverterBase<object, Visibility>(x => x == null ? Visibility.Visible : Visibility.Collapsed) { DefaultR = Visibility.Visible };
    public static IValueConverter GetListEmptyToCollapsed => new ConverterBase<IList, Visibility>(x => x == null || x.Count == 0 ? Visibility.Collapsed : Visibility.Visible) { DefaultR = Visibility.Collapsed };
    public static IValueConverter GetListEmptyToVisible => new ConverterBase<IList, Visibility>(x => x == null || x.Count == 0 ? Visibility.Visible : Visibility.Collapsed) { DefaultR = Visibility.Visible };

    public static IMultiValueConverter GetAllTrueToVisible => new MultiConverterBase<bool, Visibility>(x => x.All(k => k) ? Visibility.Visible : Visibility.Collapsed);

    public static IValueConverter GetIntToCollapsed => new ConverterBase<int, int, Visibility>((x, y) => x == y ? Visibility.Collapsed : Visibility.Visible);
    public static IValueConverter GetIntToVisible => new ConverterBase<int, int, Visibility>((x, y) => x == y ? Visibility.Visible : Visibility.Collapsed);

    public static IMultiValueConverter GetAllEqualsToVisible => new MultiConverterBase<object, Visibility>(x =>
    {
        object first = x.FirstOrDefault();
        if (first == null)
            return Visibility.Collapsed;
        bool SSS = x.All(l => l.Equals(first));
        return x.All(l => l.Equals(first)) ? Visibility.Visible : Visibility.Collapsed;
    });

    public static IMultiValueConverter GetComparableAllTrueToVisible => new MultiConverterBase<IComparable, Visibility>(x =>
    {
        IComparable first = x.FirstOrDefault();
        if (first == null)
            return Visibility.Collapsed;
        return x.All(l => l.CompareTo(first) == 0) ? Visibility.Visible : Visibility.Collapsed;
    });

    #endregion

    #region - DateTime -
    public static IValueConverter GetDateTimeToString => new ConverterBase<DateTime, string>(x => x.ToString("yyyy-MM-dd HH:mm:ss"));
    public static IValueConverter GetDateTimeToDateString => new ConverterBase<DateTime, string>(x => x.ToString("yyyy-MM-dd"));
    public static IValueConverter GetDateTimeToAge => new ConverterBase<DateTime, int>(x =>
    {
        TimeSpan span = DateTime.Now - x;
        return (int)(span.TotalDays / 365.0);
    });

    public static IValueConverter GetDateTimeToDate => new ConverterBase<DateTime, DateTime>(x => x.Date);
    public static IValueConverter GetDateTimeToAddDays => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddDays(y));
    public static IValueConverter GetDateTimeToAddHours => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddHours(y));
    public static IValueConverter GetDateTimeToAddMilliseconds => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddMilliseconds(y));
    public static IValueConverter GetDateTimeToAddSeconds => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddSeconds(y));
    public static IValueConverter GetDateTimeToAddMonths => new ConverterBase<DateTime, int, DateTime>((x, y) => x.AddMonths(y));
    public static IValueConverter GetDateTimeToAddTicks => new ConverterBase<DateTime, long, DateTime>((x, y) => x.AddTicks(y));
    public static IValueConverter GetDateTimeToAddYears => new ConverterBase<DateTime, int, DateTime>((x, y) => x.AddYears(y));
    public static IValueConverter GetDateTimeParseExact => new ConverterBase<string, DateTime>(x => DateTime.ParseExact(x, "yyyy-MM-dd HH:mm:ss", new CultureInfo("zh-CN")));
    public static IValueConverter GetDateTimeToTicks => new ConverterBase<DateTime, long>(x => x.Ticks);

    public static IValueConverter GetDateTimeNowToString => new ConverterBase<object, string>(x => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    #endregion

    #region - Uri -
    public static IValueConverter GetUri => new ConverterBase<string, Uri>(x => Uri.TryCreate(x, UriKind.RelativeOrAbsolute, out Uri uri) ? uri : null);
    public static IValueConverter GetUriRelative => new ConverterBase<string, Uri>(x => Uri.TryCreate(x, UriKind.Relative, out Uri uri) ? uri : null);
    public static IValueConverter GetUriAbsolute => new ConverterBase<string, Uri>(x => Uri.TryCreate(x, UriKind.Absolute, out Uri uri) ? uri : null);

    #endregion

    #region - Brush -
    //public static ConverterBase<bool?, Brush> GetBoolenNullBrush => new ConverterBase<bool?, Brush>(x =>
    //{
    //    if (!x.HasValue) return Brushes.Transparent;
    //    return x.Value ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush;
    //});

    //public static ConverterBase<bool?, Brush> GetBoolNullBrushDefaultGray => new ConverterBase<bool?, Brush>(x => !x.HasValue ? Brushes.Gray : x.Value ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush) { DefaultR = Brushes.Gray };

    //public static ConverterBase<bool, Brush> GetBoolenBrush => new ConverterBase<bool, Brush>(x => x ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush);
    //public static ConverterBase<int, Brush> GetIntBrush => new ConverterBase<int, Brush>(x =>
    //{
    //    if (x <= 0)
    //        return Application.Current.FindResource(BrushKeys.Green) as Brush;
    //    if (x == 1)
    //        return Brushes.SkyBlue;
    //    if (x == 2)
    //        return Application.Current.FindResource(BrushKeys.Yellow) as Brush;
    //    if (x == 3)
    //        return Application.Current.FindResource(BrushKeys.Orange) as Brush;
    //    if (x == 4)
    //        return Application.Current.FindResource(BrushKeys.Red) as Brush;
    //    if (x == 5)
    //        return Brushes.Purple;

    //    return null;
    //});

    //public static ConverterBase<string, Brush> GetPassStateBrush => new ConverterBase<string, Brush>(x => x == "合格" ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush);

    //public static ConverterBase<string, Brush> GetStateBrush => new ConverterBase<string, Brush>(x =>
    //{
    //    if (x.Contains("不") || x.Contains("失败") || x.Contains("错误") || x.Contains("断开") || x.Contains("否") || x.Contains("停止"))
    //        return Application.Current.FindResource(BrushKeys.Red) as Brush;
    //    return Application.Current.FindResource(BrushKeys.Green) as Brush;
    //});


    //public static ConverterBase<string, Color> GetStateColor => new ConverterBase<string, Color>(x =>
    //{
    //    if (x.Contains("不") || x.Contains("失败") || x.Contains("错误") || x.Contains("断开") || x.Contains("否") || x.Contains("停止"))
    //        return (Application.Current.FindResource(BrushKeys.Red) as SolidColorBrush).Color;
    //    return (Application.Current.FindResource(BrushKeys.Red) as SolidColorBrush).Color;
    //});

    public static IValueConverter GetColorToSolidColorBrush => new ConverterBase<Color, SolidColorBrush>(x => new SolidColorBrush(x), x => x.Color);

    public static IValueConverter GetSolidColorBrushToColor => new ConverterBase<SolidColorBrush, Color>(x => x.Color, x => new SolidColorBrush(x));


    #endregion

    #region - TreeViewItem -
    public static IValueConverter GetTreeViewItemMargin => new ConverterBase<TreeViewItem, double, Thickness>((x, e) =>
     {
         int level = GetTreeViewItemLevel.Set(x);
         return new Thickness(level * e, 0, 0, 0);
     });

    public static ConverterBase<TreeViewItem, int> GetTreeViewItemLevel => new ConverterBase<TreeViewItem, int>(x =>
    {
        int level = 0;
        Action<TreeViewItem> action = null;
        action = t =>
        {
            ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(t);
            if (parent is TreeViewItem item)
            {
                level++;
                action(item);
            }

            return;
        };
        action.Invoke(x);
        return level;
    });

    //public static ConverterBase<TreeViewItem, bool> GetTreeViewItemContainSelected => new ConverterBase<TreeViewItem, bool>(x => x.GetChild<TreeViewItem>(k => k.IsSelected) != null);

    #endregion

    #region - CornerRadius -
    public static IValueConverter GetDoubleToCornerRadius => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, x, x, x));
    public static IValueConverter GetDoubleToCornerRadiusLeft => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, 0, 0, x));
    public static IValueConverter GetDoubleToCornerRadiusTop => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, x, 0, 0));
    public static IValueConverter GetDoubleToCornerRadiusRigth => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, x, x, 0));
    public static IValueConverter GetDoubleToCornerRadiusBottm => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, 0, x, x));
    public static IValueConverter GetDoubleToCornerRadiusLeftTop => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, 0, 0, 0));
    public static IValueConverter GetDoubleToCornerRadiusLeftBottom => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, 0, 0, x));
    public static IValueConverter GetDoubleToCornerRadiusRightTop => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, x, 0, 0));
    public static IValueConverter GetDoubleToCornerRadiusRightBottom => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, 0, x, 0));
    #endregion

    #region - Enum -
    public static IValueConverter GetEnumSource => new ConverterBase<Type, IEnumerable>(x =>
    {
        if (null == x)
            throw new InvalidOperationException("This EnumType must be specified.");
        Type actualEnumType = Nullable.GetUnderlyingType(x) ?? x;
        Array enumVlues = Enum.GetValues(actualEnumType);
        if (actualEnumType == x)
            return enumVlues;
        Array tempArray = Array.CreateInstance(actualEnumType, enumVlues.Length + 1);
        enumVlues.CopyTo(tempArray, 1);
        return tempArray;
    });
    #endregion

}

public class ConverterBase<T, P, R> : IValueConverter
{
    public ConverterBase(Func<T, P, R> set)
    {
        this.Set = set;
    }

    public ConverterBase(Func<T, P, R> set, Func<R, P, T> get) : this(set)
    {
        this.Get = get;
    }
    public Func<T, P, R> Set { get; set; }

    public Func<R, P, T> Get { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null) return default(R);
        if (value == null) return default(R);
        if (value is T t && parameter is P p)
        {
            return this.Set.Invoke(t, p);
        }
        if (!value.TryChangeType(out T t1))
        {
            return default(R);
        }

        if (!parameter.TryChangeType(out P p1))
        {
            return default(R);
        }

        return this.Set.Invoke(t1, p1);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Get == null) return default(T);
        if (value == null) return default(T);

        if (value is R t && parameter is P p)
        {
            return this.Get.Invoke(t, p);
        }

        //if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(typeof(R)) && parameter is P p1)
        //{
        //    var t1 = (R)System.Convert.ChangeType(value, typeof(T));
        //    return this.Get.Invoke(t1, p1);
        //}
        if (!value.TryChangeType(out T t1))
        {
            return default(T);
        }

        if (!parameter.TryChangeType(out P p1))
        {
            return default(T);
        }

        return this.Set.Invoke(t1, p1);
    }
}

public class ConverterBase<T, R> : IValueConverter
{
    public ConverterBase(Func<T, R> set)
    {
        this.Set = set;
    }

    public ConverterBase(Func<T, R> set, Func<R, T> get) : this(set)
    {
        this.Get = get;
    }
    public Func<T, R> Set { get; set; }

    public Func<R, T> Get { get; set; }

    public R DefaultR { get; set; } = default(R);
    public T DefaultT { get; set; } = default(T);

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null) return this.DefaultR;
        if (value == null) return this.DefaultR;

        if (value is T t)
        {
            return this.Set.Invoke(t);
        }

        //if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(typeof(T)))
        //{
        //    var t1 = (T)System.Convert.ChangeType(value, typeof(T));
        //    return this.Set.Invoke(t1);
        //} 

        //return default(R);

        if (value.TryChangeType(out T t1))
        {
            return this.Set.Invoke(t1);
        }

        return this.DefaultR;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Get == null) return this.DefaultT;
        if (value == null) return this.DefaultT;

        if (value is R t)
        {
            return this.Get.Invoke(t);
        }

        Type ss = value.GetType();
        Type sss = typeof(R);

        //if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(typeof(R)))
        //{
        //    var t1 = (R)System.Convert.ChangeType(value, typeof(T));
        //    return this.Get.Invoke(t1);
        //}

        //return default(T);

        if (value.TryChangeType(out R t1))
        {
            return this.Get.Invoke(t1);
        }

        return this.DefaultT;
    }
}

public class MultiConverterBase<T, R> : IMultiValueConverter
{
    public MultiConverterBase(Func<T[], R> set)
    {
        this.Set = set;
    }

    public MultiConverterBase(Func<T[], R> set, Func<R, T[]> get) : this(set)
    {
        this.Get = get;
    }
    public Func<T[], R> Set { get; set; }

    public Func<R, T[]> Get { get; set; }

    public R DefaultR { get; set; } = default(R);
    public T DefaultT { get; set; } = default(T);

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null) return this.DefaultR;
        if (values == null) return this.DefaultR;
        try
        {
            T[] ts = values.Cast<T>().ToArray();
            return this.Set.Invoke(ts);
        }
        catch
        {
            return this.DefaultR;
        }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IConvertibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(targetType))
        {
            return System.Convert.ChangeType(value, targetType);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IEnumerableConverterBase<T, R> : IValueConverter
{
    public IEnumerableConverterBase(Func<IEnumerable<T>, R> set)
    {
        this.Set = set;
    }

    public Func<IEnumerable<T>, R> Set { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null) return default(R);
        if (value == null) return default(R);

        if (value is IEnumerable<T> t)
        {
            return this.Set.Invoke(t);
        }

        return default(R);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IEnumerableConverterBase<T, P, R> : IValueConverter
{
    public IEnumerableConverterBase(Func<IEnumerable<T>, P, R> set)
    {
        this.Set = set;
    }

    public Func<IEnumerable<T>, P, R> Set { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null) return default(R);
        if (value == null) return default(R);

        if (value is IEnumerable<T> t && parameter is P p)
        {
            return this.Set.Invoke(t, p);
        }

        return default(R);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IEnumerableTypeConverterBase<T, P> : IValueConverter
{
    public IEnumerableTypeConverterBase(Func<IEnumerable<T>, P, Type, object> set)
    {
        this.Set = set;
    }

    public Func<IEnumerable<T>, P, Type, object> Set { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null)
            return value;
        if (value == null)
            return null;
        if (value is IEnumerable<T> t)
        {
            if (parameter.TryChangeType(out P p))
                return this.Set.Invoke(t, p, targetType);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IEnumerableTypeConverterBase<T> : IValueConverter
{
    public IEnumerableTypeConverterBase(Func<IEnumerable<T>, Type, object> set)
    {
        this.Set = set;
    }

    public Func<IEnumerable<T>, Type, object> Set { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.Set == null) return value;
        if (value == null) return null;

        if (value is IEnumerable<T> t)
        {
            return this.Set.Invoke(t, targetType);
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

//public class Display<T>
//{
//    public T Value { get; set; }
//    public int Order { get; set; }
//    public string GroupName { get; set; }
//    public string Description { get; set; }
//    public string Name { get; set; }
//    public string ID { get; set; }
//    public string Icon { get; set; }
//    public string TabName { get; set; }
//}
