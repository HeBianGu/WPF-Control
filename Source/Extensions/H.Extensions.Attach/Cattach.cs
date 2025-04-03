// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Win32;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Extensions.Attach;

public static partial class Cattach
{
    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
        "Watermark", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

    public static string GetWatermark(DependencyObject d)
    {
        return (string)d.GetValue(WatermarkProperty);
    }

    public static void SetWatermark(DependencyObject obj, string value)
    {
        obj.SetValue(WatermarkProperty, value);
    }

    [Obsolete]
    public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.RegisterAttached("AllowsAnimation"
        , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, AllowsAnimationChanged));
    [Obsolete]
    public static bool GetAllowsAnimation(DependencyObject d)
    {
        return (bool)d.GetValue(AllowsAnimationProperty);
    }
    [Obsolete]
    public static void SetAllowsAnimation(DependencyObject obj, bool value)
    {
        obj.SetValue(AllowsAnimationProperty, value);
    }
    private static DoubleAnimation RotateAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(200)));

    [Obsolete]
    private static void AllowsAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //var uc = d as FrameworkElement;
        //if (uc == null) return;
        //if (uc.RenderTransformOrigin == new Point(0, 0))
        //{
        //    uc.RenderTransformOrigin = new Point(0.5, 0.5);
        //    RotateTransform trans = new RotateTransform(0);
        //    uc.RenderTransform = trans;
        //}
        //var value = (bool)e.NewValue;
        //if (value)
        //{
        //    RotateAnimation.To = 180;
        //    uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
        //}
        //else
        //{
        //    RotateAnimation.To = 0;
        //    uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
        //}
    }
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        "CornerRadius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));

    public static CornerRadius GetCornerRadius(DependencyObject d)
    {
        return (CornerRadius)d.GetValue(CornerRadiusProperty);
    }

    public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
    {
        obj.SetValue(CornerRadiusProperty, value);
    }
    [Obsolete]
    public static readonly DependencyProperty DoubleAttachProperty = DependencyProperty.RegisterAttached(
        "DoubleAttach", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(0.0));
    [Obsolete]
    public static double GetDoubleAttach(DependencyObject d)
    {
        return (double)d.GetValue(DoubleAttachProperty);
    }
    [Obsolete]
    public static void SetDoubleAttach(DependencyObject obj, double value)
    {
        obj.SetValue(DoubleAttachProperty, value);
    }

    [Obsolete]
    public static readonly DependencyProperty IsClearTextButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsClearTextButtonBehaviorEnabled"
        , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsClearTextButtonBehaviorEnabledChanged));
    [Obsolete]
    [AttachedPropertyBrowsableForType(typeof(TextBox))]
    public static bool GetIsClearTextButtonBehaviorEnabled(DependencyObject d)
    {
        return (bool)d.GetValue(IsClearTextButtonBehaviorEnabledProperty);
    }
    [Obsolete]
    public static void SetIsClearTextButtonBehaviorEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(IsClearTextButtonBehaviorEnabledProperty, value);
    }
    [Obsolete]
    private static void IsClearTextButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button button = d as Button;
        if (e.OldValue != e.NewValue && button != null)
        {
            button.CommandBindings.Add(ClearTextCommandBinding);
        }
    }
    [Obsolete]
    public static readonly DependencyProperty DetailProperty = DependencyProperty.RegisterAttached(
        "Detail", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));
    [Obsolete]
    public static string GetDetail(DependencyObject d)
    {
        return (string)d.GetValue(DetailProperty);
    }
    [Obsolete]
    public static void SetDetail(DependencyObject obj, string value)
    {
        obj.SetValue(DetailProperty, value);
    }
    [Obsolete]
    public static readonly DependencyProperty IsOpenFileButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsOpenFileButtonBehaviorEnabled"
        , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsOpenFileButtonBehaviorEnabledChanged));
    [Obsolete]
    [AttachedPropertyBrowsableForType(typeof(TextBox))]
    public static bool GetIsOpenFileButtonBehaviorEnabled(DependencyObject d)
    {
        return (bool)d.GetValue(IsOpenFileButtonBehaviorEnabledProperty);
    }
    [Obsolete]
    public static void SetIsOpenFileButtonBehaviorEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(IsOpenFileButtonBehaviorEnabledProperty, value);
    }
    [Obsolete]
    private static void IsOpenFileButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button button = d as Button;
        if (e.OldValue != e.NewValue && button != null)
        {
            button.CommandBindings.Add(OpenFileCommandBinding);
        }
    }
    [Obsolete]
    public static readonly DependencyProperty IsOpenFolderButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsOpenFolderButtonBehaviorEnabled"
        , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsOpenFolderButtonBehaviorEnabledChanged));
    [Obsolete]
    [AttachedPropertyBrowsableForType(typeof(TextBox))]
    public static bool GetIsOpenFolderButtonBehaviorEnabled(DependencyObject d)
    {
        return (bool)d.GetValue(IsOpenFolderButtonBehaviorEnabledProperty);
    }
    [Obsolete]
    public static void SetIsOpenFolderButtonBehaviorEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(IsOpenFolderButtonBehaviorEnabledProperty, value);
    }
    [Obsolete]
    private static void IsOpenFolderButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button button = d as Button;
        if (e.OldValue != e.NewValue && button != null)
        {
            button.CommandBindings.Add(OpenFolderCommandBinding);
        }
    }
    [Obsolete]
    public static readonly DependencyProperty IsSaveFileButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsSaveFileButtonBehaviorEnabled"
        , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsSaveFileButtonBehaviorEnabledChanged));
    [Obsolete]
    [AttachedPropertyBrowsableForType(typeof(TextBox))]
    public static bool GetIsSaveFileButtonBehaviorEnabled(DependencyObject d)
    {
        return (bool)d.GetValue(IsSaveFileButtonBehaviorEnabledProperty);
    }
    [Obsolete]
    public static void SetIsSaveFileButtonBehaviorEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(IsSaveFileButtonBehaviorEnabledProperty, value);
    }
    [Obsolete]
    private static void IsSaveFileButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Button button = d as Button;
        if (e.OldValue != e.NewValue && button != null)
        {
            button.CommandBindings.Add(SaveFileCommandBinding);
        }
    }
    [Obsolete]
    public static RoutedUICommand ClearTextCommand { get; private set; }
    [Obsolete]
    private static readonly CommandBinding ClearTextCommandBinding;
    [Obsolete]
    private static void ClearButtonClick(object sender, ExecutedRoutedEventArgs e)
    {
        FrameworkElement tbox = e.Parameter as FrameworkElement;
        if (tbox == null) return;
        if (tbox is TextBox)
        {
            ((TextBox)tbox).Clear();
        }
        if (tbox is PasswordBox)
        {
            ((PasswordBox)tbox).Clear();
        }
        if (tbox is ComboBox)
        {
            ComboBox cb = tbox as ComboBox;
            cb.SelectedItem = null;
            cb.Text = string.Empty;
        }

        if (tbox is DatePicker)
        {
            DatePicker dp = tbox as DatePicker;
            dp.SelectedDate = null;
            dp.Text = string.Empty;
        }
        tbox.Focus();
    }

    [Obsolete]
    public static RoutedUICommand OpenFileCommand { get; private set; }
    [Obsolete]
    private static readonly CommandBinding OpenFileCommandBinding;
    [Obsolete]
    private static void OpenFileButtonClick(object sender, ExecutedRoutedEventArgs e)
    {
        FrameworkElement tbox = e.Parameter as FrameworkElement;
        TextBox txt = tbox as TextBox;
        string filter = txt.Tag == null ? "所有文件(*.*)|*.*" : txt.Tag.ToString();
        if (filter.Contains(".bin"))
        {
            filter += "|所有文件(*.*)|*.*";
        }
        if (txt == null) return;
        OpenFileDialog fd = new OpenFileDialog();
        fd.Title = "请选择文件";
        //“图像文件(*.bmp, *.jpg)|*.bmp;*.jpg|所有文件(*.*)|*.*”
        fd.Filter = filter;
        fd.FileName = txt.Text.Trim();
        if (fd.ShowDialog() == true)
        {
            txt.Text = fd.FileName;
        }
        tbox.Focus();
    }
    [Obsolete]
    public static RoutedUICommand OpenFolderCommand { get; private set; }
    [Obsolete]
    private static readonly CommandBinding OpenFolderCommandBinding;
    [Obsolete]
    private static void OpenFolderButtonClick(object sender, ExecutedRoutedEventArgs e)
    {
        //var tbox = e.Parameter as FrameworkElement;
        //var txt = tbox as TextBox;
        //if (txt == null) return;
        //System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
        //fd.Description = "请选择文件路径";
        //fd.SelectedPath = txt.Text.Trim();
        //if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //{
        //    txt.Text = fd.SelectedPath;
        //}
        //tbox.Focus();
    }
    [Obsolete]
    public static RoutedUICommand SaveFileCommand { get; private set; }
    [Obsolete]
    private static readonly CommandBinding SaveFileCommandBinding;
    [Obsolete]
    private static void SaveFileButtonClick(object sender, ExecutedRoutedEventArgs e)
    {
        //var tbox = e.Parameter as FrameworkElement;
        //var txt = tbox as TextBox;
        //if (txt == null) return;
        SaveFileDialog fd = new SaveFileDialog();
        //fd.Title = "文件保存路径";
        //fd.Filter = "所有文件(*.*)|*.*";
        //fd.FileName = txt.Text.Trim();
        //if (fd.ShowDialog() == DialogResult.OK)
        //{
        //    txt.Text = fd.FileName;
        //}
        //tbox.Focus();


        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件|*.*";
        saveFileDialog.FileName = "设置默认文件名";
        saveFileDialog.DefaultExt = "txt";
        saveFileDialog.AddExtension = true;
        saveFileDialog.RestoreDirectory = true;
        saveFileDialog.Title = "保存文件";
        if (saveFileDialog.ShowDialog() != true) return;
    }
    public static readonly DependencyProperty PasswordProperty =
      DependencyProperty.RegisterAttached("Password",
      typeof(string), typeof(Cattach),
      new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(Cattach));
    public static string GetPassword(DependencyObject dp)
    {
        return (string)dp.GetValue(PasswordProperty);
    }
    public static void SetPassword(DependencyObject dp, string value)
    {
        dp.SetValue(PasswordProperty, value);
    }
    private static bool GetIsUpdating(DependencyObject dp)
    {
        return (bool)dp.GetValue(IsUpdatingProperty);
    }
    private static void SetIsUpdating(DependencyObject dp, bool value)
    {
        dp.SetValue(IsUpdatingProperty, value);
    }
    private static void OnPasswordPropertyChanged(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        passwordBox.PasswordChanged -= PasswordChanged;
        if (!GetIsUpdating(passwordBox))
        {
            passwordBox.Password = (string)e.NewValue;
        }
        passwordBox.PasswordChanged += PasswordChanged;
    }
    private static void Attach(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        if (passwordBox == null)
            return;
        if ((bool)e.OldValue)
        {
            passwordBox.PasswordChanged -= PasswordChanged;
        }
        if ((bool)e.NewValue)
        {
            passwordBox.PasswordChanged += PasswordChanged;
        }
    }
    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        SetIsUpdating(passwordBox, true);
        SetPassword(passwordBox, passwordBox.Password);
        SetIsUpdating(passwordBox, false);
    }

    public static IList GetSelectedItems(DependencyObject obj)
    {
        return (IList)obj.GetValue(SelectedItemsProperty);
    }

    public static void SetSelectedItems(DependencyObject obj, IList value)
    {
        obj.SetValue(SelectedItemsProperty, value);
    }

    //Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...

    public static readonly DependencyProperty SelectedItemsProperty =

        DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(Cattach), new FrameworkPropertyMetadata(OnSelectedItemsChanged));

    public static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ListBox listBox = d as ListBox;

        if ((listBox != null) && (listBox.SelectionMode == SelectionMode.Multiple))
        {
            if (e.OldValue != null)
            {
                listBox.SelectionChanged -= OnlistBoxSelectionChanged;
            }

            IList collection = e.NewValue as IList;

            listBox.SelectedItems.Clear();

            if (collection != null)
            {
                foreach (object item in collection)
                {
                    listBox.SelectedItems.Add(item);
                }
                listBox.SelectionChanged += OnlistBoxSelectionChanged;
            }
        }
    }

    private static void OnlistBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        IList dataSource = GetSelectedItems(sender as DependencyObject);

        //添加用户选中的当前项.
        foreach (object item in e.AddedItems)
        {
            dataSource.Add(item);
        }

        //删除用户取消选中的当前项
        foreach (object item in e.RemovedItems)
        {
            dataSource.Remove(item);
        }
    }
    [Obsolete]
    public static readonly DependencyProperty BoolProperty = DependencyProperty.RegisterAttached(
        "Bool", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false));
    [Obsolete]
    public static bool GetBool(DependencyObject d)
    {
        return (bool)d.GetValue(BoolProperty);
    }
    [Obsolete]
    public static void SetBool(DependencyObject obj, bool value)
    {
        obj.SetValue(BoolProperty, value);
    }

    [Obsolete]
    public static bool GetIsOpen(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsOpenProperty);
    }
    [Obsolete]
    public static void SetIsOpen(DependencyObject obj, bool value)
    {
        obj.SetValue(IsOpenProperty, value);
    }

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(Cattach), new PropertyMetadata(true, OnIsOpenChanged));
    [Obsolete]
    public static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;
        bool n = (bool)e.NewValue;
        bool o = (bool)e.OldValue;
    }

    static Cattach()
    {
        //ClearTextCommand
        ClearTextCommand = new RoutedUICommand();
        ClearTextCommandBinding = new CommandBinding(ClearTextCommand);
        ClearTextCommandBinding.Executed += ClearButtonClick;
        //OpenFileCommand
        OpenFileCommand = new RoutedUICommand();
        OpenFileCommandBinding = new CommandBinding(OpenFileCommand);
        OpenFileCommandBinding.Executed += OpenFileButtonClick;
        //OpenFolderCommand
        OpenFolderCommand = new RoutedUICommand();
        OpenFolderCommandBinding = new CommandBinding(OpenFolderCommand);
        OpenFolderCommandBinding.Executed += OpenFolderButtonClick;

        SaveFileCommand = new RoutedUICommand();
        SaveFileCommandBinding = new CommandBinding(SaveFileCommand);
        SaveFileCommandBinding.Executed += SaveFileButtonClick;
    }
}

