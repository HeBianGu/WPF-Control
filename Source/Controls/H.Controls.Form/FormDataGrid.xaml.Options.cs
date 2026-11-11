// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form;

public partial class FormDataGrid
{
    public string ExceptPropertyNames
    {
        get => (string)GetValue(ExceptPropertyNamesProperty);
        set => SetValue(ExceptPropertyNamesProperty, value);
    }
    public static readonly DependencyProperty ExceptPropertyNamesProperty = RegisterOption(nameof(ExceptPropertyNames), typeof(string), null);

    public string UseGroupNames
    {
        get => (string)GetValue(UseGroupNamesProperty);
        set => SetValue(UseGroupNamesProperty, value);
    }
    public static readonly DependencyProperty UseGroupNamesProperty = RegisterOption(nameof(UseGroupNames), typeof(string), null);

    public string UseTabNames
    {
        get => (string)GetValue(UseTabNamesProperty);
        set => SetValue(UseTabNamesProperty, value);
    }
    public static readonly DependencyProperty UseTabNamesProperty = RegisterOption(nameof(UseTabNames), typeof(string), null);

    public bool UsePropertyView
    {
        get => (bool)GetValue(UsePropertyViewProperty);
        set => SetValue(UsePropertyViewProperty, value);
    }
    public static readonly DependencyProperty UsePropertyViewProperty = RegisterOption(nameof(UsePropertyView), typeof(bool), false);

    public bool UseDeclaredOnly
    {
        get => (bool)GetValue(UseDeclaredOnlyProperty);
        set => SetValue(UseDeclaredOnlyProperty, value);
    }
    public static readonly DependencyProperty UseDeclaredOnlyProperty = RegisterOption(nameof(UseDeclaredOnly), typeof(bool), false);

    public bool UseDisplayOnly
    {
        get => (bool)GetValue(UseDisplayOnlyProperty);
        set => SetValue(UseDisplayOnlyProperty, value);
    }
    public static readonly DependencyProperty UseDisplayOnlyProperty = RegisterOption(nameof(UseDisplayOnly), typeof(bool), false);

    public bool UseTypeConverterOnly
    {
        get => (bool)GetValue(UseTypeConverterOnlyProperty);
        set => SetValue(UseTypeConverterOnlyProperty, value);
    }
    public static readonly DependencyProperty UseTypeConverterOnlyProperty = RegisterOption(nameof(UseTypeConverterOnly), typeof(bool), false);

    public bool UseEnumerator
    {
        get => (bool)GetValue(UseEnumeratorProperty);
        set => SetValue(UseEnumeratorProperty, value);
    }
    public static readonly DependencyProperty UseEnumeratorProperty = RegisterOption(nameof(UseEnumerator), typeof(bool), true);

    public bool UseEnum
    {
        get => (bool)GetValue(UseEnumProperty);
        set => SetValue(UseEnumProperty, value);
    }
    public static readonly DependencyProperty UseEnumProperty = RegisterOption(nameof(UseEnum), typeof(bool), true);

    public bool UseDateTime
    {
        get => (bool)GetValue(UseDateTimeProperty);
        set => SetValue(UseDateTimeProperty, value);
    }
    public static readonly DependencyProperty UseDateTimeProperty = RegisterOption(nameof(UseDateTime), typeof(bool), true);

    public bool UseClass
    {
        get => (bool)GetValue(UseClassProperty);
        set => SetValue(UseClassProperty, value);
    }
    public static readonly DependencyProperty UseClassProperty = RegisterOption(nameof(UseClass), typeof(bool), true);

    public bool UseArray
    {
        get => (bool)GetValue(UseArrayProperty);
        set => SetValue(UseArrayProperty, value);
    }
    public static readonly DependencyProperty UseArrayProperty = RegisterOption(nameof(UseArray), typeof(bool), true);

    public bool UseInterface
    {
        get => (bool)GetValue(UseInterfaceProperty);
        set => SetValue(UseInterfaceProperty, value);
    }
    public static readonly DependencyProperty UseInterfaceProperty = RegisterOption(nameof(UseInterface), typeof(bool), true);

    public bool UsePrimitive
    {
        get => (bool)GetValue(UsePrimitiveProperty);
        set => SetValue(UsePrimitiveProperty, value);
    }
    public static readonly DependencyProperty UsePrimitiveProperty = RegisterOption(nameof(UsePrimitive), typeof(bool), true);

    public bool UseString
    {
        get => (bool)GetValue(UseStringProperty);
        set => SetValue(UseStringProperty, value);
    }
    public static readonly DependencyProperty UseStringProperty = RegisterOption(nameof(UseString), typeof(bool), true);

    public bool UseBoolen
    {
        get => (bool)GetValue(UseBoolenProperty);
        set => SetValue(UseBoolenProperty, value);
    }
    public static readonly DependencyProperty UseBoolenProperty = RegisterOption(nameof(UseBoolen), typeof(bool), true);

    public bool UseCommand
    {
        get => (bool)GetValue(UseCommandProperty);
        set => SetValue(UseCommandProperty, value);
    }
    public static readonly DependencyProperty UseCommandProperty = RegisterOption(nameof(UseCommand), typeof(bool), true);

    public bool UseCommandOnly
    {
        get => (bool)GetValue(UseCommandOnlyProperty);
        set => SetValue(UseCommandOnlyProperty, value);
    }
    public static readonly DependencyProperty UseCommandOnlyProperty = RegisterOption(nameof(UseCommandOnly), typeof(bool), false);

    private static DependencyProperty RegisterOption(string name, Type type, object defaultValue)
    {
        return DependencyProperty.Register(name, type, typeof(FormDataGrid),
            new FrameworkPropertyMetadata(defaultValue, OnOptionsChanged));
    }

    private static void OnOptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var grid = (FormDataGrid)d;
        grid.RefreshColumns();
    }

    private static string[] SplitNames(string names)
    {
        return names?.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }

    private static bool MatchesNames(string filter, string names)
    {
        return string.IsNullOrWhiteSpace(filter)
            || SplitNames(filter).Intersect(SplitNames(names), StringComparer.Ordinal).Any();
    }
}