public static partial class Cattach
{
    public static bool GetIsBuzy(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsBuzyProperty);
    }

    public static void SetIsBuzy(DependencyObject obj, bool value)
    {
        obj.SetValue(IsBuzyProperty, value);
    }

    /// <summary> 是否等待 </summary>
    public static readonly DependencyProperty IsBuzyProperty =
        DependencyProperty.RegisterAttached("IsBuzy", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false));

    public static string GetBuzyText(DependencyObject obj)
    {
        return (string)obj.GetValue(BuzyTextProperty);
    }

    public static void SetBuzyText(DependencyObject obj, string value)
    {
        obj.SetValue(BuzyTextProperty, value);
    }

    // Using a DependencyProperty as the backing store for BuzyText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BuzyTextProperty =
        DependencyProperty.RegisterAttached("BuzyText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata("请等待"));


    public static Geometry GetPath(DependencyObject obj)
    {
        return (Geometry)obj.GetValue(PathProperty);
    }

    public static void SetPath(DependencyObject obj, Geometry value)
    {
        obj.SetValue(PathProperty, value);
    }

    public static readonly DependencyProperty PathProperty =
        DependencyProperty.RegisterAttached("Path", typeof(Geometry), typeof(Cattach), new FrameworkPropertyMetadata(default(Geometry)));


}

public static partial class Cattach
{
    [Obsolete]
    public static readonly DependencyProperty UseCloseProperty = DependencyProperty.RegisterAttached(
        "UseClose", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits, OnUseCloseChanged));
    [Obsolete]
    public static bool GetUseClose(DependencyObject d)
    {
        return (bool)d.GetValue(UseCloseProperty);
    }
    [Obsolete]
    public static void SetUseClose(DependencyObject obj, bool value)
    {
        obj.SetValue(UseCloseProperty, value);
    }
    [Obsolete]
    private static void OnUseCloseChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }

    public static readonly DependencyProperty IsDragEnterProperty = DependencyProperty.RegisterAttached(
        "IsDragEnter", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDragEnterChanged));

    public static bool GetIsDragEnter(DependencyObject d)
    {
        return (bool)d.GetValue(IsDragEnterProperty);
    }

    public static void SetIsDragEnter(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDragEnterProperty, value);
    }

    private static void OnIsDragEnterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }

}

public static partial class Cattach
{
    //public static readonly DependencyProperty CellMarginProperty = DependencyProperty.RegisterAttached(
    //    "CellMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnCellMarginChanged));

    //public static Thickness GetCellMargin(DependencyObject d)
    //{
    //    return (Thickness)d.GetValue(CellMarginProperty);
    //}

    //public static void SetCellMargin(DependencyObject obj, Thickness value)
    //{
    //    obj.SetValue(CellMarginProperty, value);
    //}

    //private static void OnCellMarginChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    //{

    //}
    [Obsolete]
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(
        "Orientation", typeof(Orientation), typeof(Cattach), new FrameworkPropertyMetadata(default(Orientation), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnOrientationChanged));
    [Obsolete]
    public static Orientation GetOrientation(DependencyObject d)
    {
        return (Orientation)d.GetValue(OrientationProperty);
    }
    [Obsolete]
    public static void SetOrientation(DependencyObject obj, Orientation value)
    {
        obj.SetValue(OrientationProperty, value);
    }
    [Obsolete]
    private static void OnOrientationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }
    [Obsolete]
    public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
        "Value", typeof(Double), typeof(Cattach), new FrameworkPropertyMetadata(default(Double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));
    [Obsolete]
    public static Double GetValue(DependencyObject d)
    {
        return (Double)d.GetValue(ValueProperty);
    }
    [Obsolete]
    public static void SetValue(DependencyObject obj, Double value)
    {
        obj.SetValue(ValueProperty, value);
    }
    [Obsolete]
    private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }

    [Obsolete]
    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
        "Text", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));
    [Obsolete]
    public static string GetText(DependencyObject d)
    {
        return (string)d.GetValue(TextProperty);
    }
    [Obsolete]
    public static void SetText(DependencyObject obj, string value)
    {
        obj.SetValue(TextProperty, value);
    }
    [Obsolete]
    private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }
    [Obsolete]
    public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.RegisterAttached(
        "IsIndeterminate", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsIndeterminateChanged));
    [Obsolete]
    public static bool GetIsIndeterminate(DependencyObject d)
    {
        return (bool)d.GetValue(IsIndeterminateProperty);
    }
    [Obsolete]
    public static void SetIsIndeterminate(DependencyObject obj, bool value)
    {
        obj.SetValue(IsIndeterminateProperty, value);
    }
    [Obsolete]
    private static void OnIsIndeterminateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }
    [Obsolete]
    public static readonly DependencyProperty IsStayOpenProperty = DependencyProperty.RegisterAttached(
        "IsStayOpen", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsStayOpenChanged));
    [Obsolete]
    public static string GetIsStayOpen(DependencyObject d)
    {
        return (string)d.GetValue(IsStayOpenProperty);
    }
    [Obsolete]
    public static void SetIsStayOpen(DependencyObject obj, string value)
    {
        obj.SetValue(IsStayOpenProperty, value);
    }
    [Obsolete]
    private static void OnIsStayOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }





    //#region - 历史记录相关 -

    ///// <summary>
    ///// 是否启用历史数据
    ///// </summary>
    //public static readonly DependencyProperty IsUseHistoryProperty = DependencyProperty.RegisterAttached(
    //    "IsUseHistory", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsUseHistoryChanged));

    //public static bool GetIsUseHistory(DependencyObject d)
    //{
    //    return (bool)d.GetValue(IsUseHistoryProperty);
    //}

    //public static void SetIsUseHistory(DependencyObject obj, bool value)
    //{
    //    obj.SetValue(IsUseHistoryProperty, value);
    //}

    //private static void OnIsUseHistoryChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    //{
    //    TextBox textBox = sender as TextBox;

    //    if (textBox == null) return;

    //    if (args.NewValue is bool b == true)
    //    {
    //        textBox.LostFocus -= TextBox_LostFocus;
    //        textBox.LostFocus += TextBox_LostFocus;
    //    }
    //    else
    //    {
    //        textBox.LostFocus -= TextBox_LostFocus;
    //    }
    //}

    //private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
    //{
    //    TextBox textBox = sender as TextBox;

    //    ObservableCollection<string> datas = Cattach.GetHistoryData(textBox);

    //    int capacity = Cattach.GetCapacity(textBox);

    //    string regex = Cattach.GetRegex(textBox);

    //    if (datas.Count > capacity)
    //    {
    //        datas = datas.Take(capacity)?.ToObservable();
    //    }

    //    if (!Regex.IsMatch(textBox.Text, regex)) return;

    //    datas.Remove(textBox.Text);
    //    datas.Insert(0, textBox.Text);

    //    Cattach.SetHistoryData(textBox, datas);

    //    //  Do ：保存本地
    //    IMetaSettingService metaSettingService = ServiceRegistry.Instance.GetInstance<IMetaSettingService>();

    //    if (metaSettingService == null) return;

    //    TextBoxHistoryMetaSetting metaSetting = new TextBoxHistoryMetaSetting() { Data = datas.ToList() };

    //    metaSettingService.Serilize(metaSetting, "default");
    //}


    ///// <summary>
    ///// 历史数据
    ///// </summary>
    //public static readonly DependencyProperty HistoryDataProperty = DependencyProperty.RegisterAttached(
    //    "HistoryData", typeof(ObservableCollection<string>), typeof(Cattach), new FrameworkPropertyMetadata(GetHistoryData(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHistoryDataChanged));

    //public static ObservableCollection<string> GetHistoryData(DependencyObject d)
    //{
    //    return (ObservableCollection<string>)d.GetValue(HistoryDataProperty);
    //}

    //public static void SetHistoryData(DependencyObject obj, ObservableCollection<string> value)
    //{
    //    obj.SetValue(HistoryDataProperty, value);
    //}

    //private static void OnHistoryDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    //{

    //}

    //private static ObservableCollection<string> GetHistoryData()
    //{
    //    IMetaSettingService metaSettingService = ServiceRegistry.Instance.GetInstance<IMetaSettingService>();

    //    TextBoxHistoryMetaSetting find = metaSettingService?.Deserilize<TextBoxHistoryMetaSetting>("default");

    //    return find?.Data?.ToObservable() ?? new ObservableCollection<string>();
    //}

    ///// <summary>
    ///// 数据容量
    ///// </summary>
    //public static readonly DependencyProperty CapacityProperty = DependencyProperty.RegisterAttached(
    //    "Capacity", typeof(int), typeof(Cattach), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCapacityChanged));

    //public static int GetCapacity(DependencyObject d)
    //{
    //    return (int)d.GetValue(CapacityProperty);
    //}

    //public static void SetCapacity(DependencyObject obj, int value)
    //{
    //    obj.SetValue(CapacityProperty, value);
    //}

    //private static void OnCapacityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    //{

    //}


    ///// <summary>
    ///// 验证数据的正则表达式 默认不等于空
    ///// </summary>
    //public static readonly DependencyProperty RegexProperty = DependencyProperty.RegisterAttached(
    //    "Regex", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(@"\S", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRegexChanged));

    //public static string GetRegex(DependencyObject d)
    //{
    //    return (string)d.GetValue(RegexProperty);
    //}

    //public static void SetRegex(DependencyObject obj, string value)
    //{
    //    obj.SetValue(RegexProperty, value);
    //}

    //private static void OnRegexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    //{

    //}


    ///// <summary>
    ///// 选中历史数据改变
    ///// </summary>
    //public static readonly DependencyProperty SelectedHistroyItemProperty = DependencyProperty.RegisterAttached(
    //    "SelectedHistroyItem", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedHistroyItemChanged));

    //public static string GetSelectedHistroyItem(DependencyObject d)
    //{
    //    return (string)d.GetValue(SelectedHistroyItemProperty);
    //}

    //public static void SetSelectedHistroyItem(DependencyObject obj, string value)
    //{
    //    obj.SetValue(SelectedHistroyItemProperty, value);
    //}

    //private static void OnSelectedHistroyItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    //{
    //    TextBox textBox = sender as TextBox;

    //    string config = args.NewValue as string;

    //    if (config == null) return;

    //    string regex = Cattach.GetRegex(textBox);

    //    if (!Regex.IsMatch(config, regex)) return;

    //    textBox.Text = config;
    //}

    //#endregion

    [Obsolete]
    public static string GetDateTimeFormat(DependencyObject obj)
    {
        return (string)obj.GetValue(DateTimeFormatProperty);
    }
    [Obsolete]
    public static void SetDateTimeFormat(DependencyObject obj, string value)
    {
        obj.SetValue(DateTimeFormatProperty, value);
    }

    [Obsolete]
    public static readonly DependencyProperty DateTimeFormatProperty =
        DependencyProperty.RegisterAttached("DateTimeFormat", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata("YYYY-MM-dd", OnDateTimeFormatChanged));
    [Obsolete]
    public static void OnDateTimeFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


    [Obsolete]
    public static readonly DependencyProperty EnumTypeSourceProperty = DependencyProperty.RegisterAttached(
        "EnumTypeSource", typeof(Type), typeof(Cattach), new FrameworkPropertyMetadata(default(Type), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnEnumTypeSourceChanged));
    [Obsolete]
    public static Type GetEnumTypeSource(DependencyObject d)
    {
        return (Type)d.GetValue(EnumTypeSourceProperty);
    }
    [Obsolete]
    public static void SetEnumTypeSource(DependencyObject obj, Type value)
    {
        obj.SetValue(EnumTypeSourceProperty, value);
    }
    [Obsolete]
    private static void OnEnumTypeSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        ItemsControl items = sender as ItemsControl;

        Type type = args.NewValue as Type;

        if (type != null)
        {
            Type actualEnumType = Nullable.GetUnderlyingType(type) ?? type;
            Array enumVlues = Enum.GetValues(actualEnumType);

            if (actualEnumType == type)
            {
                items.ItemsSource = enumVlues;
                return;
            }


            Array tempArray = Array.CreateInstance(actualEnumType, enumVlues.Length + 1);

            enumVlues.CopyTo(tempArray, 1);

            items.ItemsSource = tempArray;
        }
    }

    [Obsolete]
    public static Dock GetDock(DependencyObject obj)
    {
        return (Dock)obj.GetValue(DockProperty);
    }
    [Obsolete]
    public static void SetDock(DependencyObject obj, Dock value)
    {
        obj.SetValue(DockProperty, value);
    }
    [Obsolete]

    public static readonly DependencyProperty DockProperty =
        DependencyProperty.RegisterAttached("Dock", typeof(Dock), typeof(Cattach), new PropertyMetadata(default(Dock), OnDockChanged));
    [Obsolete]
    public static void OnDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Dock n = (Dock)e.NewValue;

        Dock o = (Dock)e.OldValue;
    }

    /// <summary>
    /// 通过绑定方式设置资源
    /// </summary>
    public static readonly DependencyProperty DynamicResourcesProperty =
        DependencyProperty.RegisterAttached(
            "DynamicResources",
            typeof(ResourceDictionary),
            typeof(Cattach),
            new PropertyMetadata(null, OnDynamicResourcesChanged));

    public static void SetDynamicResources(DependencyObject obj, ResourceDictionary value) =>
        obj.SetValue(DynamicResourcesProperty, value);

    public static ResourceDictionary GetDynamicResources(DependencyObject obj) =>
        (ResourceDictionary)obj.GetValue(DynamicResourcesProperty);

    private static void OnDynamicResourcesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element && e.NewValue is ResourceDictionary newResources)
        {
            element.Resources = newResources; // 替换整个 Resources
        }
    }
}
//public class TextBoxHistoryMetaSetting : IMetaSetting
//{
//    public List<string> Data { get; set; } = new List<string>();
//}
